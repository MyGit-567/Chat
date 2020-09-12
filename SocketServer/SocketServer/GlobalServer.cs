
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SocketServer
{
    public class GlobalServer
    {
        Dictionary<Guid, Socket> allsocket = new Dictionary<Guid, Socket>();
        public void StartPublicServer(ClientAndMessage account, Socket handler)
        {
            Console.WriteLine(account.ID);
            var task = Task.Factory.StartNew(newhandler =>
            {
                try
                {
                    allsocket[account.ID] = handler;

                    while (true)
                    {
                        byte[] bytes = new Byte[1024];
                        string data = null;
                        int bytesRec = ((Socket)newhandler).Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("text received : {0}", data);
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        SendEveryone(msg);
                    }
                }
                catch (SocketException e)
                {

                    allsocket.Remove(account.ID);
                    SendEveryone(Encoding.ASCII.GetBytes("Someone want to exit"));
                }
            }, handler
            );
        }
        public void SendEveryone(byte[] bytes)
        {
            foreach (Socket user in allsocket.Values)
            {

                user.Send(bytes);
                Console.WriteLine(bytes);
            }
        }
    }
}

