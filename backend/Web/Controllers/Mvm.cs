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
    }
}