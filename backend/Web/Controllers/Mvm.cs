using Application.Services.CommunicationsService.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [ApiController]
    [Route("api/mvm")]
    public class WebController : ControllerBase
    {
        private readonly ICommunicationsService _mvmService;

        public WebController(ICommunicationsService mvmService)
        {
            _mvmService = mvmService;
        }
        
        [HttpGet("{record}")]
        public IActionResult Get(string record)
        {
            var response = _mvmService.GetByRecord(record);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _mvmService.GetAll();
            if (response == null)
                return BadRequest(new { message = "Error in data" });

            return Ok(response);
        }
        
        [HttpPost("create")]
        public IActionResult Create(CreateCommunicationDto data)
        {
            var response = _mvmService.CreateCommunication(data);
            if (response == null)
                return BadRequest(new { message = "Information is incorrect" });

            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _mvmService.Delete(id);
            if (response == null)
                return BadRequest(new { message = "Information is incorrect" });

            return Ok(response);
        }
    }
}