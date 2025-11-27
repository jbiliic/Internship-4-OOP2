using OOP2.Application.Common.Model;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.User;

namespace OOP2.Application.Users.User
{
    
    public class UserRequestHandler : RequestHandler<CreateUserRequest, SuccessResponse<Domain.Entities.User.User>>
    {
        private readonly Domain.Services.UserDomainService _userDomainService;
        private readonly Domain.Repository.User.IUserRepository _userRepository;
        public UserRequestHandler(Domain.Services.UserDomainService userDomainService,IUserRepository userRepository)
        {
            _userDomainService = userDomainService;
            _userRepository = userRepository;
        }


        protected override Task<bool> AuthorizeRequest(CreateUserRequest request)
        {
            return Task.FromResult(true);
        }

        protected override Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleDeleteRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            throw new NotImplementedException();
        }

        protected override async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleGetRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false }); 
                return resault;
            }
            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = user , IsSuccess = true });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecuteGetAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandleGetRequestAsync(request, resault);
        }

        protected async override Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandlePostRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var user = new Domain.Entities.User.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Website = request.Website,
                AdressCity = request.AdressCity,
                Password = Guid.NewGuid().ToString(),
                AdressStreet = request.AdressStreet,
                BirthDate = request.BirthDate,
                CoordinateLng = request.CoordinateLng,
                CoordinateLat = request.CoordinateLat,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = request.IsActive
            };
            var validationResault = await _userDomainService.ValidateUserAsync(user);
            resault.setValidationResault(validationResault);

            if ( resault.hasErrors)
                return resault;

            await _userRepository.InsertAsync(user);
            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Id = user.Id });

            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecutePostAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandlePostRequestAsync(request, resault);
        }

        protected override Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandlePutRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            throw new NotImplementedException();
        }

        protected async Task<Resault<GetAllResponse<Domain.Entities.User.User>>> HandleGetallRequestAsync(CreateUserRequest request, Resault<GetAllResponse<Domain.Entities.User.User>> resault)
        {
            var value = await _userRepository.GetAllUsersAsync();
            if (value == null)
            {
                return resault;
            }
            resault.setValue(new GetAllResponse<Domain.Entities.User.User> (value.ToList()));
            return resault;
        }
        public async Task<Resault<GetAllResponse<Domain.Entities.User.User>>> ExecuteGetAllAsync(CreateUserRequest request)
        {
            var resault = new Resault<GetAllResponse<Domain.Entities.User.User>>();
            return await HandleGetallRequestAsync(request, resault);
        }
    }
}
