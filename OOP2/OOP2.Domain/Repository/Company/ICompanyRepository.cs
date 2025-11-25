using OOP2.Domain.Repository.Common;

namespace OOP2.Domain.Repository.Company
{
    public interface ICompanyRepository : IRepository<Entities.Company.Company, int>
    {
        Task<bool> CompanyNameExistsAsync(string name);
    }
}
