using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using IWshRuntimeLibrary;

namespace ShortcutHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PrintIntro();
                GetInfo();
                GetFLPaths();
                bool go = Review();
                if (go)
                {
                    Execute();
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                if (!go)
                {
                    Console.WriteLine("Operation cancelled");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(-1);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(string.Format("ERR: {0}", e.Message));
               Environment.Exit(-1);
            }
        }
        static void PrintIntro()
        {
            Console.WriteLine("This program will create a shortcut that will launch both FL Studio and FL-RPC for Discord");
            Console.WriteLine("Written by HugoPilot (MusicalProgrammer) - Licensed under GNU GPL v3");
            Console.WriteLine("Check out my other projects at https://github.com/hugopilot!");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("\n");
        }
        static bool ReplaceDesktop;
        static string[] WriteShortcuts = null;
        static string FLStudioPaths;
        static void GetInfo()
        {
            Console.WriteLine("Would you like to replace your current shortcuts on your desktop? (Y/N)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    ReplaceDesktop = true;
                    break;
                case ConsoleKey.N:
                    ReplaceDesktop = false;
                    break;
                default:
                    ReplaceDesktop = false;
                    break;
            }
            Console.Write("\n Where would you like to write (additonal) shortcuts? Write the full path to the folder, seperate folder paths with a comma (,):");
            string feed = Console.ReadLine();
            if (feed != null)
            {
                WriteShortcuts = feed.Split(',');
            }
            Console.WriteLine("Thank you! \n \n");
        }
        static void GetFLPaths()
        {
            string path = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Image-Line\\Shared\\Paths", "FL Studio", null);
            FLStudioPaths = path;
            Console.WriteLine(string.Format("Found FL Studio at path: {0}", path));
        }
        static bool Review()
        {
            Console.WriteLine("\n Please review these settings: \n");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Shortcuts:");
            Console.WriteLine(string.Format("   Replace desktop shortcuts: {0} \n", ReplaceDesktop.ToString()));
            if(WriteShortcuts == null)
            {
                Console.WriteLine(string.Format("   Don't write additional shortcuts"));
            }
            else
            {
                Console.WriteLine(string.Format("   Write additional shortcuts to: \n"));
                foreach(string p in WriteShortcuts)
                {
                    Console.WriteLine(string.Format("       {0}", p));
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("FL Studio versions:\n");
            Console.WriteLine(string.Format("   Detected path: {0} \n", FLStudioPaths));
            Console.WriteLine("-------------------------------------------------------------------------------\n");
            Console.WriteLine("Are these settings OK? (Y/N)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                    return false;
            }
            return false;
        }
        static string FolderExe = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        static string IconPath()
        {
            
            return Path.Combine(FolderExe, "ICONS\\FL.ico");
        }
        static void Execute()
        {
            string batchpath = Path.Combine(FolderExe, "SHORTCUT.bat");
            CreateBatch(FLStudioPaths, batchpath);
                if (ReplaceDesktop)
                {
                CreateShortcut("FL Studio", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), batchpath, IconPath());
                }
                if (WriteShortcuts != null)
                {
                    foreach (string p in WriteShortcuts)
                    {
                        string realP = p.Replace(@"""", string.Empty);
                        CreateShortcut("FL Studio", realP , batchpath, IconPath());
                    }
                }
            Console.WriteLine("\n\n Done, Thank you!");
        }
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string icopath)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "FL Studio";   // The description of the shortcut
            shortcut.IconLocation = icopath;           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }
        public static void CreateBatch(string pathToFL, string outputPath)
        {
            string[] lines = new string[] { "@echo off", "ECHO Lauching FL Studio", string.Empty, "REM Change this if FL Studio was installed elsewhere", @"start """" " + string.Format(@"""{0}""", pathToFL), string.Empty, "ECHO FL Studio launched", "ECHO.", "ECHO Cancel if RPC shouldn't be enabled", "TIMEOUT /T 10 /NOBREAK", "rpc-win32" };
            System.IO.File.WriteAllLines(outputPath, lines);
        }
    }
}
