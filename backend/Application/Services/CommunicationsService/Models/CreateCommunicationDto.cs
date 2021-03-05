using Domain.Communications;

namespace Application.Services.CommunicationsService.Models
{
    public class CreateCommunicationDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public Type Type { get; set; }
    }
}