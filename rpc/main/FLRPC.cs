using System;
using System.Text;
using System.Threading;
using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;
using System.IO;
using FLRPC.Helpers;
namespace FLRPC
{
    public static class FL_RPC
    {

        #region Presets
        // States if everyting is active
        public static bool Active = true;

        // Init the standard layout
        private static RichPresence rp = new RichPresence()
        {
            Details = "FL Studio <version>",
            State = "Looking at a empty project!",
            Assets = new Assets()
            {
                LargeImageKey = "fl12_logo",
                LargeImageText = "FL-RPC",
            }
        };
        private static LogLevel logLevel = LogLevel.None;
        private static DiscordRpcClient client;
        private static XmlSettings settings;
        #endregion

        #region Public methods
        /// <summary>
        /// Initalizes an RPC client instance and starts it
        /// </summary>
        public static void Init()
        {
            settings = ReadSettings();
            string ClientID = settings.ClientID;
            int DPipe = settings.Pipe;
            using(client = new DiscordRpcClient(ClientID, false, DPipe))
            {
                //Set log levels
                client.Logger = new ConsoleLogger() { Level = logLevel, Coloured = true };

                //Event registration
                client.OnReady += OnReady;
                client.OnClose += OnClose;
                client.OnError += OnError;

                client.OnConnectionEstablished += OnConnectionEstablished;
                client.OnConnectionFailed += OnConnectionFailed;

                client.OnPresenceUpdate += OnPresenceUpdate;

                //Set additional rp info
                rp.Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow
                };

                //'link' rp with client
                client.SetPresence(rp);

                //Initilize the client and return
                client.Initialize();
                Start();
            }
        }
        public static bool secret;
        public static void Start()
        {
            Active = true;
            // loop

            while (client != null && Active)
            {
                if (client != null)
                    client.Invoke();
                // Get info
                FLInfo InitInfo = GetFLInfo();
                if(InitInfo.appName == null && InitInfo.projectName == null)
                {
                    throw new Exceptions.ProcessNotPresentException("FL Studio doens't seem to be active, start FL Studio first!");
                }
                if (InitInfo.projectName == null)
                {
                    rp.State = "Looking at a empty project!";
                    rp.Details = InitInfo.appName;
                }

                else
                {
                    rp.Details = InitInfo.appName;
                    rp.State = InitInfo.projectName;
                }
                settings.Secret = secret;
                if (secret)
                {
                    rp.State = settings.SecretMessage;
                } 
                // Try to read any keys if available
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.H:
                            Console.WriteLine("Commands: \n s: turn on secret mode \n q: Quit \n h: help \n Other settings can be changed in the settings.xml file");
                            break;
                        case ConsoleKey.S:
                            if (secret)
                            {
                                secret = false;
                                Console.WriteLine("\n Secret Mode turned off!");
                                rp.State = InitInfo.projectName;
                            }
                            else if (!secret)
                            {
                                secret = true;
                                Console.WriteLine("\n Secret Mode turned on!");
                                rp.State = settings.SecretMessage;
                            }
                            
                            break;
                        case ConsoleKey.Q:
                            Stop(client);
                            break;

                    }
                }

                //This can be what ever value you want, as long as it is faster than 30 seconds.
                //Console.Write("+");
                Thread.Sleep(5000);
                
                client.SetPresence(rp);
            }
        }
        public static void Stop(DiscordRpcClient client)
        {
            client.Dispose();
            Console.WriteLine("Services stopped, terminating...");
            Thread.Sleep(750);
            Environment.Exit(0);
        }
        #endregion
        #region Private methods
        private static XmlSettings ReadSettings()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The settings file was not found on it's desired location!");
            }
            // Get all settings as strings
            System.Xml.XmlDocument d = XMLParser.LoadDocument(path);
            string ClientID = XMLParser.FindByTag("ClientID", d)[0];
            string Pipe = XMLParser.FindByTag("Pipe", d)[0];
            string Secret = XMLParser.FindByTag("SecretProject", d)[0];
            string DebugLevel = XMLParser.FindByTag("DebugLevel", d)[0];
            string SecretMessage = XMLParser.FindByTag("SecretMessage", d)[0];

            // Convert, store and return
            XmlSettings setting = new XmlSettings();
            setting.ClientID = ClientID;
            setting.Pipe = Convert.ToInt32(Pipe);
            setting.Secret = Convert.ToBoolean(Secret);
            setting.SecretMessage = SecretMessage;
            switch (Convert.ToInt32(DebugLevel))
            {
                case 0:
                    setting.logLevel = LogLevel.Info;
                    break;
                case 1:
                    setting.logLevel = LogLevel.Warning;
                    break;
                case 2:
                    setting.logLevel = LogLevel.Error;
                    break;
                case 3:
                    setting.logLevel = LogLevel.None;
                    break;
            }
            return setting;
        }

        public static FLInfo GetFLInfo()
        {
            FLInfo i = new FLInfo();
            //Get title
            string fullTitle = Processes.GetMainWindowTitleByName(@"FL Studio");

            // Check if project is new/unsaved
                //if yes, return null
                //if not, return name
            if(fullTitle == null)
            {
                i.projectName = null;
                i.appName = null;
            }
            else if (!fullTitle.Contains("-"))
            {
                i.projectName = null;
                i.appName = fullTitle;
            }
                
            else
            {
                string[] splitbyminus = fullTitle.Split('-');
                i.projectName = splitbyminus[0];
                i.appName = splitbyminus[1];
            }
            //return
            return i;

        }
        public struct FLInfo
        {
            public string appName { get; set; }
            public string projectName { get; set; }
        }
        #endregion
        #region Events
        private static void OnReady(object sender, ReadyMessage args)
        {
            //This is called when we are all ready to start receiving and sending discord events. 
            // It will give us some basic information about discord to use in the future.

            //It can be a good idea to send a inital presence update on this event too, just to setup the inital game state.
            Console.WriteLine("On Ready. RPC Version: {0}", args.Version);

        }
        private static void OnClose(object sender, CloseMessage args)
        {
            //This is called when our client has closed. The client can no longer send or receive events after this message.
            // Connection will automatically try to re-establish and another OnReady will be called (unless it was disposed).
            Console.WriteLine("Lost Connection with client because of '{0}'", args.Reason);
        }
        private static void OnError(object sender, ErrorMessage args)
        {
            //Some error has occured from one of our messages. Could be a malformed presence for example.
            // Discord will give us one of these events and its upto us to handle it
            Console.WriteLine("Error occured within discord. ({1}) {0}", args.Message, args.Code);
        }
        private static void OnConnectionEstablished(object sender, ConnectionEstablishedMessage args)
        {
            //This is called when a pipe connection is established. The connection is not ready yet, but we have at least found a valid pipe.
            Console.WriteLine("Pipe Connection Established. Valid on pipe #{0}", args.ConnectedPipe);
        }
        private static void OnConnectionFailed(object sender, ConnectionFailedMessage args)
        {
            //This is called when the client fails to establish a connection to discord. 
            // It can be assumed that Discord is unavailable on the supplied pipe.
            Console.WriteLine("Pipe Connection Failed. Could not connect to pipe #{0}", args.FailedPipe);
            Active = false;
        }
        private static void OnPresenceUpdate(object sender, PresenceMessage args)
        {
            //This is called when the Rich Presence has been updated in the discord client.
            // Use this to keep track of the rich presence and validate that it has been sent correctly.
           
        }
        #endregion
    }
    
}
