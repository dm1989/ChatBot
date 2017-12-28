using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using System;
namespace TwitchBot
{
    public class SteamBotModule
    {        
        public static void CallSourceTv(SteamGameCoordinator gc)
        {
            var sourceTvRequest = new ClientGCMsgProtobuf<CMsgClientToGCFindTopSourceTVGames>((uint)EDOTAGCMsg.k_EMsgClientToGCFindTopSourceTVGames);
            sourceTvRequest.Body.start_game = 50;
            gc.Send(sourceTvRequest, 570);
        }
        public static void CallPlayerInfo(SteamGameCoordinator gc, ProPlayers proPlayers)
        {
            var w = new ClientGCMsgProtobuf<CMsgGCPlayerInfoRequest>((uint)EDOTAGCMsg.k_EMsgGCPlayerInfoRequest);
            w.Body.player_infos = proPlayers.PlayerInfoRequest;
            gc.Send(w, 570);
        }
        public static void CallProfileCard(SteamGameCoordinator gc, UInt32 steamId32)
        {
            var requestProfileCard = new ClientGCMsgProtobuf<CMsgDOTAProfileCard>((uint)EDOTAGCMsg.k_EMsgClientToGCGetProfileCard);
            requestProfileCard.Body.account_id = steamId32;
            gc.Send(requestProfileCard, 570);
        }        
        public static void CallIsPro(SteamGameCoordinator gc, UInt32 steamId32)
        {
            var msg = new ClientGCMsgProtobuf<CMsgGCIsProQuery>((uint)EDOTAGCMsg.k_EMsgGCIsProQuery);
            msg.Body.account_id = 36343070;
            gc.Send(msg, 570);
        }
    }
}
