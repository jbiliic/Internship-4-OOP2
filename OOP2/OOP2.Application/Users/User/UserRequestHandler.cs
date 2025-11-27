using OOP2.Application.Common.Model;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.User;

namespace OOP2.Application.Users.User
{
    
    public class UserRequestHandler : RequestHandler<CreateUserRequest, SuccessResponseId>
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

        protected override Task<Resault<SuccessResponseId>> HandleDeleteRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
        {
            throw new NotImplementedException();
        }

        protected override Task<Resault<SuccessResponseId>> HandleGetRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
        {
            throw new NotImplementedException();
        }

        protected async override Task<Resault<SuccessResponseId>> HandlePostRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
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
            resault.setValue(new SuccessResponseId { Id = user.Id });

            return resault;
        }
        public async Task<Resault<SuccessResponseId>> ExecutePostAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponseId>();
            return await HandlePostRequestAsync(request, resault);
        }

        protected override Task<Resault<SuccessResponseId>> HandlePutRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
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

        protected override Task<Resault<SuccessResponseId>> HandleGetAllRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
        {
            throw new NotImplementedException();
        }
    }
}
