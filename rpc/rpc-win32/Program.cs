/*  This is the laucher
 *  Not very special huh?
 */

using System;
using FLRPC;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace rpc_win32
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Running...");
            try
            {
                FL_RPC.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n \nERROR: " + e.Message);
                Console.WriteLine("INFO: Exception Name: " + e.GetType());
                Console.WriteLine("\n");
                FL_RPC.StopAndExit();
                Environment.Exit(-1);
            }
        }
    }
}
