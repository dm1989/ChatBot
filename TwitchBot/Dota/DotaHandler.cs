using System;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using System.Collections.Generic;

namespace TwitchBot
{
    static class DotaHandler
    {       
        static List<Channel> channels = new List<Channel>();
        static List<Channel> completedChannelRoutes = new List<Channel>();
        static Queue<Action> actionsToBeTriggered = new Queue<Action>();
        static ActionProgress actionProgress = new ActionProgress();
        public class ActionProgress
        {
            public DateTime LastAction { get; set; } = DateTime.Parse("01/01/2000 12:00:00 AM");
            public bool ActionCompleted { get; set; } = false;
        }
        private class Action
        {
            public Response response;
            public object obj;
        }
        private class SourceTvGamesCollection
        {
            public List<CSourceTVGameSmall> SourceTvGames { get; set; }
            public DateTime LastAddedTime { get; set; } = DateTime.Parse("01/01/2000 12:00:00 AM");
        }
        static SourceTvGamesCollection sourceTvGames = new SourceTvGamesCollection();
        public static void OnGCMessage(SteamGameCoordinator.MessageCallback callback)
        {
            IPacketGCMsg m = callback.Message;
            switch ((EDOTAGCMsg)callback.EMsg)
            {
                case EDOTAGCMsg.k_EMsgClientToGCGetProfileCardResponse:
                    var profileCard = new ClientGCMsgProtobuf<CMsgDOTAProfileCard>(m);
                    SubmitNextAction(Response.ProfileCard, profileCard);
                    break;
                case EDOTAGCMsg.k_EMsgClientToGCTopFriendMatchesRequest:
                    var friendMatches = new ClientGCMsgProtobuf<CMsgGCToClientTopFriendMatchesResponse>(m);
                    break;
                case EDOTAGCMsg.k_EMsgGCToClientFindTopSourceTVGamesResponse:
                    var sourceTvGames = new ClientGCMsgProtobuf<SteamKit2.GC.Dota.Internal.CMsgGCToClientFindTopSourceTVGamesResponse>(m);
                    SubmitNextAction(Response.SourceTv, sourceTvGames.Body.game_list);
                    break;
                case EDOTAGCMsg.k_EMsgGCPlayerInfo:
                    var players = new ClientGCMsgProtobuf<CMsgGCPlayerInfo>(m);
                    SubmitNextAction(Response.PlayerInfo, players);
                    break;
                case EDOTAGCMsg.k_EMsgClientToGCMatchesMinimalResponse:
                    var game = new ClientGCMsgProtobuf<CMsgClientToGCMatchesMinimalResponse>(m);
                    break;
                case (EDOTAGCMsg)24:
                    //spectateFriend = new ClientGCMsgProtobuf<CMsgSpectateFriendGameResponse>(m);
                    SteamBot.OnSpectateFriendResponse(m);
                    break;
                case (EDOTAGCMsg)4004:
                    SteamBot.OnClientWelcome(m);
                    break;
                default:
                    break;
            }
        }
        public static void TriggerRoute(Routes route, string channel, Action<string> botReturn, string argument = "")
        {
            if (argument.Equals(Program.BotDetails.TwitchNick)) { botReturn("Kappa"); return; }
            switch (route)
            {
                case Routes.PlayerRank:
                    if (!argument.Equals(""))
                    {
                        ulong profileSteamId64 = ApiModule.TwitchAPISteamId64(argument);
                        if (profileSteamId64 == 1)
                        {
                            botReturn(argument + " please link your steam to twitch");
                        }
                        else if (profileSteamId64 != 0)
                        {
                            uint steamId32 = DotaModule.ConvertSteamId32(profileSteamId64);
                            channels.Add(new Channel()
                            {
                                ChannelName = channel,
                                ChannelResponse = botReturn,
                                EndRoute = Routes.PlayerRank,
                                SteamId32 = steamId32
                            });
                            SteamBotModule.CallProfileCard(SteamBot.gameCoordinator, steamId32);
                        }
                        else { botReturn("user not found"); }
                    }
                    break;
                case Routes.NotablePlayers:
                    ulong npSteamId64 = ApiModule.TwitchAPISteamId64(channel);
                    var players = DotaModule.SourceTvProPlayers(
                                sourceTvGames.SourceTvGames,
                                DotaModule.ConvertSteamId32(npSteamId64));
                    if(players.GameFound == false)
                    {
                        botReturn("game not found");
                        break;
                    }
                    if (npSteamId64 != 1 || npSteamId64 != 0)
                    {
                        channels.Add(new Channel()
                        {
                            ChannelName = channel,
                            ChannelResponse = botReturn,
                            EndRoute = Routes.NotablePlayers,
                            Pros = players,
                            SteamId32 = DotaModule.ConvertSteamId32(npSteamId64)
                        });
                        SteamBotModule.CallPlayerInfo(
                            SteamBot.gameCoordinator,
                            players
                            );
                    }
                    break;
                case Routes.PlayerInfo:
                    ulong piSteamId64 = ApiModule.TwitchAPISteamId64(channel);
                    if (piSteamId64 != 1 || piSteamId64 != 0)
                    {
                        int playerPosition = DotaModule.FindPlayerPosition(argument);
                        if (playerPosition >= 0 && playerPosition <= 9)
                        {
                            var pros = DotaModule.SourceTvProPlayers(
                                    sourceTvGames.SourceTvGames,
                                    DotaModule.ConvertSteamId32(piSteamId64));
                            channels.Add(new Channel()
                            {
                                ChannelName = channel,
                                ChannelResponse = botReturn,
                                EndRoute = Routes.PlayerInfo,
                                Pros = pros,
                                PlayerColor = argument,
                                SteamId32 = DotaModule.ConvertSteamId32(piSteamId64)
                            });
                            SteamBotModule.CallPlayerInfo(
                                SteamBot.gameCoordinator,
                                pros
                            );
                        }
                        ulong SteamId64 = ApiModule.TwitchAPISteamId64(argument);
                        if (playerPosition == 10)
                        {
                            if (SteamId64 == 1) { botReturn(argument + " please link your steam to your twitch"); }
                            else if (SteamId64 == 0) { botReturn("twitchname or color not found"); }
                            else 
                            {
                                var player = new ProPlayers();
                                uint SteamId32 = DotaModule.ConvertSteamId32(SteamId64);
                                player.PlayerInfoRequest.Add(new CMsgGCPlayerInfoRequest.PlayerInfo() { account_id = SteamId32 });
                                channels.Add(new Channel()
                                {
                                    ChannelName = channel,
                                    ChannelResponse = botReturn,
                                    EndRoute = Routes.PlayerInfo,
                                    Pros = player,
                                    PlayerColor = "blue",
                                    SteamId32 = SteamId32
                                });
                                SteamBotModule.CallPlayerInfo(
                                    SteamBot.gameCoordinator,
                                    player
                                );
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public static void SubmitNextAction(Response response, object obj)
        {
            var action = new Action() { response = response, obj = obj };
            actionsToBeTriggered.Enqueue(action);
        }
        public static void TriggerNextAction()
        {
            if (((DateTime.Now - actionProgress.LastAction) > TimeSpan.FromSeconds(4)) || actionProgress.ActionCompleted)
            {
                if (actionsToBeTriggered.Count != 0)
                {
                    var action = actionsToBeTriggered.Dequeue();
                    NextAction(action.response, action.obj);
                    actionProgress.ActionCompleted = false;
                }
            }
        }
        public static void NextAction(Response response, object obj)
        {
            List<Channel> currentRequests = channels;
            foreach (Channel channel in channels)
            {
                if ((DateTime.Now - channel.TimeAdded) > TimeSpan.FromSeconds(30))
                {
                    RemoveWhenFinished(channel);
                }                
            }
            RemoveFinishedRoutes();
            if (response == Response.SourceTv)
            {
                if(DateTime.Now - sourceTvGames.LastAddedTime > TimeSpan.FromSeconds(15))
                {
                    sourceTvGames.SourceTvGames = new List<CSourceTVGameSmall>();
                }
                sourceTvGames.SourceTvGames.AddRange((List<CSourceTVGameSmall>)obj);
                sourceTvGames.LastAddedTime = DateTime.Now;
            }
            else if (channels.Count != 0)
            {
                try
                {
                    foreach (Channel channel in channels)
                    {
                        switch (channel.EndRoute)
                        {
                            case Routes.NotablePlayers:
                                switch (response)
                                {
                                    case Response.PlayerInfo:
                                        var players = (ClientGCMsgProtobuf<CMsgGCPlayerInfo>)obj;
                                        if (channel.Pros.PlayerInfoRequest.Exists(p => p.account_id == channel.SteamId32))
                                        {
                                            channel.ChannelResponse(DotaModule.SmallPlayerInfos(
                                                players,
                                                channel.Pros));
                                        }
                                        RemoveWhenFinished(channel);
                                        break;
                                    default:
                                        SteamBotModule.CallPlayerInfo(
                                            SteamBot.gameCoordinator,
                                            DotaModule.SourceTvProPlayers(
                                                sourceTvGames.SourceTvGames,
                                                channel.SteamId32));
                                        break;
                                }
                                break;
                            case Routes.PlayerInfo:
                                switch (response)
                                {
                                    case Response.PlayerInfo:
                                        var players = (ClientGCMsgProtobuf<CMsgGCPlayerInfo>)obj;
                                        int playerPosition = DotaModule.FindPlayerPosition(channel.PlayerColor);
                                        if (players.Body.player_infos.Count == 1) { playerPosition = 0; }
                                        if (playerPosition == 10) { channel.ChannelResponse("twitchname or color not found"); }
                                        else
                                        {
                                            var player = DotaModule.PlayerInfo(
                                                    players,
                                                    channel.PlayerColor);
                                            if (player.account_id != channel.SteamId32) { player = new CMsgGCPlayerInfo.PlayerInfo(); }
                                            if (channel.Pros.PlayerInfoRequest.Count != 0)
                                            {
                                                channel.SteamId32 = channel.Pros.PlayerInfoRequest[playerPosition].account_id;
                                                channel.PlayerInfo = DotaModule.FullGeneralPlayerInfo(player, channel.Pros.PlayerInfoRequest[playerPosition]);
                                                SteamBotModule.CallProfileCard(
                                                    SteamBot.gameCoordinator,
                                                    channel.Pros.PlayerInfoRequest[playerPosition].account_id);
                                            }
                                        }
                                        break;
                                    case Response.ProfileCard:
                                        var Profile = (ClientGCMsgProtobuf<CMsgDOTAProfileCard>)obj;
                                        if (Profile.Body.account_id == channel.SteamId32)
                                        {
                                            channel.ChannelResponse(
                                                channel.PlayerInfo + " "
                                                + DotaModule.ProfileCardRank(Profile)
                                                + ApiModule.OpenDotaAPIAvgMMR(Profile.Body.account_id.ToString()));
                                            RemoveWhenFinished(channel);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Routes.PlayerRank:
                                switch (response)
                                {
                                    case Response.PlayerInfo:
                                        var players = (ClientGCMsgProtobuf<CMsgGCPlayerInfo>)obj;
                                        if (players.Body.player_infos.Exists(p => p.account_id == channel.SteamId32))
                                        {
                                            var player = DotaModule.PlayerInfo(
                                                    players,
                                                    channel.PlayerColor);
                                            if (player == null) { channel.ChannelResponse("color not found"); }
                                            else
                                            {
                                                channel.SteamId32 = player.account_id;
                                                SteamBotModule.CallProfileCard(
                                                    SteamBot.gameCoordinator,
                                                    player.account_id);
                                            }
                                        }
                                        break;
                                    case Response.ProfileCard:
                                        var Profile = (ClientGCMsgProtobuf<CMsgDOTAProfileCard>)obj;
                                        if (Profile.Body.account_id == channel.SteamId32) { 
                                            if (channel.PlayerColor != "")
                                            {
                                                channel.ChannelResponse(
                                                    channel.PlayerInfo + " "
                                                    + DotaModule.ProfileCardRank(Profile)
                                                    + ApiModule.OpenDotaAPIAvgMMR(Profile.Body.account_id.ToString()));
                                                RemoveWhenFinished(channel);
                                            }
                                            else
                                            {
                                                channel.ChannelResponse(
                                                    DotaModule.ProfileCardRank(Profile)
                                                    + ApiModule.OpenDotaAPIAvgMMR(Profile.Body.account_id.ToString()));
                                                RemoveWhenFinished(channel);
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    RemoveFinishedRoutes();
                    System.Threading.Thread.Sleep(100);
                    actionProgress.ActionCompleted = true;
                }
                catch { }
            }
        }

        private static void RemoveFinishedRoutes()
        {
            foreach (Channel channel in completedChannelRoutes)
            {
                channels.Remove(channel);
            }
            completedChannelRoutes = new List<Channel>();
        }
        private static void RemoveWhenFinished(Channel channel)
        {
            completedChannelRoutes.Add(channel);
        }        
    }
    class Channel
    {
        public string ChannelName { get; set; }
        public Routes EndRoute { get; set; }        
        public string PlayerColor { get; set; } = "";
        public string PlayerInfo { get; set; }
        public ProPlayers Pros { get; set; } = new ProPlayers();
        public uint SteamId32 { get; set; }
        public Action<string> ChannelResponse { get; set; }
        public DateTime TimeAdded { get; } = DateTime.Now;
    }
    enum Response
    {
        SourceTv = 0,
        PlayerInfo = 1,
        ProfileCard = 2,
        Match = 3,
        FriendMatches = 4,
        SpectateFriend = 5,
        ReturnResult = 6
    }
    enum Routes
    {
        NotablePlayers = 0,
        PlayerInfo = 1,
        PlayerRank = 2
    }
}
