using Application.Services.CommunicationsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ICommunicationsService
    {
        string CreateCommunication(CreateCommunicationDto data);
        CommunicationDto GetByRecord(string record);
        string Delete(string record);
    }
}