using Microsoft.AspNetCore.Mvc;
using OOP2.Application.Companys.Company;
using OOP2.Application.Users.User;

namespace OOP2.API.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyReqHandler _handler;
        public CompanyController(CompanyReqHandler handler)
        {
            _handler = handler;
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
    }
}
