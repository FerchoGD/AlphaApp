using System.Collections.Generic;
using Application.Services.CommunicationsService.Models;

namespace Application.Services.Interfaces
{
    public interface ICommunicationsService
    {
        string CreateCommunication(CreateCommunicationDto data);
        CommunicationDto GetByRecord(string record);
        List<CommunicationDto> GetAll();
        string Delete(string record);
    }
}