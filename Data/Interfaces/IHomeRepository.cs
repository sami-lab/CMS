using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Account;
using WebBuilder.ViewModel.Home;

namespace WebBuilder.Data.Interfaces
{
    public interface IHomeRepository
    {
        List<ContactViewModel> GetDetailsWithCompany(int? companyId);
        int Add(ContactViewModel model);
        bool delete(int id);
       
    }
}
