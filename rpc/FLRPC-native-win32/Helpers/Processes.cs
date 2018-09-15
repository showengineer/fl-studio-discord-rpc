using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FLRPC.Helpers
{
    class Processes
    {
        public static Process GetMainWindowTitleByName(string query)
        {

            Process title = null;
            Process[] p = Process.GetProcesses();
            string Title = string.Empty;
            for (var i = 0; i < p.Length; i++)
            {
                Title = p[i].MainWindowTitle;

                if (Title.Contains(query))
                    return p[i];
                
            }
            return title;
        }

        /// <summary>
        /// Finds the Main Window Tilte by searching the corrosponding exe
        /// </summary>
        /// <param name="exeName">Name of the exe</param>
        /// <returns>MainWindowTitle</returns>
        public static string GetMainWindowsTilteByProcessName(string exeName)
        {
            Process[] AP = Process.GetProcessesByName(exeName);

            //does nothing, for debug
            string hold = null;
            // Return first one. Since the end-user probably only runs one instance of FL.exe/FL64.exe. RPC can also detect only one version.
            if(AP.Length < 1)
            {
                return null;
            }
            return AP[0].MainWindowTitle;
        }
    }
}
