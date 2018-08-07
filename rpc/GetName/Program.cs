using System;
using System.Threading;
using System.Diagnostics;
namespace GetName
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Process[] p = Process.GetProcesses();
                string Title = String.Empty;
                for (var i = 0; i < p.Length; i++)
                {
                    Title = p[i].MainWindowTitle;

                    if (Title.Contains(@"FL Studio"))
                        Console.WriteLine(Title);
                    
                }
                Thread.Sleep(1000);
            } while (true);
        }
    }
}
