using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Companies;

namespace WebBuilder.Data
{
    public interface IUtilities
    {
        string ProcessPhotoproperty(IFormFile Photo, string InnerFolder);
        CompaniesViewModel GetUserCompany(string userId);
        CompaniesViewModel getCompanybyId(int id);
        CompaniesViewModel getCompanyByName(string name);
        Task<bool> IncrementAccess(string userId);
        bool RemoveImage(int imageId);
        List<countries> getCountries();
        List<states> getStates(int country_id);
        List<cities> getCities(int state_id);

        Task<string> getProfile();
    }
}
