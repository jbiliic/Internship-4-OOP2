using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.Application.Common.Model;
using OOP2.Application.Users.User;
using OOP2.Domain.Repository.Company;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services.Cache;

namespace OOP2.Application.Companys.Company
{
    public class CompanyReqHandler : RequestHandler<CreateCompanyReq, SuccessResponse<Domain.Entities.Company.Company>>
    {

        private readonly Domain.Services.CompanyDomainService _companyDomainService;
        private readonly Domain.Repository.Company.ICompanyRepository _companyRepository;
        private readonly IUserCacheService _cacheService;

        public CompanyReqHandler(Domain.Services.CompanyDomainService companyDomainService, ICompanyRepository companyRepository, IUserCacheService userCacheService)
        {
            _companyDomainService = companyDomainService;
            _companyRepository = companyRepository;
            _cacheService = userCacheService;
        }
        protected override Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> HandleDeleteRequestAsync(CreateCompanyReq request, Resault<SuccessResponse<Domain.Entities.Company.Company>> resault)
        {
            throw new NotImplementedException();
        }

        protected override Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> HandleGetRequestAsync(CreateCompanyReq request, Resault<SuccessResponse<Domain.Entities.Company.Company>> resault)
        {
            throw new NotImplementedException();
        }

        protected override async Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> HandlePostRequestAsync(CreateCompanyReq request, Resault<SuccessResponse<Domain.Entities.Company.Company>> resault)
        {
            var company = new Domain.Entities.Company.Company
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var valResault =  await _companyDomainService.ValidateCompanyAsync(company);
            resault.setValidationResault(valResault);
            if (resault.hasErrors)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.Company.Company>() { Value = null, IsSuccess = false });
                return resault;
            }
            await _companyRepository.InsertAsync(company);
            resault.setValue(new SuccessResponse<Domain.Entities.Company.Company>() { Id = company.Id, Value = null, IsSuccess = true });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> ExecutePostAsync(CreateCompanyReq request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.Company.Company>>();
            return await HandlePostRequestAsync(request, resault);
        }

        protected override async Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> HandlePutRequestAsync(CreateCompanyReq request, Resault<SuccessResponse<Domain.Entities.Company.Company>> resault)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id.Value);
            if (company == null) {
                resault.setValue(new SuccessResponse<Domain.Entities.Company.Company>() { Value = null, IsSuccess = false });
                return resault;
            }
            company.Name = request.Name;
            company.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow,DateTimeKind.Utc);
            company.CreatedAt = DateTime.SpecifyKind(company.CreatedAt, DateTimeKind.Utc);
            var valResault = await _companyDomainService.ValidateCompanyAsync(company);
            resault.setValidationResault(valResault);
            if (resault.hasErrors)
            {
                resault.setValue(new SuccessResponse<Domain.Entities.Company.Company>() { Value = null, IsSuccess = false });
                return resault;
            }
            await _companyRepository.UpdateAsync(company);
            resault.setValue(new SuccessResponse<Domain.Entities.Company.Company>() { Id = company.Id, Value = company, IsSuccess = true });
            return resault;
        }
        public async Task<Resault<SuccessResponse<Domain.Entities.Company.Company>>> ExecutePutAsync(CreateCompanyReq request)
        {
            var resault = new Resault<SuccessResponse<Domain.Entities.Company.Company>>();
            return await HandlePutRequestAsync(request, resault);
        }
    }
}
