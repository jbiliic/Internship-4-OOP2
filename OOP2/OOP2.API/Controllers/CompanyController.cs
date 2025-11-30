using Microsoft.AspNetCore.Mvc;
using OOP2.Application.Common.Auth;
using OOP2.Application.Companys.Company;
using OOP2.Application.Users.User;

namespace OOP2.API.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyReqHandler _handler;
        private readonly AuthReqHandler _handlerAuth;
        public CompanyController(CompanyReqHandler handler, AuthReqHandler handlerAuth)
        {
            _handler = handler;
            _handlerAuth = handlerAuth;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyReq companyReq)
        {
            var res = await _handler.ExecutePostAsync(companyReq);
            if (res.Value.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCompany([FromBody] CreateCompanyReq companyReq , [FromRoute] int id)
        {
            companyReq.Id = id;
            var res = await _handler.ExecutePutAsync(companyReq);
            if (res.Value.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] int id, [FromQuery] string username , [FromQuery] string password)
        {
            var authReq = new CreateAuthReq { Username =  username , Password = password };
            var resAuth = await _handlerAuth.ExecuteAuthAsync(authReq);
            if (resAuth.Value.IsSuccess == false)
            {
                return BadRequest(resAuth);
            }
            var companyReq = new CreateCompanyReq { Id = id };
            var res = await _handler.ExecuteGetAsync(companyReq);
            if (res.Value.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany( [FromQuery] string username, [FromQuery] string password)
        {
            var authReq = new CreateAuthReq { Username = username, Password = password };
            var resAuth = await _handlerAuth.ExecuteAuthAsync(authReq);
            if (resAuth.Value.IsSuccess == false)
            {
                return BadRequest(resAuth);
            }
            var companyReq = new CreateCompanyReq { };
            var res = await _handler.ExecuteGetAllAsync(companyReq);
            if (res.Value.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyById([FromRoute] int id, [FromQuery] string username, [FromQuery] string password)
        {
            var authReq = new CreateAuthReq { Username = username, Password = password };
            var resAuth = await _handlerAuth.ExecuteAuthAsync(authReq);
            if (resAuth.Value.IsSuccess == false)
            {
                return BadRequest(resAuth);
            }
            var companyReq = new CreateCompanyReq { Id = id };
            var res = await _handler.ExecuteDeleteAsync(companyReq);
            if (res.Value.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
