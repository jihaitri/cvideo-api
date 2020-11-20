using AutoMapper;
using CVideoAPI.Context;
using CVideoAPI.Datasets.Account;
using CVideoAPI.Repositories;
using FirebaseAdmin.Auth;
using System;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Authen
{
    public class AuthenService : IAuthenService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public AuthenService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<AccountDataset> Login(FirebaseToken userToken, string flg)
        {
            Models.Account account = await _uow.AccountRepository.GetAccountByEmail(userToken.Claims["email"].ToString());
            if (account == null)
            {
                Models.Account newAccount = new Models.Account()
                {
                    Email = userToken.Claims["email"].ToString()
                };
                _uow.AccountRepository.Insert(newAccount);
                if (flg == CVideoConstant.Client.Employee)
                {
                    newAccount.RoleId = CVideoConstant.Roles.Employee.Id;
                    _uow.EmployeeRepository.Insert(new Models.Employee()
                    {
                        Account = newAccount,
                        FullName = userToken.Claims["name"].ToString(),
                        Avatar = userToken.Claims["picture"].ToString(),
                        DateOfBirth = DateTime.UtcNow
                    });
                }
                else if (flg == CVideoConstant.Client.Employer)
                {
                    newAccount.RoleId = CVideoConstant.Roles.Employer.Id;
                    _uow.CompanyRepository.Insert(new Models.Company()
                    {
                        Account = newAccount,
                        CompanyName = userToken.Claims["name"].ToString(),
                        Avatar = userToken.Claims["picture"].ToString()
                    });
                }
                else
                {
                    throw new ArgumentException("Flag is invalid");
                }
                if (await _uow.CommitAsync() > 0)
                {
                    account = await _uow.AccountRepository.GetFirst(filter: acc => acc.AccountId == newAccount.AccountId, includeProperties: "Role");
                }
                else
                {
                    throw new Exception("Create new account failed");
                }
            }
            else
            {
                if ((flg == CVideoConstant.Client.Employee && account.Role.RoleName != CVideoConstant.Roles.Employee.Name) ||
                    (flg == CVideoConstant.Client.Employer && account.Role.RoleName != CVideoConstant.Roles.Employer.Name) ||
                    (flg == CVideoConstant.Client.Admin && account.Role.RoleName != CVideoConstant.Roles.Admin.Name))
                {
                    return null;
                }
            }
            return _mapper.Map<AccountDataset>(account);
        }
    }
}
