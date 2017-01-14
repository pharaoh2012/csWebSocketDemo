using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp.Server;

namespace csServer
{
    class Program
    {
        static void Main(string[] args)
        {
    var wssv = new WebSocketServer("ws://localhost:5998");
    wssv.AddWebSocketService<EchoServer>("/Echo");
    wssv.Start();
    Console.ReadLine();
    wssv.Stop();
        }
    }
}
