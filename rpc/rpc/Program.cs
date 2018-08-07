/*  This is the laucher
 *  Not very special huh?
 *  
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
            FL_RPC.Init();
            //try
            //{
            //    FL_RPC.Init();
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("ERROR: " + e.Message);
            //   Environment.Exit(-1);
            //}
        }
    }
}
