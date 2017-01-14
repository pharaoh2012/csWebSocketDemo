using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;

namespace csClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:5998/Echo"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Server says: " + e.Data);

                ws.Connect();
                for (int i = 0; i < 10; i++)
                {
                    ws.Send("msg:" + i);
                    System.Threading.Thread.Sleep(500);
                }
                //ws.Send("BALUS 中文");
                Console.ReadKey(true);
            }
        }
    }
}
