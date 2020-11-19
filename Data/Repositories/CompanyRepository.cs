using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models;
using WebBuilder.ViewModel.Categories;
using WebBuilder.ViewModel.Companies;
using WebBuilder.ViewModel.Products;

namespace WebBuilder.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext context;
        public readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUtilities utilities;
        public CompanyRepository(ApplicationDbContext _context, IHostingEnvironment _hostingEnvironment, IHttpContextAccessor _httpContextAccessor, IUtilities _utilities)
        {
            context = _context;
            httpContextAccessor = _httpContextAccessor;
            hostingEnvironment = _hostingEnvironment;
            utilities = _utilities;
        }

        public string Add(CompaniesViewModel model)
        {
            Companies company = new Companies()
            {
                Date = DateTime.Now,
                CompanyTitle = model.CompanyTitle,
                CompanyName = model.CompanyName,
                CompanyDesc = model.CompanyDesc,
                CompanyLogo = utilities.ProcessPhotoproperty(model.CompanyLogo, "Companies"),
                CompanyBackgorund = utilities.ProcessPhotoproperty(model.CompanyBackgorund, "Companies"),
                whyCooseUsImage = utilities.ProcessPhotoproperty(model.whyCooseUsImage, "Companies"),
                whyCooseUsText = model.whyCooseUsText,
                CompanyEmail = model.CompanyEmail,
                CompanyPhone = model.CompanyPhone,
                CompanyAdd = model.CompanyAdd,
                CompanyOwner = model.CompanyOwner,
                linkedinProfile = model.linkedinProfile,
                FbProfile = model.FbProfile,
                twitterProfile = model.twitterProfile,
            };
            context.Companies.Add(company);
            context.SaveChanges();
            return company.CompanyName;
        }

        public bool delete(int id)
        {
            Companies b = new Companies()
            {
                id = id
            };
            context.Entry(b).State = EntityState.Deleted;
            context.SaveChanges();
            return true;
        }

        public CompaniesViewModel GetDetail(int id)
        {

            var company = context.Companies.Include(x => x.Products).Include(x => x.Catergories).Select(x => new CompaniesViewModel()
            {
                id = x.id,
                Date = x.Date,
                CompanyTitle = x.CompanyTitle,
                CompanyName = x.CompanyName,
                CompanyDesc = x.CompanyDesc,
                CompanyLogoPath = x.CompanyLogo,
                CompanyBackgorundPath = x.CompanyBackgorund,
                whyCooseUsImagePath = x.whyCooseUsImage,
                whyCooseUsText = x.whyCooseUsText,
                CompanyEmail = x.CompanyEmail,
                CompanyPhone = x.CompanyPhone,
                CompanyAdd = x.CompanyAdd,
                CompanyOwner = x.CompanyOwner,
                CompanyOwnerName = x.User.Name,
                TotalProducts = x.Products.Count,
                TotalCategories = x.Catergories.Count,
                linkedinProfile = x.linkedinProfile,
                FbProfile = x.FbProfile,
                twitterProfile = x.twitterProfile,
                Products = x.Products.Select(p => new ProductsViewModel()
                {
                    id = p.id,
                    CategoryId = p.CategoryId,
                    CompanyId = p.CompanyId,
                    ImageName = p.Images.FirstOrDefault().Image_Path,
                    title = p.title,
                    isSpecial= p.isSpecial
                }).Take(12).ToList(),
                SpecialProducts = x.Products.Select(p => new ProductsViewModel()
                {
                    id = p.id,
                    CategoryId = p.CategoryId,
                    CompanyId = p.CompanyId,
                    ImageName = p.Images.FirstOrDefault().Image_Path,
                    title = p.title,
                    isSpecial= p.isSpecial
                }).Where(p => p.isSpecial).Take(9).ToList(),
                Categories = x.Catergories.Select(c => new CategoriesViewModel()
                {
                    id = c.id,
                    CategoryName = c.CategoryName,
                    TotalProducts = c.Products.Count,
                    CompanyId = c.CompanyId
                }).ToList(),
            }).FirstOrDefault(x => x.id == id);

            return company;
        }

        public List<CompaniesViewModel> GetDetails()
        {
            var companies = context.Companies.Select(x => new CompaniesViewModel()
            {
                Date = x.Date,
                id = x.id,
                CompanyTitle = x.CompanyTitle,
                CompanyName = x.CompanyName,
                CompanyDesc = x.CompanyDesc,
                CompanyLogoPath = x.CompanyLogo,
                CompanyBackgorundPath = x.CompanyBackgorund,
                whyCooseUsImagePath = x.whyCooseUsImage,
                whyCooseUsText = x.whyCooseUsText,
                CompanyEmail = x.CompanyEmail,
                CompanyPhone = x.CompanyPhone,
                CompanyAdd = x.CompanyAdd,
                CompanyOwner = x.CompanyOwner,
                linkedinProfile = x.linkedinProfile,
                FbProfile = x.FbProfile,
                twitterProfile = x.twitterProfile,
                CompanyOwnerName = x.User.Name,
                TotalProducts = x.Products.Count,
                TotalCategories = x.Catergories.Count,
            }).ToList();
            return companies;
        }

        public string Update(int id, CompaniesViewModel model)
        {
            var company = context.Companies.Find(id);
            if (!httpContextAccessor.HttpContext.User.IsInRole("Super Admin"))
            {
                string loggedInAdminId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (loggedInAdminId != company.CompanyOwner)
                {
                    return null;
                }
            }
            if (company == null) return null;
            company.CompanyTitle = model.CompanyTitle;
            company.CompanyName = model.CompanyName;
            company.CompanyDesc = model.CompanyDesc;
            if (model.CompanyLogo != null)
            {
                if (company.CompanyLogo != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Image", "Companies", company.CompanyLogo);
                    System.IO.File.Delete(filepath);
                }
                company.CompanyLogo = utilities.ProcessPhotoproperty(model.CompanyLogo, "Companies");
            }
            if (model.whyCooseUsImage != null)
            {
                if (company.whyCooseUsImage != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Image", "Companies", company.whyCooseUsImage);
                    System.IO.File.Delete(filepath);
                }
                company.whyCooseUsImage = utilities.ProcessPhotoproperty(model.whyCooseUsImage, "Companies");
            }
            if (model.CompanyBackgorund != null)
            {
                if (company.CompanyBackgorund != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Image", "Companies", company.CompanyBackgorund);
                    System.IO.File.Delete(filepath);
                }
                company.CompanyBackgorund = utilities.ProcessPhotoproperty(model.CompanyBackgorund, "Companies");
            }
            company.whyCooseUsText = model.whyCooseUsText;
            company.CompanyEmail = model.CompanyEmail;
            company.CompanyPhone = model.CompanyPhone;
            company.CompanyAdd = model.CompanyAdd;
            company.linkedinProfile = model.linkedinProfile;
            company.FbProfile = model.FbProfile;
            company.twitterProfile = model.twitterProfile;

            context.Companies.Update(company);
            context.SaveChanges();
            return company.CompanyName;
        }


    }
}
