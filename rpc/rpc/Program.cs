/*  This is the laucher
 *  Not very special huh?
 */ 

using System;
using FLRPC;
using DiscordRPC;
namespace rpc
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
            catch(Exception e)
            {
                Console.WriteLine("\n \nERROR: " + e.Message);
                Console.WriteLine("INFO: Exception Name: " + e.GetType());
                Console.WriteLine("\n Exiting...");
                Environment.Exit(-1);
            }
        }
    }
}
