using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Products
{
    public class GroupByCompany
    {
        [Display(Name = "ID")]
        public int CompanyId { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        public IEnumerable<ProductsViewModel> Products { get; set; }
        [Display(Name = "Total Products")]
        public int TotalProducts { get; set; }
    }
}
