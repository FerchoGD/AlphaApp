using System;
using Domain.Users;

namespace Domain.Communications
{
    public class Communication
    {
        private Communication()
        { }

        public Communication(string record, int senderId, int receiverId, Type type)
        {
            Record = record;
            SenderId = senderId;
            ReceiverId = receiverId;
            Type = type;
            CreatedOn = DateTimeOffset.Now;
        }
        
        public int Id { get; set; }
        public string Record { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }
        public User Receiver { get; set; }
        public int ReceiverId { get; set; }
        public Type Type { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}