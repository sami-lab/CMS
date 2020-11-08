using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Products;

namespace WebBuilder.Data.Interfaces
{
    public interface IProductRepository
    {
        List<ProductsViewModel> GetSpecialProducts(int companyId);
        IEnumerable<GroupByCompany> GetDetails();
        int CountProducts(int companyId);
        List<ProductsViewModel> GetDetailsWithCompany(int companyId,int skip, int limit);
        List<ProductsViewModel> GetRecentProducts(int? companyId);
        List<ProductsViewModel> GetDetailWithCategory(int categoryId, int companyId);
        Tuple<List<ProductsViewModel>, int> SearchProducts<T>(int skip, int limit, string searchList, T searchField);
        ProductsViewModel GetDetail(int id);
        int Add(ProductsViewModel model);
        int Update(int id, ProductsViewModel model);
        bool delete(int id);
    }
}
