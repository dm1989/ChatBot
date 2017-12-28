using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Internal;
using System;
using System.IO;
using System.Threading;
namespace TwitchBot
{
    public class SteamBot
    {
        static SteamClient steamClient = new SteamClient();
        static CallbackManager manager = new CallbackManager(steamClient);
        static SteamUser steamUser;
        static SteamFriends steamFriends;
        public static SteamGameCoordinator gameCoordinator;
        static bool dotaResponse;
        static string authCode;
        static bool isRunning = false;        
        public SteamBot()
        {           
            steamUser = steamClient.GetHandler<SteamUser>();
            steamFriends = steamClient.GetHandler<SteamFriends>();
            gameCoordinator = steamClient.GetHandler<SteamGameCoordinator>();
            manager.Subscribe<SteamClient.ConnectedCallback>(new JobID(), OnConnected);
            manager.Subscribe<SteamUser.LoggedOnCallback>(new JobID(), OnLoggedOn);
            manager.Subscribe<SteamClient.DisconnectedCallback>(new JobID(), OnDisconected);
            manager.Subscribe<SteamUser.UpdateMachineAuthCallback>(new JobID(), UpdateMachineAuthCallBack);
            manager.Subscribe<SteamGameCoordinator.MessageCallback>(new JobID(), DotaHandler.OnGCMessage);
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
            SteamBotModule.CallIsPro(gameCoordinator, 36343070);
            dotaResponse = true;
            manager.RunWaitAllCallbacks(TimeSpan.FromSeconds(5));
            SteamBotModule.CallSourceTv(gameCoordinator);
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
                    SteamBotModule.CallSourceTv(gameCoordinator);
                    if (!dotaResponse)
                    {
                        Dota();
                    }
                    dotaResponse = false;
                }
                manager.RunWaitAllCallbacks(TimeSpan.FromMilliseconds(100));
                DotaHandler.TriggerNextAction();
                keepRunning++;
            }
        }          
        public static void OnClientWelcome(IPacketGCMsg packetMsg)
        {
            Console.WriteLine("dota ping received");
        }
        public static void OnSpectateFriendResponse(IPacketGCMsg packetMsg)
        {
            dotaResponse = true;
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
                Username = Program.BotDetails.SteamAccountName,
                Password = Program.BotDetails.SteamAccountPassword,
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
