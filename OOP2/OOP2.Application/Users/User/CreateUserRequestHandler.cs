using OOP2.Application.Common.Model;
using OOP2.Domain.Repository.User;

namespace OOP2.Application.Users.User
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressStreet { get; set; }
        public DateOnly? BirthDate { get; set; }
        public float CoordinateLng { get; set; }
        public float CoordinateLat { get; set; }
        public bool IsActive { get; set; } = true;

    }
    internal class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessResponseId>
    {
        private readonly Domain.Services.UserDomainService _userDomainService;
        private readonly Domain.Repository.User.IUserRepository _userRepository;
        public CreateUserRequestHandler(Domain.Services.UserDomainService userDomainService,IUserRepository userRepository)
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
                Password = new Guid().ToString(),
                AdressStreet = request.AdressStreet,
                BirthDate = request.BirthDate,
                CoordinateLng = request.CoordinateLng,
                CoordinateLat = request.CoordinateLat,
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

        protected override Task<Resault<SuccessResponseId>> HandlePutRequestAsync(CreateUserRequest request, Resault<SuccessResponseId> resault)
        {
            throw new NotImplementedException();
        }
    }
}
