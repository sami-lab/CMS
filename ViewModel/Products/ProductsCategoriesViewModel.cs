using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Companies;

namespace WebBuilder.ViewModel.Products
{
    public class ProductsCategoriesViewModel
    {
        public CompaniesViewModel company { get; set; }
        public IEnumerable<Categories.CategoriesViewModel> Categories { get; set; }
        public IEnumerable<ProductsViewModel> SpecialProducts { get; set; }
        public IEnumerable<ProductsViewModel> Products { get; set; }
    }
}
