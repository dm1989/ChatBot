using System;
using Newtonsoft.Json.Linq;
using System.Net;
namespace TwitchBot
{
    public static class ApiModule
    {
        private static WebClient web = new WebClient();
        public static ulong TwitchAPISteamId64(string twitchname)
        {
            try
            {
                string twitchJsonString = web.DownloadString("https://api.twitch.tv/channels/" + twitchname + "?client_id=ztfhzeaaimwkryclu266ybtvrrage3");
                dynamic twitch = JObject.Parse(twitchJsonString);
                string returnString = twitch.steam_id.ToString();
                if (returnString != "") { return Convert.ToUInt64(returnString); }
                else { return 1; }
            }
            catch
            {
                return 0;
            }
        }
        public static string OpenDotaAPIAvgMMR(string steamid32)
        {
            try
            {
                string odJsonString = web.DownloadString("https://api.opendota.com/api/players/" + steamid32);
                dynamic od = JObject.Parse(odJsonString);
                string returnString = ", Average Game MMR: " + od.mmr_estimate.estimate.ToString() +" (opendota)";
                if (returnString != "") { return returnString; }
                else { return ""; }
            }
            catch
            {
                return "";
            }
        }
    }
}
