using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKit2;
using System.IO;
using System.Threading;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.GC;
using SteamKit2.Internal;
using static SteamKit2.GC.Dota.Internal.CSourceTVGameSmall;
using static CMsgGCPlayerInfoRequest;
using Newtonsoft.Json.Linq;

namespace TwitchBot
{

    public class SteamBot
    {
        static List<notableMMR> noteMMR = new List<notableMMR>();
        static SteamClient steamClient = new SteamClient();
        static CallbackManager manager = new CallbackManager(steamClient);
        static SteamUser steamUser;
        static SteamFriends steamFriends;
        static SteamGameCoordinator gameCoordinator;
        static string temp = "";
        static uint gameMode;
        static bool dotaResponse;
        public static string playerInfo = "";
        static string playersMessage;
        static UInt32 tempAccountId;
        static string authCode;
        static bool isRunning = false;
        static List<Heros> heros = new List<Heros>();
        public SteamBot()
        {
            heros = Program.json.Deserialize<List<Heros>>(File.ReadAllText(@"C:\heros.json", Encoding.UTF8));
            steamUser = steamClient.GetHandler<SteamUser>();
            steamFriends = steamClient.GetHandler<SteamFriends>();
            gameCoordinator = steamClient.GetHandler<SteamGameCoordinator>();
            manager.Subscribe<SteamClient.ConnectedCallback>(new JobID(), OnConnected);
            manager.Subscribe<SteamUser.LoggedOnCallback>(new JobID(), OnLoggedOn);
            manager.Subscribe<SteamClient.DisconnectedCallback>(new JobID(), OnDisconected);
            manager.Subscribe<SteamUser.UpdateMachineAuthCallback>(new JobID(), UpdateMachineAuthCallBack);
            manager.Subscribe<SteamGameCoordinator.MessageCallback>(new JobID(), OnGCMessage);
            //connect to steam
            steamClient.Connect();
            ConnectionCallback();
            Dota();
            //start dedicated callback listener
            Thread.Sleep(5000);
            Thread callbackThread = new Thread(new ThreadStart(CallBackThread));
            callbackThread.Start();

        }

        private static void Dota()
        {
            //tell steam that we are playing dota
            var playGame = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
            playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
            {
                game_id = new GameID(570)
            });
            steamClient.Send(playGame);
            //give game coordinator time
            Thread.Sleep(5000);
            //send a dota client hello message(ensures dota client has started)
            var clientHello = new ClientGCMsgProtobuf<CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
            clientHello.Body.engine = ESourceEngine.k_ESE_Source2;
            gameCoordinator.Send(clientHello, 570);            
            manager.RunWaitCallbacks(TimeSpan.FromSeconds(15));            
            //give dota time
            Thread.Sleep(5000);
            //run a request to establish a stream
            var msg = new ClientGCMsgProtobuf<CMsgSpectateFriendGame>((uint)EDOTAGCMsg.k_EMsgGCSpectateFriendGame);
            msg.Body.steam_id = 76561197968857302;
            gameCoordinator.Send(msg, 570);
            dotaResponse = true;
            manager.RunWaitAllCallbacks(TimeSpan.FromSeconds(5));

        }


        public static void SourceTv()
        {
            NotablePlayers.notables = new List<Player>();
            var sourceTvRequest = new ClientGCMsgProtobuf<CMsgClientToGCFindTopSourceTVGames>((uint)EDOTAGCMsg.k_EMsgClientToGCFindTopSourceTVGames);
            sourceTvRequest.Body.start_game = 50;
            gameCoordinator.Send(sourceTvRequest, 570);
        }
        public static void ProfileCard(UInt32 id)
        {
            var requestProfile = new ClientGCMsgProtobuf<CMsgDOTAProfileCard>((uint)EDOTAGCMsg.k_EMsgClientToGCGetProfileCard);
            requestProfile.Body.account_id = id;
            gameCoordinator.Send(requestProfile, 570);
            tempAccountId = id;
        }
        static void ConnectionCallback()
        {
            isRunning = true;
            while (isRunning)
            {
                manager.RunWaitAllCallbacks(TimeSpan.FromMilliseconds(100));
            }
        }
        static void CallBackThread() //continously checks for responses from the server
        {            
            int keepRunning = 0;
            while (true)
            {
                if (keepRunning == 300)
                {
                    keepRunning = 0;
                    var msg = new ClientGCMsgProtobuf<CMsgSpectateFriendGame>((uint)EDOTAGCMsg.k_EMsgGCSpectateFriendGame);
                    msg.Body.steam_id = 76561197968857302;
                    gameCoordinator.Send(msg, 570);
                    if (!dotaResponse)
                    {
                        Dota();
                    }
                    dotaResponse = false;
                }
                manager.RunWaitAllCallbacks(TimeSpan.FromMilliseconds(100));
                keepRunning++;
            }
        }

        private static void OnGCMessage(SteamGameCoordinator.MessageCallback callback)
        {
            var messageMap = new Dictionary<uint, Action<IPacketGCMsg>>
            {
                {(uint)EDOTAGCMsg.k_EMsgClientToGCGetProfileCardResponse, OnProfileCardResponse },
                {(uint)EDOTAGCMsg.k_EMsgGCSpectateFriendGameResponse, OnSpectateFriendResponse },
                {(uint)EDOTAGCMsg.k_EMsgGCToClientFindTopSourceTVGamesResponse, OnTopSourceTvReSponse },         
                {(uint)EDOTAGCMsg.k_EMsgGCPlayerInfo, OnPlayerInfoResponse }
            };
            if (callback.EMsg == 4004)
            {
                OnClientWelcome(callback.Message);
            }
            else if (!messageMap.TryGetValue(callback.EMsg, out Action<IPacketGCMsg> Func)){}
            else { Func(callback.Message); }
        }
        private static void OnPlayerInfoResponse(IPacketGCMsg packetMsg)
        {
            var players = new ClientGCMsgProtobuf<CMsgGCPlayerInfo>(packetMsg);
            string[] colors = { "Blue", "Teal", "Purple", "Yellow", "Orange", "Pink", "Gray", "Light Blue", "Green", "Brown" };            
            if (playerInfo == "")
            {                
                int tempColor = 0;
                string tempHero;
                foreach (var player in players.Body.player_infos)
                {
                    if (player.is_pro)
                    {

                        try { tempHero = heros.Find(x => x.id == NotablePlayers.notables.Find(y => y.account_id == player.account_id).hero_id).localized_name; }
                        catch
                        {
                            if (gameMode != 2)
                            {
                                tempHero = colors[tempColor];
                            }
                            else { tempHero = "Not Picked"; }
                        }
                        if (tempHero == "")
                        {
                            if (gameMode != 2)
                            {
                                tempHero = colors[tempColor];
                            }
                            else { tempHero = "Not Picked"; }
                        }                        
                        if (player.team_name != "")
                        {
                            temp = temp + player.team_name + "." + player.name + " (" + tempHero + "), ";
                        }
                        else
                        {
                            temp = temp + player.name + " (" + tempHero + "), ";
                        }

                    }
                    tempColor++;
                }
                Program.sendThis(temp.Substring(0, (temp.Length - 2)));
            }
            else
            {
                string temp = "";
                if (gameMode != 2)
                {
                    for (int i = 0; i < colors.Length; i++)
                    {
                        if (colors[i].ToLower().Equals(playerInfo.ToLower()))
                        {

                            if (players.Body.player_infos[i].team_name != "") { temp = players.Body.player_infos[i].team_name + "."; }
                            if (players.Body.player_infos[i].name.Equals("") || players.Body.player_infos[i].name.Equals(" ")) { temp = temp + "No Name Found"; } else { temp = temp + players.Body.player_infos[i].name; }
                            if (players.Body.player_infos[i].is_pro) { temp = temp + ", ProAccount: Yes"; } else { temp = temp + ", ProAccount: No"; }
                            temp = temp + ", Account: " + "steamcommunity.com/profiles/" + (players.Body.player_infos[i].account_id + 76561197960265728).ToString();
                            playersMessage = temp;
                            ProfileCard(players.Body.player_infos[i].account_id);
                        }
                    }
                }
                else
                {
                    try
                    {
                        int tempHeroId = heros.Find(y => y.localized_name.ToLower().Equals(playerInfo.ToLower())).id;
                        uint lmao = (uint)tempHeroId;
                        uint tempID = NotablePlayers.notables.Find(x => x.hero_id == lmao).account_id;
                        string tempPlayer = "";
                        foreach (var player in players.Body.player_infos)
                        {
                            if (player.account_id == tempID)
                            {
                                if (player.team_name != "") { tempPlayer = player.team_name + "."; }
                                if (player.name.Equals("")) { tempPlayer = tempPlayer + "Some No Name Baddie"; } else { tempPlayer = tempPlayer + player.name; }
                                if (player.is_pro) { tempPlayer = tempPlayer + ", ProAccount: Yes"; } else { tempPlayer = tempPlayer + ", ProAccount: No"; }
                                tempPlayer = tempPlayer + ", Account: " + "steamcommunity.com/profiles/" + (player.account_id + 76561197960265728).ToString();
                                playersMessage = tempPlayer;
                                ProfileCard(player.account_id);
                            }
                        }

                    }
                    catch { }
                }
            }

        }
        private static void OnTopFriendMatchesResponse(IPacketGCMsg packetMsg)
        {
            var msg = new ClientGCMsgProtobuf<CMsgGCToClientTopFriendMatchesResponse>(packetMsg);
            foreach (CMsgDOTAMatchMinimal a in msg.Body.matches)
            {
                if (a.players.Count != 10)
                {
                    Console.WriteLine("no go");
                }
            }
            Console.WriteLine("topfriendmatchesresponsereceived");

        }
        public static class NotablePlayers
        {
            public static List<Player> notables { get; set; }
            public static void Clear()
            {
                notables = new List<Player>();
            }
        }
        private static void OnTopSourceTvReSponse(IPacketGCMsg packetMsg)
        {
            bool gameFound = false;
            var sourceTvGames = new ClientGCMsgProtobuf<CMsgGCToClientFindTopSourceTVGamesResponse>(packetMsg);
            var tempPlayers = new List<PlayerInfo>();
            foreach (var game in sourceTvGames.Body.game_list)
            {
                foreach (var player in game.players)
                {
                    if (player.account_id == 8591574)
                    {
                        gameMode = game.game_mode;
                        Console.WriteLine(game.activate_time.ToString());
                        if (gameMode != 2)
                        {
                            temp = "Average MMR [" + game.average_mmr.ToString() + "]: ";
                        }
                        else { temp = "Captains Mode: "; }
                        gameFound = true;
                        noteMMR = new List<notableMMR>();
                        foreach (var p in game.players)
                        {
                            tempPlayers.Add(new PlayerInfo() { account_id = p.account_id });
                            NotablePlayers.notables.Add(p);
                        }
                        break;
                    }
                }
            }
            if (gameFound)
            {
                var w = new ClientGCMsgProtobuf<CMsgGCPlayerInfoRequest>((uint)EDOTAGCMsg.k_EMsgGCPlayerInfoRequest);
                w.Body.player_infos = tempPlayers;
                gameCoordinator.Send(w, 570);
            }
        }

        private static void OnClientWelcome(IPacketGCMsg packetMsg)
        {
            Console.WriteLine("dota welcome message received");
        }

        private static void OnSpectateFriendResponse(IPacketGCMsg packetMsg)
        {
            dotaResponse = true;
            //Console.WriteLine("CMsgDOTARequestMatchesResponse received");
        }

        private static void OnProfileCardResponse(IPacketGCMsg packetMsg)
        {            
            var msg = new ClientGCMsgProtobuf<CMsgDOTAProfileCard>(packetMsg);
            if (msg.Body.account_id == tempAccountId)
            {
                try
                {
                    int temp = int.Parse(msg.Body.slots.Find(x => x.stat.stat_id == CMsgDOTAProfileCard.EStatID.k_eStat_SoloRank).stat.stat_score.ToString());
                    if (temp != 0)
                    {
                        if (playerInfo.Equals(""))
                        {
                            //Console.WriteLine(temp);
                            Program.sendThis(temp.ToString());
                        }
                        else
                        {
                            Program.sendThis(playersMessage + " MMR: " + temp.ToString());
                        }
                        playerInfo = "";
                    }
                }
                catch
                {
                    if (!playerInfo.Equals(""))
                    {
                        //Console.WriteLine(temp);
                        string odJSONString = Program.web.DownloadString("https://api.opendota.com/api/players/" + msg.Body.account_id);
                        dynamic od = JObject.Parse(odJSONString);
                        string mmr = od.solo_competitive_rank.ToString();
                        if (mmr.Equals(""))
                        {
                            string mmrEst = "";
                            try { mmrEst = od.mmr_estimate.estimate.ToString(); } catch { }
                            
                            if (!mmrEst.Equals(""))
                            {
                                Program.sendThis(playersMessage + " MMR: " + mmrEst + " (opendota)");
                            }
                            else
                            {
                                Program.sendThis(playersMessage);
                            }
                        }
                        else
                        {
                            Program.sendThis(playersMessage + " MMR: " + mmr + " (opendota)");
                        }
                        playerInfo = "";
                    }
                }
            }
        }

        static void OnDisconected(SteamClient.DisconnectedCallback callback)
        {
            Console.WriteLine("disconnected, reconnecting in 5...");
            Thread.Sleep(5000);
            steamClient.Connect();
        }

        static void UpdateMachineAuthCallBack(SteamUser.UpdateMachineAuthCallback callback)
        {
            byte[] sentryHash = CryptoHelper.SHAHash(callback.Data);
            File.WriteAllBytes("sentry.bin", callback.Data);
            steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                FileName = callback.FileName,
                BytesWritten = callback.BytesToWrite,
                FileSize = callback.Data.Length,
                Offset = callback.Offset,
                Result = EResult.OK,
                LastError = 0,
                OneTimePassword = callback.OneTimePassword,
                SentryFileHash = sentryHash,
                JobID = callback.JobID
            });
        }

        static void OnConnected(SteamClient.ConnectedCallback callback)
        {
            if (callback.Result != EResult.OK)
            {
                Console.WriteLine("unable to connect: {0}", callback.Result);
                isRunning = false;
                return;
            }
            Console.WriteLine("connected, logging in");
            byte[] sentryHash = null;
            if (File.Exists("sentry.bin"))
            {
                byte[] sentryFile = File.ReadAllBytes("sentry.bin");
                sentryHash = CryptoHelper.SHAHash(sentryFile);
            }
            steamUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = "account name",
                Password = "password",
                AuthCode = authCode,
                SentryFileHash = sentryHash,
            });
        }
        static void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result == EResult.AccountLogonDenied)
            {
                Console.WriteLine("cannot log in(steam guard protected): {0}", callback.Result);
                Console.WriteLine("enter auth code:");
                authCode = Console.ReadLine();
                return;
            }

            if (callback.Result != EResult.OK)
            {
                Console.WriteLine("log in not successful: {0}", callback.Result);
                isRunning = false;
                return;
            }
            Console.WriteLine("sucessfully logged in");
            isRunning = false;
        }
    }

}
