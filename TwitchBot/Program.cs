using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TwitchBot
{
    public class Program
    {
        public static TwitchBot.BotUser BotDetails = FileModule.JsonToClass<TwitchBot.BotUser>("BotDetails.json");
        static SteamBot SteamBot = new SteamBot();
        static List<IRCChannel> IRCChannels = new List<IRCChannel>();             
            
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);        
        static void Main(string[] args)
        {
            //ShowWindow(GetConsoleWindow(), 0);
            DotaModule.heros = FileModule.JsonToClass<List<Heros>>("Heros.json");
            var twitchIRC = new IRCClient();            
            Console.ReadLine();
        }
    }
}