using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services.CommunicationsService.Models;
using Application.Services.Interfaces;
using Domain.Communications;
using Persistence.Context;
using Type = Domain.Communications.Type;

namespace Application.Services.CommunicationsService
{
    public class CommunicationService : ICommunicationsService
    {
        private readonly AlphaContext _context;

        public CommunicationService(AlphaContext context)
        {
            _context = context;
        }
        
        [Serializable]
        class InvalidSenderOrReceiverException : Exception
        {
            public InvalidSenderOrReceiverException()
            { }

            public InvalidSenderOrReceiverException(int sender, int receiver)
                : base(String.Format($"Invalid IDs: Sender {sender}, Receiver {receiver}"))
            { }
        }
        
        
        public string CreateCommunication(CreateCommunicationDto data)
        {
            try
            {
                if (data.SenderId < 1 || data.ReceiverId < 1)
                    throw new InvalidSenderOrReceiverException(data.SenderId, data.ReceiverId);
                
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
            catch(InvalidSenderOrReceiverException exception)
            {
                return exception.Message;
            }
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

            try
            {
                var communicationToDelete = _context.Communications
                    .SingleOrDefault(x => x.Id == id);

                if (communicationToDelete is null) return "Not Found";

                communicationToDelete.IsDeleted = true;

                _context.SaveChanges();
                return "Ok";
            }
            catch
            {
                return null;
            }
            
        }
    }
}