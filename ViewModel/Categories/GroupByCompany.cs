using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Categories
{
    public class GroupByCompany
    {
        [Display(Name = "公司")]
        public int CompanyId { get; set; }
        [Display(Name = "公司")]
        public string CompanyName { get; set; }
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
