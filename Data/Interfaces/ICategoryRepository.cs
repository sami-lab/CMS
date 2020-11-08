using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Categories;

namespace WebBuilder.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<CategoriesViewModel> GetDetails();
        List<CategoriesViewModel> GetRecentCategories(int? companyId);
        List<CategoriesViewModel> GetDetailsWithCompany(int? companyId);
        CategoriesViewModel GetDetail(int categoryId);
        int Add(CategoriesViewModel model);
        int Update(int id,CategoriesViewModel model);
        bool delete(int id);
    }
}
