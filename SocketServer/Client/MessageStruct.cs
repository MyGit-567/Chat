
namespace Client
{
    class MessageStruct
    {
        public string typeofchat { get; set; }
        public string message { get; set; }
        public string usertotalwith { get; set; }

        public MessageStruct(string TypeOfChat)
        {
            typeofchat = TypeOfChat;
        }
    }
}
