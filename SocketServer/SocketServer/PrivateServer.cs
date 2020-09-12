
using Common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class PrivateServer
    {
        public Dictionary<string, Socket> allsocket;
        public PrivateServer()
        {
            allsocket = new Dictionary<string, Socket>();
        }
        
        public void StartPrivateServer(ClientAndMessage account, Socket handler)
        {          
            Console.WriteLine(account.username);            
            var task = Task.Factory.StartNew(newhandler =>
            {
                if (!allsocket.ContainsKey(account.username) && allsocket.Count < 2)
                {
                    allsocket.Add(account.username, handler);
                }

                if (allsocket.ContainsKey(account.usertotalwith))
                {
                    string data = "Client Connected";
                    Console.WriteLine(data);
                    byte[] msg = Encoding.ASCII.GetBytes(data);
                    allsocket[account.usertotalwith].Send(msg);
                }

                while (true)
                {                                    
                        byte[] bytes = new Byte[1024];
                        string data = "";
                        int bytesRec = ((Socket)newhandler).Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("text received : {0}", data);
                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        allsocket[account.username].Send(msg);
                        if (allsocket.ContainsKey(account.usertotalwith))
                        {
                            allsocket[account.usertotalwith].Send(msg);
                        }                   
                }
            }, handler
            );          
        }
    }
}
