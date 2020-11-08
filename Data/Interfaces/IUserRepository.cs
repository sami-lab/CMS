using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.Data.Models.User;
using WebBuilder.ViewModel.Account;

namespace WebBuilder.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailexist(string email);
        Task<Tuple<string, IdentityResult>> Register(ApplicationUserViewModel model);
        Task<ApplicationUserViewModel> Login(LoginViewModel model);
        Task Logout();
        Task<ApplicationUserViewModel> GetDetail(string id);
        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
        Task<Tuple<string, IdentityResult>> Edit(string id, ApplicationUserViewModel model);
      
    }
}
