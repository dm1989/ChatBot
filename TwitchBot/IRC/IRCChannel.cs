using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class IRCChannel
    {
        public string ChannelName { get; set; }
        public Game GameName { get; set; }
    }
    public enum Game
    {
        Dota = 0,
        Battlerite = 1
    }
}
