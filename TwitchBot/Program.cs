using Newtonsoft.Json.Linq;
using SimpleIRCLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Web.Script.Serialization;

namespace TwitchBot
{
    class Program
    {

        public SimpleIRC qwe = new SimpleIRC();
        //memes
        static string[] joke = File.ReadAllLines(@"C:\joke.txt", Encoding.UTF8);
        static List<string> users = new List<string>();
        static string currentJoke = "";
        //other
        static blackjack bj = new blackjack();

        static Random r = new Random();

        public static JavaScriptSerializer json = new JavaScriptSerializer();
        public static WebClient web = new WebClient();
        public static Program b = new Program();
        static SteamBot sb = new SteamBot();


        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);   
            b.start();
            bj.users = json.Deserialize<List<user>>(File.ReadAllText(@"C:\users.json", Encoding.UTF8));
        }
        public static void sendThis(string m)
        {
            b.sendMessage(m);
        }
        public static void mmr(string m)
        {
            try
            {
                string twitchJsonString = web.DownloadString("https://api.twitch.tv/channels/" + m.Split(Char.Parse(" "))[1] + "?client_id=abcdefghijklmnopqrstuvwxyz");
                dynamic twitch = JObject.Parse(twitchJsonString);
                string steamid64 = twitch.steam_id.ToString();
                if (steamid64.Equals(""))
                {
                    b.sendMessage(m.Split(Char.Parse(" "))[1] + " please link your steam to twitch");
                    return;
                }
                string steamid32 = (UInt64.Parse(steamid64.Substring(3)) - 61197960265728).ToString();
                SteamBot.ProfileCard(UInt32.Parse(steamid32));
            }            
            catch
            {
                b.sendMessage("user not found");
            }
        }

        public void bjbot()
        {
            if (bj.bj == false || bj.users.FindAll(x => x.playing == true).Count < 2) { return; }
            bj.resetTimers();
            bj.newGame();
            bj.pot = (bj.bet * bj.currentUsers.Count);
            b.sendMessage("/me Current Pot: $" + bj.pot.ToString() + "($" + bj.bet.ToString() + " each)");
            foreach (user c in bj.users.FindAll(x => x.playing == true))
            {
                c.play = false;
                c.chips = c.chips - bj.bet;
                card(c.nick); card(c.nick);
                print(c.nick);
            }
            rtimer(bj.timer, endround);
        }
        public void endround()
        {
            if (!bj.endtriggered)
            {
                bj.resetTimers();
                List<string> winner = new List<string>();
                int highest = 0;
                foreach (user p in bj.currentUsers)
                {
                    int to = twentyone(p.nick);
                    if (to <= 21)
                    {
                        if (to > highest)
                        {
                            highest = to;
                        }
                    }
                }
                foreach (user p in bj.currentUsers)
                {
                    int to = twentyone(p.nick);
                    if (to <= 21)
                    {
                        if (to > highest)
                        {
                            winner.Clear();
                            winner.Add(p.nick);
                        }
                        else if (to == highest)
                        {
                            winner.Add(p.nick);
                        }
                    }
                }
                if (winner.Count == 1)
                {
                    user temp = bj.users.Find(x => x.nick == winner[0]);
                    b.sendMessage("winner is " + winner[0] + " $" + temp.chips + " + $" + bj.pot.ToString());
                    bj.users.Find(x => x.nick == winner[0]).chips = temp.chips + bj.pot;
                }
                else if (winner.Count > 1)
                {
                    string s = "Pot is split:";
                    foreach (string w in winner)
                    {
                        user temp = bj.users.Find(x => x.nick == w);
                        decimal d = bj.pot / winner.Count;
                        bj.users.Find(x => x.nick == w).chips = temp.chips + (int)Math.Ceiling(d);
                        s = s + " " + temp.nick + " $" + (int)Math.Ceiling(d);
                    }
                    b.sendMessage(s);
                }
                else
                {
                    b.sendMessage("everyone loses :(");
                }
                bj.bet = bj.nexbet;
                saveUsers(bj.users);
                if (bj.bj && bj.users.FindAll(x => x.playing == true).Count >= 2)
                {
                    rtimer(10000, bjbot);
                }
            }
            bj.endtriggered = true;
        }
        public void join(string u)
        {
            bj.users.Find(x => x.nick == u).playing = true;
        }
        public int twentyone(string u)
        {
            List<pcards> cards = bj.used.FindAll(x => x.user == u);
            int aces = 0;
            int total = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                string card = bj.cards[cards[i].usercard].Substring(0, 1);
                int tempval = new int();
                try
                {
                    tempval = int.Parse(card);
                    if (tempval == 1) { tempval = 10; }
                }
                catch
                {
                    if ((card.Equals("J")) || (card.Equals("Q")) || (card.Equals("K")))
                    {
                        tempval = 10;
                    }
                    else
                    {
                        aces++;
                    }
                }
                total = total + tempval;
            }
            if (aces > 1)
            {
                for (int i = 0; i < aces - 1; i++)
                {
                    total++;
                }
                if (total <= 10)
                {
                    total = total + 11;
                }
                else
                {
                    total++;
                }
            }
            else if (aces == 1)
            {
                if (total <= 10)
                {
                    total = total + 11;
                }
                else
                {
                    total++;
                }
            }
            return total;
        }
        public void print(string u)
        {
            List<pcards> hand = bj.used.FindAll(x => x.user == u);
            string cards = "";
            for (int i = 0; i < hand.Count; i++)
            {
                int tempcard = hand[i].usercard;
                if (i == (hand.Count - 1))
                {
                    cards = cards + bj.cards[tempcard];
                }
                else
                {
                    cards = cards + bj.cards[tempcard] + "|";
                }
            }
            b.sendMessage(cards + " - " + twentyone(u) + "/21 - " + u + " - $" + bj.currentUsers.Find(x => x.nick == u).chips);
        }
        public void hit(string u)
        {
            if (twentyone(u) > 21)
            {
                return;
            }
            card(u);
            if (twentyone(u) > 21)
            {
                string temp = "";
                foreach (pcards t in bj.used.FindAll(x => x.user == u)) { temp = temp + bj.cards[t.usercard] + "|"; }
                b.sendMessage(temp.Substring(0, temp.Length - 1) + " - you busted" + u);
                stand(u);
            }
            else
            {
                print(u);
            }
        }

        static public void card(string u)
        {
            int num = r.Next(0, 52);
            if (bj.used.Count > 52)
            {
                bj.used.Add(new pcards { user = u, usercard = 0 });
                return;
            }
            if (!bj.used.Any<pcards>(prod => prod.usercard == num))
            {
                num = r.Next(0, 52);
            }
            else
            {
                while (bj.used.Any<pcards>(prod => prod.usercard == num))
                {
                    num = r.Next(0, 52);
                }
            }
            bj.used.Add(new pcards { user = u, usercard = num });
        }
        public void stand(string u)
        {
            if (bj.currentUsers.Find(x => x.nick == u).playing == true)
            {
                bj.currentUsers.Find(x => x.nick == u).play = true;
                if (bj.currentUsers.Exists(x => x.play == false))
                {
                    return;
                }
                else
                {
                    endround();
                }
            }
        }
        public void bot(string m, string u)
        {
            string q = m.ToLower();
            if (q.Equals("!joke") || q.Equals("!jokes") || q.Equals("!puns") || q.Equals("!pun")) { jokes(); return; }
            if (q.Equals("!modme")) { modme(u); return; }
            if (q.Equals("!commands"))
            {
                qwe.sendMessage("!join !leave !stay !hit");
                return;
            }
            if (u == "user")
            {
                if (Left(q, 5).Equals("!give"))
                {
                    try
                    {
                        string[] temp = q.Split(Char.Parse(" "));
                        if (bj.users.Exists(x => x.nick == temp[1])) { bj.users.Find(x => x.nick == temp[1]).chips = bj.users.Find(x => x.nick == temp[1]).chips + int.Parse(temp[2]); }
                    }
                    catch { }
                    return;
                }
                if (q.Equals("!on"))
                {
                    bj.bj = true;
                    bjbot();
                    return;
                }
                if (q.Equals("!off"))
                {
                    bj.bj = false;
                    return;
                }
                if (Left(q, 4).Equals("!bet"))
                {
                    try
                    {
                        bj.nexbet = int.Parse(q.Substring(5));
                    }
                    catch { }
                    return;
                }
                if (Left(q, 6).Equals("!timer"))
                {
                    try
                    {
                        int temp = int.Parse(q.Substring(7));
                        temp = temp * 1000;
                        bj.timer = temp;
                    }
                    catch { }
                    return;
                }
                if (Left(q, 4).Equals("!add"))
                {
                    string temp = q.Substring(5);
                    if (bj.users.Exists(x => x.nick == temp)) { bj.users.Find(x => x.nick == temp).playing = true; }
                    else { bj.users.Add(new user() { nick = temp, chips = 1000 }); }
                    return;
                }
                if (Left(q, 7).Equals("!remove"))
                {
                    string temp = q.Substring(8);
                    if (bj.users.Exists(x => x.nick == temp)) { bj.users.Find(x => x.nick == temp).playing = false; }
                    return;
                }
                if (q.Equals("!end") || q.Equals("!endround")) { endround(); }
            }
            if (Left(q, 5).Equals("!join"))
            {
                string temp = u;
                if (bj.users.Exists(x => x.nick == temp)) { bj.users.Find(x => x.nick == temp).playing = true; }
                else { bj.users.Add(new user() { nick = temp, chips = 1000 }); }
                return;
            }

            if (Left(q, 4).Equals("?mmr"))
            {

                mmr(q);

            }
            if (Left(q, 3).Equals("?pi"))
            {

                playerInfo(q);

            }
            if (Left(q, 9).Equals("?opendota"))
            {

                opendota(q);

            }
            if (q.Equals("?np"))
            {
                SteamBot.SourceTv();
            }


            if (Left(q, 6).Equals("!leave"))
            {
                string temp = u;
                if (bj.users.Exists(x => x.nick == temp)) { bj.users.Find(x => x.nick == temp).playing = false; }
                else { bj.users.Add(new user() { nick = temp, chips = 1000 }); }
                return;
            }
            if (q.Equals("!stand") || q.Equals("!stay"))
            {
                stand(u);
                return;
            }
            if (q.Equals("!hit"))
            {
                if (!bj.endtriggered) { hit(u); }
                return;
            }
            if (u.Equals(currentJoke) == false)
            {
                if (users.Count >= 3)
                {
                    sendMessage(currentJoke + " x" + users.Count.ToString() + " Combo!!");
                }
                users.Clear();
                currentJoke = m;
                users.Add(u);
                return;
            }
            else
            {
                if (users.Contains(u) == false)
                {
                    users.Add(u);
                    return;
                }
            }
        }

        private void playerInfo(string q)
        {
            try
            {
                SteamBot.playerInfo = q.Substring(4);
                SteamBot.SourceTv();
            }
            catch { }
        }

        private void opendota(string q)
        {
            try
            {
                string twitchJsonString = web.DownloadString("https://api.twitch.tv/channels/" + q.Split(Char.Parse(" "))[1] + "?client_id=abcdefghijklmopqrstuvwxyz");
                dynamic twitch = JObject.Parse(twitchJsonString);
                string steamid64 = twitch.steam_id.ToString();
                if (steamid64.Equals(""))
                {
                    b.sendMessage(q.Split(Char.Parse(" "))[1] + " please link your steam to twitch");
                    return;
                }
                string steamid32 = (UInt64.Parse(steamid64.Substring(3)) - 61197960265728).ToString();
                string odJsonString = web.DownloadString("https://api.opendota.com/api/players/" + steamid32);
                dynamic od = JObject.Parse(odJsonString);
                string odMMR = od.solo_competitive_rank.ToString();
                if (!odMMR.Equals(""))
                {
                    b.sendMessage(odMMR);
                }
                else
                {
                    if (!od.mmr_estimate.estimate.ToString().Equals(""))
                    {
                        b.sendMessage(od.mmr_estimate.estimate.ToString());
                    }
                }
            }
            catch
            {
                b.sendMessage("user not found");
            }
        }

        public static string Left(string s, int length)
        {
            if (s.Length < length) { return s; }
            string result = s.Substring(0, length);
            return result;
        }
        public void saveUsers(List<user> u)
        {
            string users = json.Serialize(u);
            File.WriteAllText(@"C:\users.json", users);
        }
        public void start()
        {
            qwe.setupIrc("irc.twitch.tv", 6667, "user", "oauth:", "#channel", output);
            qwe.setDebugCallback(debug);
            qwe.startClient();
        }
        public void jokes()
        {
            qwe.sendMessage(joke[r.Next(0, joke.Length - 1)]);
        }
        public void modme(string u)
        {
            Program hi = new Program();
            hi.timer(1000, () =>
            {
                qwe.sendMessage("/timeout " + u + " 1");
            });
        }
        public void timer(double t, Action m)
        {
            Timer time = new Timer();
            time.Interval = t;
            time.Elapsed += (sender, e) => { m(); time.Dispose(); };
            time.AutoReset = false;
            time.Enabled = true;
        }
        public void rtimer(double t, Action m)
        {
            Timer temptimer = new Timer();
            temptimer.Interval = t;
            temptimer.Elapsed += (sender, e) => { m(); temptimer.Dispose(); };
            temptimer.AutoReset = false;
            temptimer.Enabled = true;
            bj.rtimers.Add(temptimer);
        }
        public void sendMessage(string meme)
        {
            qwe.sendMessage(meme);
        }
        public void output(string user, string msg)
        {
            if (user.Equals("ING :tmi.twitch.tv")) { return; }
            string n = "";
            if (msg.Length > 2)
            {
                n = msg.Substring(2);
                bot(n, user);
            }
            //Console.WriteLine(user + ": " + n);
        }
        public void debug(string msg)
        {
            //Console.WriteLine(msg);
        }
    }
}