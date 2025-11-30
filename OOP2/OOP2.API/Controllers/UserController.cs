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
            return Ok(res.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int  id)
        {
            var res = await _handler.ExecuteGetAsync(new CreateUserRequest { Id = id });

            
            if (res.Value.IsSuccess == false)
                return BadRequest(res);

            return Ok(res.Value);

        }
        [HttpPost] 
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            var res = await _handler.ExecutePostAsync(userRequest);

            if (res.hasErrors)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] int id , [FromBody] CreateUserRequest userRequest)
        {
            userRequest.Id = id;
            var res = await _handler.ExecutePutAsync(userRequest);

            if (res.hasErrors)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var res = await _handler.ExecuteDeleteAsync( new CreateUserRequest() { Id = id });
            if (res.Value.IsSuccess == false)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUser([FromRoute] int id)
        {
            var res = await _handler.ExecuteActivationAsync(new CreateUserRequest() {Id = id });
            if(res.Value.IsSuccess == false)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser([FromRoute] int id)
        {
            var res = await _handler.ExecuteDeactivationAsync(new CreateUserRequest() { Id = id });
            if (res.Value.IsSuccess == false)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportUser()
        {
            var res = await _handler.ExecuteImportAsync(new CreateUserRequest() { });
            if (res.Value.IsSuccess == false)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
