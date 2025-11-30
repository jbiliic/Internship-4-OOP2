
using OOP2.Application.Common.Model;
using OOP2.Application.Users.User;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services.Cache;
namespace OOP2.Application.Common.Auth
{
    public class AuthReqHandler
    {
        private readonly Domain.Services.IAuthService _authService;
        private readonly IUserCacheService _userCacheService;
        public AuthReqHandler(Domain.Services.IAuthService authService, IUserCacheService service)
        {
            _authService = authService;
            _userCacheService = service;
        }

        
        protected async Task<Resault<SuccessResponse<CreateAuthReq>>> HandleAuthAsync(CreateAuthReq request , Resault<SuccessResponse<CreateAuthReq>> resault)
        {
            var exists = await _authService.ValidateCredentialsAsync(request.Username, request.Password);
            if (exists)
            {
                resault.setValue(new SuccessResponse<CreateAuthReq>
                {
                    IsSuccess = true,
                    Value = request
                });
                return resault;
            }
            resault.setValue(new SuccessResponse<CreateAuthReq>
            {
                IsSuccess = false,
                Value = null
            });
            return resault;
        }
        public async Task<Resault<SuccessResponse<CreateAuthReq>>> ExecuteAuthAsync(CreateAuthReq request)
        {
            var resault = new Resault<SuccessResponse<CreateAuthReq>>();
            return await HandleAuthAsync(request, resault);
        }
    }
}

