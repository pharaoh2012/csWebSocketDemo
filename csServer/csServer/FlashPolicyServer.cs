using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace csServer
{
    public class FlashPolicyServer
    {
        public static void Start()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(obj =>
            {
                TcpListener serverSocket;
                try
                {
                    serverSocket = new TcpListener(System.Net.IPAddress.Loopback, 843);
                    serverSocket.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return;
                }

                TcpClient clientSocket;
                Console.WriteLine("begin flash server");
                while (true)
                {

                    clientSocket = serverSocket.AcceptTcpClient();
                    NetworkStream clientStream = clientSocket.GetStream();

                    var message = new byte[4096];
                    int bytesRead;

                    try
                    {
                        bytesRead = clientStream.Read(message, 0, 4096);
                    }
                    catch
                    {
                        break;
                    }
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    var encoder = new ASCIIEncoding();
                    var request = encoder.GetString(message, 0, bytesRead);

                    Console.WriteLine("Flash:" + request);
                    if (request != "<policy-file-request/>\0")
                    {
                        break;
                    }

                    string xml = @"<?xml version=""1.0""?>
<cross-domain-policy xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                     xsi:noNamespaceSchemaLocation=""http://www.adobe.com/xml/schemas/PolicyFileSocket.xsd"">
  <site-control permitted-cross-domain-policies=""all"" />
  <allow-access-from domain=""*"" to-ports=""*"" secure=""false"" />
</cross-domain-policy>";

                    var reply = encoder.GetBytes(xml + "\0");
                    clientStream.Write(reply, 0, reply.Count());
                    clientStream.Flush();

                    clientSocket.Close();

                }
            }); 
        }
    }
}
