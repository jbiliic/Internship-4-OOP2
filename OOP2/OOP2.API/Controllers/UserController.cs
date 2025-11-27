using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP2.Application.Common.Model;
using OOP2.Application.Users.User;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services;

namespace OOP2.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly UserRequestHandler _handler;
        public UserController(UserRequestHandler handler)
        {
            _handler = handler;
        }
       [HttpGet]
       public async Task<IActionResult> GetAllUsers()
        {
            var res = await _handler.ExecuteGetAllAsync(new CreateUserRequest());
            if (res.Value == null)
                return BadRequest(res);
            return Ok(res);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetUserById(int id)
        //{

        //}
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            var response = await _handler.ExecutePostAsync(userRequest);

            if (response.hasErrors)
                return BadRequest(response);

            if (!response.IsAuthorized)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
