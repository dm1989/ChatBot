using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace TwitchBot
{
    public class IRCClient
    {       
        static StreamReader reader;
        static StreamWriter writer;
        static DateTime lastMessage = DateTime.Parse("01/01/2000 12:00:00 AM");
        static TcpClient irc;
        static System.Threading.Thread ircThread;
        public IRCClient()
        {            
            ircThread = new System.Threading.Thread(Output)
            {
                Name = "TwitchIrcThread"
            };
            ircThread.Start();
        }
        private void ConnectIrc()
        {
            var IRCChannels = FileModule.JsonToClass<List<IRCChannel>>("BotUsers.json");
            irc = new TcpClient("irc.twitch.tv", 6667);
            var stream = irc.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine("PASS " + Program.BotDetails.TwitchOauth
                + Environment.NewLine
                + "NICK " + Program.BotDetails.TwitchNick + Environment.NewLine
                + "USER "+ Program.BotDetails.TwitchNick + " 8 * :" + Program.BotDetails.TwitchNick);
            writer.WriteLine("JOIN #" + Program.BotDetails.TwitchNick);
            foreach (IRCChannel c in IRCChannels)
            {
                writer.WriteLine("JOIN #" + c.ChannelName);
            }
            writer.Flush();            
        }
        public void Output()
        {
            while (true)
            {                
                if ((DateTime.Now - lastMessage) > TimeSpan.FromMinutes(6))
                {
                    ConnectIrc();
                    lastMessage = DateTime.Now;
                }
                string output = reader.ReadLine();
                if (output != null)
                {
                    try
                    {
                        string[] channelOutput = output.Split(Char.Parse("#"));
                        string channel = channelOutput[1].Split(Char.Parse(" "))[0];
                        string user = output.Split(Char.Parse("!"))[0].Substring(1);
                        if (user.Contains("plasma")) { writer.WriteLine("PRIVMSG #" + channel + " :" + "/ban " + user); writer.Flush(); }
                        string msg = output.Split(Char.Parse(":"))[2];
                        Console.WriteLine(channel + ": " + user + ": " + msg);
                        if (channel.Equals(Program.BotDetails.TwitchNick))
                        {
                            IRCModule.BotHome(user, msg, channel, HomeSend);
                        }
                        else
                        {
                            IRCModule.DotaBotHandler(user, msg, channel,
                                delegate (string m) { Send(channel, m); }
                                );
                        }
                    }
                    catch
                    {
                        if (output.Equals("PING :tmi.twitch.tv"))
                        {
                            writer.WriteLine("PONG :tmi.twitch.tv");
                            writer.Flush();
                            lastMessage = DateTime.Now;
                        }
                        else if (!output.Equals(""))
                        {
                            lastMessage = DateTime.Now;
                        }
                        Console.WriteLine(output);
                    }
                }                                
            }
        }
        public void HomeSend(string msg)
        {
            writer.WriteLine("PRIVMSG #" + Program.BotDetails.TwitchNick + " :" + msg);
            writer.Flush();
        }
        public void Send(string channel, string msg)
        {
            writer.WriteLine("PRIVMSG #" + channel + " :" + msg);
            writer.Flush();
        }        
    }
}
