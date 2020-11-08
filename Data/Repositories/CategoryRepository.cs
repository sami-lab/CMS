using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models;
using WebBuilder.ViewModel.Categories;

namespace WebBuilder.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public int Add(CategoriesViewModel model)
        {
            Catergories c = new Catergories()
            {
                // WebUtility.HtmlEncode(model.CategoryName),
                launch = DateTime.Now,
                CategoryName = model.CategoryName,
                CompanyId = model.CompanyId,
            };
            context.Categories.Add(c);
            context.SaveChanges();
            return c.id;
        }

        public bool delete(int id)
        {
            bool categories = context.Products.Any(x => x.CategoryId == id);
            if (categories) return false;
            Catergories c = new Catergories()
            {
                id = id
            };
            context.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
            return true;
        }

        public CategoriesViewModel GetDetail(int categoryId)
        {
            var Category = context.Categories.Select(x => new CategoriesViewModel()
            {
                id = x.id,
                CategoryName = x.CategoryName,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.CompanyName,
                TotalProducts = x.Products.Count
            }).FirstOrDefault(x => x.id == categoryId);
            return Category;
        }

        public List<CategoriesViewModel> GetDetails()
        {
            var Categories = context.Categories.Select(x => new CategoriesViewModel()
            {
                id = x.id,
                launch = x.launch,
                CategoryName = x.CategoryName,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.CompanyName,
                TotalProducts = x.Products.Count
            }).ToList();
            return Categories;
        }


        public List<CategoriesViewModel> GetRecentCategories(int? companyId)
        {
            var categories = new List<CategoriesViewModel>();
            if (companyId != null)
            {
                categories = context.Categories.Select(x => new CategoriesViewModel()
                {
                    id = x.id,
                    launch = x.launch,
                    CategoryName = x.CategoryName,
                    CompanyId = x.CompanyId,
                    TotalProducts = x.Products.Count
                }).Where(x => x.CompanyId == companyId).OrderByDescending(x => x.launch).Take(10).ToList();
            }
            else
            {
                categories = context.Categories.Select(x => new CategoriesViewModel()
                {
                    id = x.id,
                    launch = x.launch,
                    CategoryName = x.CategoryName,
                    CompanyId = x.CompanyId,
                    TotalProducts = x.Products.Count
                }).OrderByDescending(x => x.launch).Take(10).ToList();
            }

            return categories;
        }
        public List<CategoriesViewModel> GetDetailsWithCompany(int? companyId)
        {
            var Category = context.Categories.Where(x => x.CompanyId == companyId).Select(x => new CategoriesViewModel()
            {
                id = x.id,
                launch = x.launch,
                CategoryName = x.CategoryName,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.CompanyName,
                TotalProducts = x.Products.Count
            }).ToList();
            return Category;
        }

        public int Update(int id, CategoriesViewModel model)
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                context.Categories.Update(category);
                context.SaveChanges();
                return category.id;
            }
            return 0;
        }
    }
}
