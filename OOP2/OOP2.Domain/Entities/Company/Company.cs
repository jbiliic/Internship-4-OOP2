using OOP2.Domain.Common.Entity;
using OOP2.Domain.Common.Validation;
using OOP2.Domain.Common.Validation.ValidationItems;

namespace OOP2.Domain.Entities.Company
{
    public class Company : BaseEntity
    {
        public const int NameMaxLen = 200;
        public string Name { get; set; }


        public async Task<ValidationResault> ValidateBasic() { 
            var resault = new ValidationResault();
            if (Name.Length > NameMaxLen)
                resault.AddValidationItem(
                    ValidationItems.Company.CompanyNameLen
                    );
            return resault;
        }
    }
}
