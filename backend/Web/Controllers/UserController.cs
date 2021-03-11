using Application.Services.Interfaces;
using Application.Services.UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateDto data)
        {
            var response = _userService.Authenticate(data.Email, data.Password);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _userService.GetById(id);
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        [HttpPost("create")]
        public IActionResult Create(NewUserDto data)
        {
            var response = _userService.Create(data);
            if (response == null)
                return BadRequest(new { message = "Username information is incorrect" });

            return Ok(response);
        }
    }
}