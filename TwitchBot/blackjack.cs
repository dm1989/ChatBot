using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchBot
{
    class blackjack
    {        
        public string[] cards = File.ReadAllLines(@"C:\Users\qwerty\AppData\Roaming\mIRC\cards.txt", Encoding.UTF8);
        public bool bj = false;
        public List<user> users;
        public int bet = 50;
        public int pot;
        public int nexbet = 50;
        public double timer = 120000;
        public bool endtriggered = false;        
        public List<Timer> rtimers = new List<Timer>();
        public List<pcards> used;
        public List<user> currentUsers;
        void newPlebs() { currentUsers = users.FindAll(x => x.playing == true); }        
        public void newGame() { used = new List<pcards>(); newPlebs(); endtriggered = false; }
        public void resetTimers() { foreach (Timer r in rtimers) { r.Dispose(); } rtimers.Clear(); }
    }
}
