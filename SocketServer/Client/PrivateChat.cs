
using System;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common;

namespace Client
{
    class PrivateChat
    {
        public static void ClientConnection(Socket sender)
        {

            MessageStruct newmessage = new MessageStruct("privatechat"); 
            ClientStruct newclient = new ClientStruct();
            byte[] bytes = new byte[1024];
            Console.WriteLine("Socket connected to {0}",
                                sender.RemoteEndPoint.ToString()); 

            Console.WriteLine("Enter your name: ");
            newclient.username = Console.ReadLine();
            Guid id = Guid.NewGuid(); //client can be only in one conversation
            newclient.ID = id;
            Console.WriteLine("Enter the username you want to talk with him: ");
            newmessage.usertotalwith = Console.ReadLine(); 
            ClientAndMessage datatoserver = new ClientAndMessage("privatechat");
            datatoserver.username = newclient.username;
            datatoserver.ID = id;
            datatoserver.usertotalwith = newmessage.usertotalwith;
            string jsonString = JsonConvert.SerializeObject(datatoserver);
            byte[] bytetosend = Encoding.ASCII.GetBytes(jsonString);
            int bytesSent = sender.Send(bytetosend);

            var task = Task.Factory.StartNew(obj =>
            {
                    while (true)
                    {
                        int bytesRec = ((Socket)obj).Receive(bytes);
                        Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    }                   
            }, sender
            );

            Console.WriteLine("enter massage: ");
            string userInput = Console.ReadLine();
            while (userInput != "exit")
            {
                byte[] msg = Encoding.ASCII.GetBytes(userInput);
                bytesSent = sender.Send(msg);
                Console.Write("enter massage: ");
                userInput = Console.ReadLine();            
            }
        }
    }
}
