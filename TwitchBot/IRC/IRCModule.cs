using System;

namespace TwitchBot
{
    public static class IRCModule
    {
        public static void BotHome(string user, string msg, string channel, Action<string> botReturn)
        {
            if (user.ToLower().Equals(Program.BotDetails.TwitchNick))
            {
                string[] command = msg.Split(Char.Parse(" "));
                switch (command.Length)
                {
                    case 1:
                        switch (command[0])
                        {
                            case "!mmr":
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (command[0])
                        {
                            case "!mmr":
                                DotaHandler.TriggerRoute(Routes.PlayerRank, Program.BotDetails.TwitchNick, botReturn, command[1]);
                                break;
                            case "!np":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, command[1], botReturn);
                                break;
                            case "!notableplayers":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, command[1], botReturn);
                                break;
                            case "!pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, Program.BotDetails.TwitchNick, botReturn, command[1]);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (command[0])
                        {
                            case "!pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, command[1], botReturn, command[2]);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch (command[0])
                        {
                            case "!pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, command[1], botReturn, command[2] + " " + command[3]);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public static void DotaBotHandler(string user, string msg, string channel, Action<string> botReturn)
        {
            string[] command = msg.Split(Char.Parse(" "));            
            if (channel.Equals("thedurpdurp"))
            {
                switch (command.Length)
                {
                    case 1:
                        switch (command[0])
                        {
                            case "?mmr":
                                DotaHandler.TriggerRoute(Routes.PlayerRank, channel, botReturn, channel);
                                break;
                            case "?np":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, channel, botReturn);
                                break;
                            case "?notableplayers":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, channel, botReturn);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (command[0])
                        {
                            case "?mmr":
                                DotaHandler.TriggerRoute(Routes.PlayerRank, channel, botReturn, command[1]);
                                break;
                            case "?pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, channel, botReturn, command[1]);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (command[0])
                        {
                            case "?pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, channel, botReturn, command[1] + " " + command[2]);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (command.Length)
                {
                    case 1:
                        switch (command[0])
                        {
                            case "!commands":
                                botReturn("!np, !notableplayers, !pi [color(matchmaking only)/twitchname], !mmr/!mmr [twitchname]");
                                break;
                            case "!mmr":
                                DotaHandler.TriggerRoute(Routes.PlayerRank, channel, botReturn, channel);
                                break;
                            case "!np":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, channel, botReturn);
                                break;
                            case "!notableplayers":
                                DotaHandler.TriggerRoute(Routes.NotablePlayers, channel, botReturn);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (command[0])
                        {
                            case "!mmr":
                                DotaHandler.TriggerRoute(Routes.PlayerRank, channel, botReturn, command[1]);
                                break;
                            case "!pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, channel, botReturn, command[1]);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (command[0])
                        {
                            case "!pi":
                                DotaHandler.TriggerRoute(Routes.PlayerInfo, channel, botReturn, command[1] + " " + command[2]);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
        
    

