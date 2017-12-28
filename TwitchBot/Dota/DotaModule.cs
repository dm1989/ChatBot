using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using System.Collections.Generic;
using static CMsgGCPlayerInfoRequest;
namespace TwitchBot
{
    public static class DotaModule
    {
        private static string[] colors = { "Blue", "Teal", "Purple", "Yellow", "Orange", "Pink", "Gray", "Light Blue", "Green", "Brown" };
        public static List<Heros> heros;
        public static uint ConvertSteamId32(ulong steamId64)
        {
            if (steamId64 == 0) { return 0; }
            try
            {
                return (uint)(steamId64 - 76561197960265728);                
            }
            catch { return 0; }
        }
        public static ulong ConvertSteamId64(uint steamId32)
        {
            if (steamId32 == 0) { return 0; }
            try
            {
                return (ulong)(steamId32 + 76561197960265728);                
            }
            catch { return 0; }
        }
        public static ProPlayers SourceTvProPlayers(List<CSourceTVGameSmall> gameList, uint steamid32)
        {
            var ProPlayers = new ProPlayers();
            foreach (var game in gameList)
            {
                foreach (var player in game.players)
                {
                    if(player.account_id == steamid32)
                    {                        
                        ProPlayers.GameFound = true;                        
                        break;
                    }
                }
                if (ProPlayers.GameFound)
                {
                    ProPlayers.GameMode = (int)game.game_mode;
                    ProPlayers.AverageGameMMR = game.average_mmr.ToString();
                    foreach (var player in game.players)
                    {
                        ProPlayers.PlayerInfoRequest.Add(new PlayerInfo() { account_id = player.account_id });
                        ProPlayers.PlayerInfo.Add(player);                        
                    }
                    break;
                }
            }            
            return ProPlayers;
        }        
        public static string FullGeneralPlayerInfo(CMsgGCPlayerInfo.PlayerInfo player, CMsgGCPlayerInfoRequest.PlayerInfo currentPlayer)
        {            
            string returnString = "";
            if (player.team_name != "") { returnString = player.team_name + "."; }
            if (player.name.Equals("") || player.name.Equals(" "))
            {
                returnString = returnString + "No Name Found";
            }
            else { returnString = returnString + player.name; }
            if (player.is_pro) { returnString = returnString + ", ProAccount: Yes"; }
            else { returnString = returnString + ", ProAccount: No"; }
            returnString = returnString + ", Account: steamcommunity.com/profiles/"
            + ConvertSteamId64(currentPlayer.account_id).ToString()
            + " DotaBuff: dotabuff.com/players/"
            + (currentPlayer.account_id).ToString() + "¯\\_(ツ)_/¯";
            return returnString;
        }
        public static CMsgGCPlayerInfo.PlayerInfo PlayerInfo(ClientGCMsgProtobuf<CMsgGCPlayerInfo> players, string color)
        {
            int playerPosition = FindPlayerPosition(color);
            if (playerPosition == 10) { return null; }
            else
            {
                return players.Body.player_infos[playerPosition];
            }
        }        
        public static string SmallPlayerInfos(ClientGCMsgProtobuf<CMsgGCPlayerInfo> players, ProPlayers proPlayers)
        {            
            string returnString = "";
            if (proPlayers.GameMode != 2)
            {
                returnString = "Average MMR [" + proPlayers.AverageGameMMR + "]: ";
            }
            else
            {
                returnString = "Captains Mode: ";
            }            
            string tempHero = "";
            for (int i = 0; i < 10; i++)
            {
                var player = players.Body.player_infos[i];
                if (player.is_pro)
                {
                    try
                    {
                        var p = proPlayers.PlayerInfo.Find(x => x.account_id == player.account_id);
                        tempHero = heros.Find(h => h.Id == p.hero_id).localized_name;   
                    }
                    catch
                    {
                        if(proPlayers.GameMode != 2)
                        {
                            tempHero = FindPlayerColor(i);
                        }
                        else
                        {
                            tempHero = "Not Picked";
                        }
                    }
                    if(player.team_name != "")
                    {
                        returnString = returnString + player.team_name + ".";
                    }
                    returnString = returnString + player.name + " (" + tempHero + "), ";                    
                }
            }
            return returnString.Substring(0, returnString.Length - 2);
        }
        private static string FindPlayerColor(int playerPosition)
        {
            if (playerPosition >= 0 && playerPosition <= 9)
            {                
                return colors[playerPosition];
            }
            else
            {
                return "Not Picked";
            }
        }
        public static int FindPlayerPosition(string playerColor)
        {                
            for (int i = 0; i < 10; i++)
            {
                if (colors[i].ToLower().Equals(playerColor.ToLower()))
                {
                    return i; 
                }
            }
            return 10;
            
        }
        private static string GetLocalizedHeroName(uint heroId)
        {
            try
            {
                return heros.Find(x => x.Id == heroId).localized_name;
            }
            catch
            {
                return "Hero Not Found";
            }
        }
        public static string ProfileCardRank(ClientGCMsgProtobuf<CMsgDOTAProfileCard> profileCard)
        {
            string returnString = "";
            uint rank_tier = profileCard.Body.rank_tier;
            if (rank_tier == 0) { returnString = "Uncalibrated Pleb"; }
            else if (rank_tier < 20)
            {
                returnString = "Herald ";
                returnString = returnString + (rank_tier - 10).ToString();
            }
            else if (rank_tier < 30)
            {
                returnString = "Guardian ";
                returnString = returnString + (rank_tier - 20).ToString();
            }
            else if (rank_tier < 40)
            {
                returnString = "Crusader ";
                returnString = returnString + (rank_tier - 30).ToString();
            }
            else if (rank_tier < 50)
            {
                returnString = "Archon ";
                returnString = returnString + (rank_tier - 40).ToString();
            }
            else if (rank_tier < 60)
            {
                returnString = "Legend ";
                returnString = returnString + (rank_tier - 50).ToString();
            }
            else if (rank_tier < 70)
            {
                returnString = "Ancient ";
                returnString = returnString + (rank_tier - 60).ToString();
            }
            else if (rank_tier < 80)
            {
                returnString = "Divine ";
                uint tempDivine = (uint)rank_tier - 70;
                if (tempDivine == 6) { tempDivine = 5; }
                returnString = returnString + (tempDivine).ToString();
            }
            if(profileCard.Body.leaderboard_position != 0)
            {
                returnString = returnString + " Position: " + profileCard.Body.leaderboard_position.ToString();
            }
            return returnString;
        }
        
    }
}
