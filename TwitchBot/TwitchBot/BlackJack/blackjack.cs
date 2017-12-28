using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

namespace TwitchBot
{
    class blackjack
    {
        //    public string[] cards = File.ReadAllLines(@"C:\Users\qwerty\AppData\Roaming\mIRC\cards.txt", Encoding.UTF8);
        //    public bool bj = false;
        //    public static JavaScriptSerializer json = new JavaScriptSerializer();
        //    public List<user> users = json.Deserialize<List<user>>(File.ReadAllText(@"C:\Users\qwerty\AppData\Roaming\mIRC\plebs.json", Encoding.UTF8));
        //    public int bet = 50;
        //    public int pot;
        //    public int nexbet = 50;
        //    public double timer = 120000;
        //    public bool endtriggered = false;        
        //    public List<Timer> rtimers = new List<Timer>();
        //    public List<pcards> used;
        //    public List<user> currentUsers;
        //    void newPlebs() { currentUsers = users.FindAll(x => x.playing == true); }        
        //    public void newGame() { used = new List<pcards>(); newPlebs(); endtriggered = false; }
        //    public void resetTimers() { foreach (Timer r in rtimers) { r.Dispose(); } rtimers.Clear(); }
        //    public void bjbot()
        //    {
        //        if (bj == false || users.FindAll(x => x.playing == true).Count < 2) { return; }
        //        resetTimers();
        //        newGame();
        //        pot = (bet * currentUsers.Count);
        //        b.sendMessage("/me Current Pot: $" + pot.ToString() + "($" + bet.ToString() + " each)");
        //        foreach (user c in users.FindAll(x => x.playing == true))
        //        {
        //            c.play = false;
        //            c.chips = c.chips - bet;
        //            card(c.nick); card(c.nick);
        //            print(c.nick);
        //        }
        //        rtimer(timer, endround);
        //    }
        //    public void endround()
        //    {
        //        if (!endtriggered)
        //        {
        //            resetTimers();
        //            List<string> winner = new List<string>();
        //            int highest = 0;
        //            foreach (user p in currentUsers)
        //            {
        //                int to = twentyone(p.nick);
        //                if (to <= 21)
        //                {
        //                    if (to > highest)
        //                    {
        //                        highest = to;
        //                    }
        //                }
        //            }
        //            foreach (user p in currentUsers)
        //            {
        //                int to = twentyone(p.nick);
        //                if (to <= 21)
        //                {
        //                    if (to > highest)
        //                    {
        //                        winner.Clear();
        //                        winner.Add(p.nick);
        //                    }
        //                    else if (to == highest)
        //                    {
        //                        winner.Add(p.nick);
        //                    }
        //                }
        //            }
        //            if (winner.Count == 1)
        //            {
        //                user temp = users.Find(x => x.nick == winner[0]);
        //                b.sendMessage("winner is " + winner[0] + " $" + temp.chips + " + $" + pot.ToString());
        //                users.Find(x => x.nick == winner[0]).chips = temp.chips + pot;
        //            }
        //            else if (winner.Count > 1)
        //            {
        //                string s = "Pot is split:";
        //                foreach (string w in winner)
        //                {
        //                    user temp = users.Find(x => x.nick == w);
        //                    decimal d = pot / winner.Count;
        //                    users.Find(x => x.nick == w).chips = temp.chips + (int)Math.Ceiling(d);
        //                    s = s + " " + temp.nick + " $" + (int)Math.Ceiling(d);
        //                }
        //                b.sendMessage(s);
        //            }
        //            else
        //            {
        //                b.sendMessage("everyone loses :(");
        //            }
        //            bet = nexbet;
        //            saveUsers(users);
        //            if (bj && users.FindAll(x => x.playing == true).Count >= 2)
        //            {
        //                rtimer(10000, bjbot);
        //            }
        //        }
        //        endtriggered = true;
        //    }
        //    public void join(string u)
        //    {
        //        users.Find(x => x.nick == u).playing = true;
        //    }
        //    public int twentyone(string u)
        //    {
        //        List<pcards> cards = used.FindAll(x => x.user == u);
        //        int aces = 0;
        //        int total = 0;
        //        for (int i = 0; i < cards.Count; i++)
        //        {
        //            string card = cards[cards[i].usercard].Substring(0, 1);
        //            int tempval = new int();
        //            try
        //            {
        //                tempval = int.Parse(card);
        //                if (tempval == 1) { tempval = 10; }
        //            }
        //            catch
        //            {
        //                if ((card.Equals("J")) || (card.Equals("Q")) || (card.Equals("K")))
        //                {
        //                    tempval = 10;
        //                }
        //                else
        //                {
        //                    aces++;
        //                }
        //            }
        //            total = total + tempval;
        //        }
        //        if (aces > 1)
        //        {
        //            for (int i = 0; i < aces - 1; i++)
        //            {
        //                total++;
        //            }
        //            if (total <= 10)
        //            {
        //                total = total + 11;
        //            }
        //            else
        //            {
        //                total++;
        //            }
        //        }
        //        else if (aces == 1)
        //        {
        //            if (total <= 10)
        //            {
        //                total = total + 11;
        //            }
        //            else
        //            {
        //                total++;
        //            }
        //        }
        //        return total;
        //    }
        //    public void print(string u)
        //    {
        //        List<pcards> hand = used.FindAll(x => x.user == u);
        //        string cards = "";
        //        for (int i = 0; i < hand.Count; i++)
        //        {
        //            int tempcard = hand[i].usercard;
        //            if (i == (hand.Count - 1))
        //            {
        //                cards = cards + cards[tempcard];
        //            }
        //            else
        //            {
        //                cards = cards + cards[tempcard] + "|";
        //            }
        //        }
        //        b.sendMessage(cards + " - " + twentyone(u) + "/21 - " + u + " - $" + currentUsers.Find(x => x.nick == u).chips);
        //    }
        //    public void hit(string u)
        //    {
        //        if (twentyone(u) > 21)
        //        {
        //            return;
        //        }
        //        card(u);
        //        if (twentyone(u) > 21)
        //        {
        //            string temp = "";
        //            foreach (pcards t in used.FindAll(x => x.user == u)) { temp = temp + cards[t.usercard] + "|"; }
        //            b.sendMessage(temp.Substring(0, temp.Length - 1) + " - you done fucked up " + u);
        //            stand(u);
        //        }
        //        else
        //        {
        //            print(u);
        //        }
        //    }

        //    static public void card(string u)
        //    {
        //        int num = r.Next(0, 52);
        //        if (used.Count > 52)
        //        {
        //            used.Add(new pcards { user = u, usercard = 0 });
        //            return;
        //        }
        //        if (!used.Any<pcards>(prod => prod.usercard == num))
        //        {
        //            num = r.Next(0, 52);
        //        }
        //        else
        //        {
        //            while (used.Any<pcards>(prod => prod.usercard == num))
        //            {
        //                num = r.Next(0, 52);
        //            }
        //        }
        //        used.Add(new pcards { user = u, usercard = num });
        //    }
        //    public void stand(string u)
        //    {
        //        if (currentUsers.Find(x => x.nick == u).playing == true)
        //        {
        //            currentUsers.Find(x => x.nick == u).play = true;
        //            if (currentUsers.Exists(x => x.play == false))
        //            {
        //                return;
        //            }
        //            else
        //            {
        //                endround();
        //            }
        //        }
        //    }
        //    public static string Left(string s, int length)
        //    {
        //        if (s.Length < length) { return s; }
        //        string result = s.Substring(0, length);
        //        return result;
        //    }
        //    public void saveUsers(List<user> u)
        //    {
        //        string users = json.Serialize(u);
        //        File.WriteAllText(@"C:\Users\qwerty\AppData\Roaming\mIRCusers.json", users);
        //    }
        //    public void timer(double t, Action m)
        //    {
        //        Timer time = new Timer();
        //        time.Interval = t;
        //        time.Elapsed += (sender, e) => { m(); time.Dispose(); };
        //        time.AutoReset = false;
        //        time.Enabled = true;
        //    }
        //    public void rtimer(double t, Action m)
        //    {
        //        Timer temptimer = new Timer();
        //        temptimer.Interval = t;
        //        temptimer.Elapsed += (sender, e) => { m(); temptimer.Dispose(); };
        //        temptimer.AutoReset = false;
        //        temptimer.Enabled = true;
        //        bj.rtimers.Add(temptimer);
        //    }
    }
}
