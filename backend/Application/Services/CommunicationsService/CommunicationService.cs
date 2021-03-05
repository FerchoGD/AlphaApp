using System.Collections.Generic;
using System.Linq;
using Application.Services.CommunicationsService.Models;
using Application.Services.Interfaces;
using Domain.Communications;

namespace Application.Services.CommunicationsService
{
    public class CommunicationService : ICommunicationsService
    {
        public List<Communication> _externalCommunications = new List<Communication>();
        public List<Communication> _internalCommunications = new List<Communication>();
        
        
        public string CreateCommunication(CreateCommunicationDto data)
        {
            var record = "";

            if (data.Type == Type.External)
            {
                record = $"CE000000{_externalCommunications.Count + 1}";
                var communication  = new Communication(1, record, data.SenderId, data.ReceiverId, data.Type);
            
                _externalCommunications.Add(communication);   
            }
            else
            {
                record = $"CI000000{_internalCommunications.Count + 1}";
                var communication  = new Communication(1, record, data.SenderId, data.ReceiverId, data.Type);
            
                _internalCommunications.Add(communication);
            }

            return record;
        }

        public CommunicationDto GetByRecord(string record)
        {
            if (record.Contains("CI"))
            {
                return _internalCommunications
                    .Where(x => x.Record == record)
                    .Select(x => new CommunicationDto
                    {
                        Id = x.Id,
                        TenantId = x.TenantId,
                        Record = x.Record,
                        SenderName = x.Sender.FullName,
                        ReceiverName = x.Receiver.FullName,
                        CreatedOn = x.CreatedOn
                    })
                    .Single();
            }
            
            return _externalCommunications
                .Where(x => x.Record == record)
                .Select(x => new CommunicationDto
                {
                    Id = x.Id,
                    TenantId = x.TenantId,
                    Record = x.Record,
                    SenderName = x.Sender.FullName,
                    ReceiverName = x.Receiver.FullName,
                    CreatedOn = x.CreatedOn
                })
                .Single();
        }
        
        public string Delete(string record)

        {
            Communication toDelete = null;
            
            if (record.Contains("CI"))
            {
                toDelete = _internalCommunications
                    .SingleOrDefault(x => x.Record == record);

                _internalCommunications.Remove(toDelete);
            }
            else
            {
                toDelete = _externalCommunications
                    .SingleOrDefault(x => x.Record == record);
                _externalCommunications.Remove(toDelete);
            }

            return "Ok";
        }
    }
}