using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBuilder.Data.Models;
using WebBuilder.Data.Models.User;
using WebBuilder.ViewModel.Companies;

namespace WebBuilder.Data
{
    public class utilities : IUtilities
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserManager<ApplicationUser> Usermanager { get; }
        public utilities(IHostingEnvironment hostingEnvironment, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> usermanager, IHttpContextAccessor _httpContextAccessor)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.context = applicationDbContext;
            httpContextAccessor = _httpContextAccessor;
            Usermanager = usermanager;
        }
        public string ProcessPhotoproperty(IFormFile Photo, string InnerFolder)
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadedfile = Path.Combine(hostingEnvironment.WebRootPath, "Image", InnerFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filepath = Path.Combine(uploadedfile, uniqueFileName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    Photo.CopyTo(filestream);
                }
            }

            return uniqueFileName;
        }

        public CompaniesViewModel GetUserCompany(string userId)
        {
            return context.Companies.Select(x => new CompaniesViewModel()
            {
                id = x.id,
                Date = x.Date,
                CompanyAdd = x.CompanyAdd,
                CompanyOwner = x.CompanyOwner,
                CompanyOwnerName = x.User.Name,
                CompanyOwnerImage = x.User.Photopath,
                CompanyBackgorundPath = x.CompanyBackgorund,
                CompanyDesc = x.CompanyDesc,
                CompanyEmail = x.CompanyEmail,
                CompanyLogoPath = x.CompanyLogo,
                CompanyPhone = x.CompanyPhone,
                CompanyTitle = x.CompanyTitle,
                whyCooseUsImagePath = x.whyCooseUsImage,
                whyCooseUsText = x.whyCooseUsText,
                CompanyName = x.CompanyName,
                TotalProducts = x.Products.Count,
                TotalCategories = x.Catergories.Count,
                Categories = x.Catergories.Select(y => new ViewModel.Categories.CategoriesViewModel()
                {
                    CategoryName = y.CategoryName,
                    CompanyId = y.CompanyId,
                    TotalProducts = y.Products.Count,
                    id = y.id,
                    CompanyName = y.Company.CompanyName
                }).ToList()
            }).FirstOrDefault(x => x.CompanyOwner == userId);
        }

        public CompaniesViewModel getCompanybyId(int id)
        {
            return context.Companies.Include(x=> x.Catergories).Select(x => new CompaniesViewModel()
            {
                id = x.id,
                Date= x.Date,
                CompanyAdd = x.CompanyAdd,
                CompanyOwner = x.CompanyOwner,
                CompanyOwnerName= x.User.Name,
                CompanyOwnerImage = x.User.Photopath,
                CompanyBackgorundPath = x.CompanyBackgorund,
                CompanyDesc = x.CompanyDesc,
                CompanyEmail = x.CompanyEmail,
                CompanyLogoPath = x.CompanyLogo,
                CompanyPhone = x.CompanyPhone,
                CompanyTitle = x.CompanyTitle,
                whyCooseUsImagePath = x.whyCooseUsImage,
                whyCooseUsText = x.whyCooseUsText,
                CompanyName= x.CompanyName,
                TotalProducts= x.Products.Count,
                TotalCategories= x.Catergories.Count,
                Categories= x.Catergories.Select(y=> new ViewModel.Categories.CategoriesViewModel()
                {
                    CategoryName= y.CategoryName,
                    CompanyId= y.CompanyId,
                    TotalProducts= y.Products.Count,
                    id= y.id,
                    CompanyName= y.Company.CompanyName
                }).ToList()
            }).FirstOrDefault(x => x.id == id);
        }

        public CompaniesViewModel getCompanyByName(string name)
        {
            return context.Companies.Include(x => x.Catergories).Select(x => new CompaniesViewModel()
            {
                id = x.id,
                Date = x.Date,
                CompanyAdd = x.CompanyAdd,
                CompanyOwner = x.CompanyOwner,
                CompanyOwnerName = x.User.Name,
                CompanyOwnerImage = x.User.Photopath,
                CompanyBackgorundPath = x.CompanyBackgorund,
                CompanyDesc = x.CompanyDesc,
                CompanyEmail = x.CompanyEmail,
                CompanyLogoPath = x.CompanyLogo,
                CompanyPhone = x.CompanyPhone,
                CompanyTitle = x.CompanyTitle,
                whyCooseUsImagePath = x.whyCooseUsImage,
                whyCooseUsText = x.whyCooseUsText,
                CompanyName = x.CompanyName,
                TotalProducts = x.Products.Count,
                TotalCategories = x.Catergories.Count,
                Categories = x.Catergories.Select(y => new ViewModel.Categories.CategoriesViewModel()
                {
                    CategoryName = y.CategoryName,
                    CompanyId = y.CompanyId,
                    TotalProducts = y.Products.Count,
                    id = y.id,
                    CompanyName = y.Company.CompanyName
                }).ToList()
            }).FirstOrDefault(x => x.CompanyName.ToLower() == name.ToLower());
        }
        //Get All Cities List
        public List<countries> getCountries()
        {
            string file_path = Path.Combine(hostingEnvironment.WebRootPath, "data", "countries.json");
            try
            {
                string file_data = System.IO.File.ReadAllText(file_path, Encoding.UTF8);
                if (string.IsNullOrEmpty(file_data) == true)
                {
                    return null;
                }
                // Get the document
                List<countries> doc = JsonConvert.DeserializeObject<List<countries>>(file_data);
                return doc.OrderBy(x=> x.name).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<states> getStates(int country_id)
        {
            string file_path = Path.Combine(hostingEnvironment.WebRootPath, "data", "states.json");
            try
            {
                string file_data = System.IO.File.ReadAllText(file_path, Encoding.UTF8);
                if (string.IsNullOrEmpty(file_data) == true)
                {
                    return null;
                }
                // Get the document
                List<states> doc = JsonConvert.DeserializeObject<List<states>>(file_data);
                return doc.Where(x=> x.country_id==country_id).OrderBy(x => x.name).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<cities> getCities(int state_id)
        {
            string file_path = Path.Combine(hostingEnvironment.WebRootPath, "data", "cities.json");
            try
            {
                string file_data = System.IO.File.ReadAllText(file_path, Encoding.UTF8);
                if (string.IsNullOrEmpty(file_data) == true)
                {
                    return null;
                }
                // Get the document
                List<cities> doc = JsonConvert.DeserializeObject<List<cities>>(file_data);
                return doc.Where(x => x.state_id == state_id).OrderBy(x => x.name).ToList();
            }
            catch
            {
                return null;
            }
        }


        //To Remove Old images of Products
        public bool RemoveImage(int imageId)
        {
            var exist = context.Images.FirstOrDefault(x => x.id == imageId);
            if (exist == null)
            {
                return false;
            }
            try
            {
                context.Images.Remove(exist);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<string> getProfile()
        {
            var user = await Usermanager.GetUserAsync(httpContextAccessor.HttpContext.User);
            if (user != null)
            {
                return user.Photopath;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> IncrementAccess(string userId)
        {
            try
            {
                var todayVisits = context.UserStats.FirstOrDefault(x => x.UserId == userId && x.When.Date == DateTime.Now.Date);
                if (todayVisits != null)
                {
                    todayVisits.Count += 1;
                    context.UserStats.Update(todayVisits);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    UserStats stats = new UserStats()
                    {
                        When = DateTime.Now,
                        Count = 1,
                        UserId = userId,
                    };
                    context.UserStats.Add(stats);
                    await context.SaveChangesAsync();
                    return true;
                }

               
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
    public struct countries
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public struct states
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }

    }
    public struct cities
    {
        public int id { get; set; }
        public string name { get; set; }
        public int state_id { get; set; }
        public int country_id { get; set; }
    }
}
