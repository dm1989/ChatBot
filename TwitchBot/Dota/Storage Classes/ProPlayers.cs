using SteamKit2.GC.Dota.Internal;
using System.Collections.Generic;
using System;
using static CMsgGCPlayerInfoRequest;
namespace TwitchBot
{
    public class ProPlayers
    {
        public List<PlayerInfo> PlayerInfoRequest { get; set; } = new List<PlayerInfo>();
        public List<CSourceTVGameSmall.Player> PlayerInfo { get; set; } = new List<CSourceTVGameSmall.Player>();
        public int GameMode { get; set; }
        public bool GameFound { get; set; } = false;
        public string AverageGameMMR { get; set; }
    }    
}
