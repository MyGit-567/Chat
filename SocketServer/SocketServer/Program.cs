using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common;
using Newtonsoft.Json;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {                  
            { 
            IPAddress ipAddress = IPAddress.Parse("10.1.0.12");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);
            Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            GlobalServer globalization = new GlobalServer();
                 
            PrivateServer privatechat = new PrivateServer();
                while (true)
                {
                    DataConnect data = new DataConnect();
                    Socket handler = listener.Accept();
                   // privatechat.(Encoding.ASCII.GetBytes("Client Connected!"));
                    globalization.SendEveryone(Encoding.ASCII.GetBytes("Client Connected!"));                   
                    data.socket = handler;
                    byte[] bytes = new Byte[3024];
                    int bytesRec = ((Socket)handler).Receive(bytes);
                    string bytetosend = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    ClientAndMessage account = JsonConvert.DeserializeObject<ClientAndMessage>(bytetosend);                                     
                    if (account.typeofchat == "globalchat")
                    {
                        globalization.StartPublicServer(account, handler);
                    }
                    else
                    {
                        privatechat.StartPrivateServer(account, handler);
                    }
                }
            }
        }
    }
}
