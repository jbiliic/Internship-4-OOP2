using OOP2.Domain.Common.Validation;
using OOP2.Domain.Common.Validation.ValidationItems;
using OOP2.Domain.Entities.Company;

namespace OOP2.Domain.Services
{
    public class CompanyDomainService
    {
        private readonly Repository.Company.ICompanyRepository _companyRepository;
        public CompanyDomainService(Repository.Company.ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<ValidationResault> ValidateCompanyAsync(Company company)
        {
            ValidationResault result = await company.ValidateBasic();

            if (await _companyRepository.CompanyNameExistsAsync(company.Name))
                result.AddValidationItem(ValidationItems.Company.CompanyNameUnique);

            return result;
        }
    }
}
