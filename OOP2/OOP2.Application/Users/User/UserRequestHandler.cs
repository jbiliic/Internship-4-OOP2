using OOP2.Application.Common.Model;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services.Cache;
namespace OOP2.Application.Users.User
{
    
    public class UserRequestHandler : RequestHandler<CreateUserRequest, SuccessResponse<Domain.Entities.User.User>>
    {
        private readonly Domain.Services.UserDomainService _userDomainService;
        private readonly Domain.Repository.User.IUserRepository _userRepository;
        private readonly IUserCacheService _cacheService;

        public UserRequestHandler(Domain.Services.UserDomainService userDomainService,IUserRepository userRepository, IUserCacheService userCacheService)
        {
            _userDomainService = userDomainService;
            _userRepository = userRepository;
            _cacheService = userCacheService;
        }

        protected override Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleDeleteRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            throw new NotImplementedException();
        }

        protected override async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleGetRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = _cacheService.Get($"db_user_{id}");
            if (user != null)
            {
                Console.WriteLine(user.FirstName);
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = user, IsSuccess = true });
                return resault;
            }
            
            user = await _userRepository.GetByIdAsync(id);
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
            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Id = user.Id , IsSuccess=true, Value = user});

            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecutePostAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandlePostRequestAsync(request, resault);
        }

        protected override async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandlePutRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false });
                return resault;
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Website = request.Website;
            user.AdressCity = request.AdressCity;
            user.AdressStreet = request.AdressStreet;
            user.CoordinateLat = request.CoordinateLat;
            user.CoordinateLng = request.CoordinateLng;
            user.IsActive = request.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            user.CreatedAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
            user.BirthDate = request.BirthDate.HasValue
                    ? DateTime.SpecifyKind(request.BirthDate.Value, DateTimeKind.Utc)
                    : null;
            var validationResault = await _userDomainService.ValidateUserAsync(user);
            resault.setValidationResault(validationResault);

            if (resault.hasErrors)
                return resault;

            await _userRepository.UpdateAsync(user);

            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = true, Id = id });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecutePutAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandlePutRequestAsync(request, resault);
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
        protected async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleActivationRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false });
                return resault;
            }
            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;
            user.CreatedAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
            user.BirthDate = user.BirthDate.HasValue
                    ? DateTime.SpecifyKind(user.BirthDate.Value, DateTimeKind.Utc)
                    : null;

            await _userRepository.UpdateAsync(user);

            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = true, Id = id });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecuteActivationAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandleActivationRequestAsync(request, resault);
        }

        protected async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleDeactivationRequestAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false });
                return resault;
            }
            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            user.CreatedAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
            user.BirthDate = user.BirthDate.HasValue
                    ? DateTime.SpecifyKind(user.BirthDate.Value, DateTimeKind.Utc)
                    : null;

            await _userRepository.UpdateAsync(user);

            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = true, Id = id });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecuteDeactivationAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandleDeactivationRequestAsync(request, resault);
        }
        protected async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleDeleteAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var id = request.Id;
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false });
                return resault;
            }
             await _userRepository.DeleteAsync(user);
            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = true, Id = id });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecuteDeleteAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandleDeleteAsync(request, resault);
        }

        protected async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> HandleImportAsync(CreateUserRequest request, Resault<SuccessResponse<Domain.Entities.User.User>> resault)
        {
            var users = await _userRepository.GetExternalUsersAsync();
            if (users == null || users.Count == 0)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = false });
                return resault;
            }
            foreach (var user in users)
            { 
                var validationResault = await _userDomainService.ValidateUserAsync(user);
                resault.setValidationResault(validationResault);
                if (resault.hasErrors)
                    continue;
                
                await _userRepository.InsertAsync(user);
                var cacheKey = $"db_user_{user.Id}";
                _cacheService.Set(cacheKey, user);
            }
            resault.setValue(new SuccessResponse<Domain.Entities.User.User> { Value = null, IsSuccess = true });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.User.User>>> ExecuteImportAsync(CreateUserRequest request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.User.User>>();
            return await HandleImportAsync(request, resault);
        }
    }
}
