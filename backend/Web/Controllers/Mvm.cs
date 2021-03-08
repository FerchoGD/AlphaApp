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
        
        [HttpGet("id")]
        public IActionResult Get(string id)
        {
            var response = _mvmService.GetByRecord(id);
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
                return BadRequest(new { message = "Username information is incorrect" });

            return Ok(response);
        }
        
        [HttpDelete("record")]
        public IActionResult Delete(string record)
        {
            var response = _mvmService.Delete(record);
            if (response == null)
                return BadRequest(new { message = "Username information is incorrect" });

            return Ok(response);
        }
    }
}