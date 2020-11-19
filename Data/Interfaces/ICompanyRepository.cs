using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Companies;

namespace WebBuilder.Data.Interfaces
{
    public interface ICompanyRepository
    {
        List<CompaniesViewModel> GetDetails();
        CompaniesViewModel GetDetail(int id);
        string Add(CompaniesViewModel model);
        string Update(int id, CompaniesViewModel model);
        bool delete(int id);
    }
}
