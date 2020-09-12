using System;


namespace Common
{
   public class ClientAndMessage
    {
        public string username { get; set; }
        public Guid ID { get; set; }
        public string typeofchat { get; set; }
        public string message { get; set; }
        public string usertotalwith { get; set; }

        public ClientAndMessage(string TypeOfChat)
        {
            typeofchat = TypeOfChat;
        }

    }
}
