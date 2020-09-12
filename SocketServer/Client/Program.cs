using System;
using System.Net;
using System.Net.Sockets;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrivateChat privatedata = new PrivateChat();
            Console.WriteLine("Running client socket mange");
            byte[] bytes = new byte[1024];
            IPAddress ipAddress = IPAddress.Parse("10.1.0.12");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);
            Socket sender = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);

            Console.WriteLine("Hello! Welcome to chat");
            Console.WriteLine("Choose one of the options: ");
            Console.WriteLine("1. global chat");
            Console.WriteLine("2. private chat");
            Console.WriteLine("\r\n Select an option: ");
            string userInput = Console.ReadLine();


            switch (userInput)
            {
                case "1":
                   GlobalChat.StartGlobalChat(sender);
                    break;

                case "2":
                    PrivateChat.ClientConnection(sender);
                    break;
            }
        }
    }
}
