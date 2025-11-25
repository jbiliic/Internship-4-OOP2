

using OOP2.Domain.Common.Validation;
using OOP2.Domain.Common.Validation.ValidationItems;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.User;

namespace OOP2.Domain.Services
{
    public class UserDomainService
    {
        private readonly IUserRepository _repo;

        public UserDomainService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ValidationResault> ValidateUserAsync(User user)
        {
            ValidationResault result = await user.ValidateBasic();

            if (await _repo.EmailExistsAsync(user.Email))
                result.AddValidationItem(ValidationItems.User.EmailExists);

            if (await _repo.UserNameExistsAsync(user.UserName))
                result.AddValidationItem(ValidationItems.User.UserNameExists);

            return result;
        }
        

    }
}
