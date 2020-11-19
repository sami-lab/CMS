using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Account;
using WebBuilder.ViewModel.Categories;
using WebBuilder.ViewModel.Companies;
using WebBuilder.ViewModel.Products;

namespace WebBuilder.ViewModel.Administration
{
    public class dashboard
    {
        public dashboard()
        {
            Users = new List<ApplicationUserViewModel>();
            ProductsGraph = new List<Graph>();
            CategoryGraph = new List<Graph>();
            recentProducts = new List<ProductsViewModel>();
            recentCategories = new List<CategoriesViewModel>();
            recentVisits = new List<UsersLocationViewModel>();
            Companies = new List<CompaniesViewModel>();
        }
        public List<ApplicationUserViewModel> Users { get; set; }
        [Display(Name = "总用户")]
        public int TotalUsers { get; set; }
        [Display(Name = "公司总数")]
        public int TotalCompanies { get; set; }
        [Display(Name = "类别总数")]
        public int TotalCategories { get; set; }
        [Display(Name = "产品总")]
        public int TotalProducts { get; set; }
        [Display(Name = "总造访")]
        public int Visits { get; set; }
        [Display(Name = "总造访")]
        public int LastMonthsVisits { get; set; }

        public List<Graph> ProductsGraph { get; set; }
        public List<Graph> CategoryGraph { get; set; }
        public List<Graph> UserStatsGraph { get; set; }
        public List<UsersLocationViewModel> recentVisits { get; set; }
        public List<ProductsViewModel> recentProducts { get; set; }
        public List<CategoriesViewModel> recentCategories { get; set; }
        public List<CompaniesViewModel> Companies { get; set; }
    }

    public class Graph
    {

        public string Day { get; set; }
        public int Total { get; set; }
    }
}
