using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FLRPC.Helpers
{
    class Processes
    {
        public static string GetMainWindowTitleByName(string query)
        {

            string title = null;
            Process[] p = Process.GetProcesses();
            string Title = string.Empty;
            for (var i = 0; i < p.Length; i++)
            {
                Title = p[i].MainWindowTitle;

                if (Title.Contains(query))
                    return Title;
                
            }
            return title;
        }
    }
}
