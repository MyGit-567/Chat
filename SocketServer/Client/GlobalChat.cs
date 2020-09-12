
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Client
{
    class GlobalChat
    {
      public static void StartGlobalChat(Socket sender)
      {
            MessageStruct message = new MessageStruct("globalchat");
            ClientStruct client = new ClientStruct();
            byte[] bytes = new byte[1024];
            Console.WriteLine("Socket connected to {0}",
                                sender.RemoteEndPoint.ToString());

            Console.WriteLine("Enter your name: ");
            client.username = Console.ReadLine();
            Guid id = Guid.NewGuid(); //client can be only in one conversation
            client.ID = id;                     
            ClientAndMessage datatoserver = new ClientAndMessage("globalchat");
            datatoserver.username = client.username;
            datatoserver.ID = id;
            datatoserver.message = message.message;                        
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

            Console.Write("enter massage: ");
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
