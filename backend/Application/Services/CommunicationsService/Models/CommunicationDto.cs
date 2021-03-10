using System;

namespace Application.Services.CommunicationsService.Models
{
    public class CommunicationDto
    {
        public int Id { get; set; }
        public string Record { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}