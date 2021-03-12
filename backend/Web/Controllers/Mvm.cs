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
        
        [HttpGet("single")]
        public IActionResult Get([FromQuery] string record)
        {
            var response = _mvmService.GetByRecord(record);
            if (response == null)
                return BadRequest(new { message = "Not data" });

            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _mvmService.GetAll();
            if (response == null)
                return BadRequest(new { message = "No data" });

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
        
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var response = _mvmService.Delete(id);
            if (response == null)
                return BadRequest(new { message = "Information is incorrect" });

            return Ok(response);
        }
    }
}