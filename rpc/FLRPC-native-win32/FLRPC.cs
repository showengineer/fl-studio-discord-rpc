using System;
using System.Threading;
using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;
using System.IO;
using System.Diagnostics;
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
                logLevel = settings.logLevel;
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
        public static bool Csecret;
        public static bool Psecret;
        public static void Start()
        {
            Active = true;
            // loop

            while (client != null && Active)
            {
                // Get info
                FLInfo InitInfo = GetFLInfo();

                // Try to read any keys if available
                if (Console.In.Peek() != -1)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.H:
                            Console.WriteLine("Commands: \n s: turn on secret mode \n q: Quit \n h: help \n Other settings can be changed in the settings.xml file");
                            break;
                        case ConsoleKey.S:
                            if (Csecret)
                            {
                                Csecret = false;
                                Console.WriteLine("\n Secret Mode turned off!");
                                rp.State = InitInfo.projectName;
                            }
                            else if (!Csecret)
                            {
                                Csecret = true;
                                Console.WriteLine("\n Secret Mode turned on!");
                                rp.State = settings.SecretMessage;
                            }

                            break;
                        case ConsoleKey.Q:
                            Stop();
                            break;

                    }
                }


                if (client != null)
                    client.Invoke();
                

                //Skip update if nothing changes
                if (InitInfo.appName == rp.Details && InitInfo.projectName == rp.State && Csecret == Psecret)
                    continue;
                if (InitInfo.projectName == null && rp.State == settings.NoNameMessage && Csecret == Psecret)
                    continue;

                //Check if FL Studio is active
                if (InitInfo.FLProcess == null)
                {
                    throw new Exceptions.ProcessNotPresentException("FL Studio doens't seem to be active, start FL Studio first!");
                }

                // For some stupid reason if(InitInfo.FLProcess.ProcessName != "FL64" || InitInfo.FLProcess.ProcessName != "FL") doesn't work... so we do it the inefficient way
                if (InitInfo.FLProcess.ProcessName != "FL64")
                {
                    if(InitInfo.FLProcess.ProcessName != "FL")
                    {
                        throw new Exceptions.ProcessNotPresentException("FL Studio doens't seem to be active, start FL Studio first!");
                    }
                }

                //Fill State and details
                if (InitInfo.projectName == null)
                {
                    rp.State = settings.NoNameMessage;
                    rp.Details = InitInfo.appName;
                }

                else
                {
                    rp.Details = InitInfo.appName;
                    rp.State = InitInfo.projectName;
                }
                settings.Secret = Csecret;
                if (Csecret)
                {
                    rp.State = settings.SecretMessage;
                }
                Psecret = Csecret;
                client.SetPresence(rp);
                Thread.Sleep(settings.RefeshInterval);
                
                
            }
        }
        public static void Stop()
        {
            // Proper disposal of the thread
            client.Dispose();
            Console.WriteLine("Services stopped, terminating...");

            // Make it readable
            Thread.Sleep(2000);

            // Properly exit
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
            string NoNameMessage = XMLParser.FindByTag("NoNameMessage", d)[0];
            string interval = XMLParser.FindByTag("RefreshInterval", d)[0];

            // Convert, store and return
            XmlSettings setting = new XmlSettings();
            setting.ClientID = ClientID;
            setting.Pipe = Convert.ToInt32(Pipe);
            setting.Secret = Convert.ToBoolean(Secret);
            setting.SecretMessage = SecretMessage;
            setting.NoNameMessage = NoNameMessage;
            setting.RefeshInterval = Convert.ToInt32(interval);
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
        public static int tryCount = 3;
        public static FLInfo GetFLInfo()
        {
            FLInfo i = new FLInfo();
            //Get title
            Process pr = Processes.GetMainWindowTitleByName(@"FL Studio");
            if(pr != null)
            {
                i.FLProcess = pr;
            }
            string fullTitle;
            if (Process.GetProcessesByName("FL").Length >= 1)
            {
                fullTitle = Processes.GetMainWindowsTilteByProcessName("FL");
            }
            else if(Process.GetProcessesByName("FL64").Length >= 1)
            {
                fullTitle = Processes.GetMainWindowsTilteByProcessName("FL64");
            }
            else
            {
                fullTitle = null;
            }
            // Check if project is new/unsaved
                //if yes, return null
                //if not, return name
            if(pr == null)
            {
                if(tryCount > 0)
                {
                    tryCount--;
                    Thread.Sleep(1000);
                    GetFLInfo();
                }
                i.FLProcess = null;
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
            public System.Diagnostics.Process FLProcess { get; set; }
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
            Console.WriteLine("Changes detected, Presence updated");
        }
        #endregion
    }
    
}


// 300 lines of code!