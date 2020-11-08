using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Account;
using WebBuilder.ViewModel.Administration;

namespace WebBuilder.Data.Interfaces
{
    public interface IAdministrationRepository
    {
        List<IdentityRole> GetAllRoles();
        int CountUsers();
        int CountVisits(string CompanyName);
        dashboard Admin(int? companyId, string CompanyName);
        List<UsersLocationViewModel> GetUsersLocation(string CompanyName, int skip, int limit);
        List<ApplicationUserViewModel> GetAllUsers(int skip,int limit);
        Tuple<List<ApplicationUserViewModel>, int> SearchUsers<T>(int skip, int limit, string searchList, T searchField);
        Task<IdentityResult> delete(string id);//delete User
    }
}
