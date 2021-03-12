using System.Collections.Generic;
using System.Linq;
using Application.Services.CommunicationsService.Models;
using Application.Services.Interfaces;
using Domain.Communications;
using Persistence.Context;

namespace Application.Services.CommunicationsService
{
    public class CommunicationService : ICommunicationsService
    {
        private readonly AlphaContext _context;

        public CommunicationService(AlphaContext context)
        {
            _context = context;
        }
        
        
        public string CreateCommunication(CreateCommunicationDto data)
        {
            var communicationForType = _context.Communications
                .Where(x => x.Type == data.Type);
            
            var record = data.Type == Type.External
                ? $"CE00000{communicationForType.Count() + 1}"
                : $"CI00000{communicationForType.Count() + 1}";

            var newCommunication = new Communication(record, data.SenderId, data.ReceiverId, data.Type);
            _context.Add(newCommunication);

            _context.SaveChanges();

            return record;
        }

        public CommunicationDto GetByRecord(string record)
        {
            return _context.Communications
                .Where(x => x.Record == record && !x.IsDeleted)
                .Select(x => new CommunicationDto
                {
                    Id = x.Id,
                    Record = x.Record,
                    SenderId = x.SenderId,
                    SenderName = x.Sender.FullName,
                    ReceiverId = x.ReceiverId,
                    ReceiverName = x.Receiver.FullName,
                    CreatedOn = x.CreatedOn
                })
                .SingleOrDefault();
        }

        public List<CommunicationDto> GetAll()
        {
            return _context.Communications
                .Where(x => !x.IsDeleted)
                .Select(x => new CommunicationDto
                {
                    Id = x.Id,
                    Record = x.Record,
                    SenderId = x.SenderId,
                    SenderName = x.Sender.FullName,
                    ReceiverId = x.ReceiverId,
                    ReceiverName = x.Receiver.FullName,
                    CreatedOn = x.CreatedOn
                })
                .ToList();
        }

        public string Delete(int id)
        {
            var communicationToDelete = _context.Communications
                .SingleOrDefault(x => x.Id == id);

            if (communicationToDelete is null) return "Not Found";

            communicationToDelete.IsDeleted = true;

            _context.SaveChanges();
            return "Ok";
        }
    }
}