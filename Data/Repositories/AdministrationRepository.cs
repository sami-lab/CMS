using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models;
using WebBuilder.ViewModel.Account;
using WebBuilder.ViewModel.Administration;

namespace WebBuilder.Data.Repositories
{
    public class AdministrationRepository : IAdministrationRepository
    {
        private  UserManager<ApplicationUser> Usermanager { get; }
        private  RoleManager<IdentityRole> Rolemanager { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext context;
        private readonly ICompanyRepository companyRepository;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public AdministrationRepository(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext _context, ICompanyRepository _companyRepository, IProductRepository _productRepository, ICategoryRepository _categoryRepository)
        {
            Usermanager = usermanager;
            Rolemanager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            context = _context;
            companyRepository = _companyRepository;
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
        }

        public dashboard Admin(int? companyId, string CompanyName)
        {
            dashboard d = new dashboard();
            if (String.IsNullOrEmpty(CompanyName))
            {
                d.Users = GetAllUsers(0, 10);
                d.TotalUsers = CountUsers();
                d.TotalCategories = context.Categories.Count();
                d.TotalProducts = context.Products.Count();
                d.TotalCompanies = context.Companies.Count();
                d.LastMonthsVisits = context.UsersLocations.Count(x => (x.Time - DateTime.Now).TotalDays <= 30);
                d.Visits = context.UsersLocations.Count();
                d.Companies = companyRepository.GetDetails();
                d.recentProducts = productRepository.GetRecentProducts(null);
                d.recentCategories = categoryRepository.GetRecentCategories(null);
                d.recentVisits = GetUsersLocation(null, 0, 10);
                //Graphs
                var DailyGraphProducts = context.Products.Select(x => new { x.launch, x.id }).Where(x => (x.launch - DateTime.Now).TotalDays <= 30).ToList()
                .GroupBy(x => x.launch.ToString("yyyy/MM/dd"))
                      .Select(pr => new Graph()
                      {
                          Day = pr.Key.ToString(),
                          Total = pr.Count()
                      });
                d.ProductsGraph = DailyGraphProducts.ToList();


                var DailyGraphCategories = context.Categories.Select(x => new { x.launch, x.id }).Where(x => (x.launch - DateTime.Now).TotalDays <= 30).ToList()
               .GroupBy(x => x.launch.ToString("yyyy/MM/dd"))
                     .Select(pr => new Graph()
                     {
                         Day = pr.Key.ToString(),
                         Total = pr.Count()
                     });
                d.CategoryGraph = DailyGraphCategories.ToList();



                var DailyGraphUser = context.UsersLocations.Select(x => new { x.Time, x.id }).Where(x => (x.Time - DateTime.Now).TotalDays <= 30).ToList()
               .GroupBy(x => x.Time.ToString("yyyy/MM/dd"))
                     .Select(pr => new Graph()
                     {
                         Day = pr.Key.ToString(),
                         Total = pr.Count()
                     });
                d.UserStatsGraph = DailyGraphUser.ToList();
              
            }
            else
            {
                d.TotalCategories = context.Categories.Count(x=> x.CompanyId==(int)companyId);
                d.TotalProducts = context.Products.Count(x => x.CompanyId == (int)companyId);
                d.LastMonthsVisits = context.UsersLocations.Count(x => x.Url.ToLower().Contains(CompanyName) && (x.Time - DateTime.Now).TotalDays <= 30);
                d.Visits = context.UsersLocations.Count(x => x.Url.ToLower().Contains(CompanyName));
                d.recentProducts = productRepository.GetRecentProducts(companyId);
                d.recentCategories = categoryRepository.GetRecentCategories(companyId);
                d.recentVisits = GetUsersLocation(CompanyName, 0, 10);
                //Graphs
                var DailyGraphProducts = context.Products.Where(x=> x.CompanyId== companyId).Select(x => new { x.launch, x.id }).Where(x => (x.launch - DateTime.Now).TotalDays <= 30).ToList()
                .GroupBy(x => x.launch.ToString("yyyy/MM/dd"))
                      .Select(pr => new Graph()
                      {
                          Day = pr.Key.ToString(),
                          Total = pr.Count()
                      });
                d.ProductsGraph = DailyGraphProducts.ToList();


                var DailyGraphCategories = context.Categories.Where(x => x.CompanyId == companyId).Select(x => new { x.launch, x.id }).Where(x => (x.launch - DateTime.Now).TotalDays <= 30).ToList()
               .GroupBy(x => x.launch.ToString("yyyy/MM/dd"))
                     .Select(pr => new Graph()
                     {
                         Day = pr.Key.ToString(),
                         Total = pr.Count()
                     });
                d.CategoryGraph = DailyGraphCategories.ToList();

                var DailyGraphUser = context.UsersLocations.Where(x => x.Url.ToLower().Contains(CompanyName.ToLower())).Select(x => new { x.Time, x.id }).Where(x => (x.Time - DateTime.Now).TotalDays <= 30).ToList()
             .GroupBy(x => x.Time.ToString("yyyy/MM/dd"))
                   .Select(pr => new Graph()
                   {
                       Day = pr.Key.ToString(),
                       Total = pr.Count()
                   });
                d.UserStatsGraph = DailyGraphUser.ToList();

               
            }
          

            return d;
        }
        public List<IdentityRole> GetAllRoles()
        {
           return  Rolemanager.Roles.ToList();
        }

        public int CountUsers()
        {
           return Usermanager.Users.Count();
        }

        public int CountVisits(string CompanyName)
        {
            if(String.IsNullOrEmpty(CompanyName)) return context.UsersLocations.Count();
            return context.UsersLocations.Count(x => x.Url.ToLower().Contains(CompanyName));
        }

        public List<ApplicationUserViewModel> GetAllUsers(int skip,int limit)
        {
           return  Usermanager.Users.Select(user=> new ApplicationUserViewModel()
           {
               Id = user.Id,
               Name = user.Name,
               Email = user.Email,
               PhoneNumber = user.PhoneNumber,
               Photopath = user.Photopath,
               Address = new AddressViewModel()
               {
                   City = user.Address.City,
                   Country = user.Address.Country,
                   State = user.Address.State,
                   StreetAddress = user.Address.StreetAddress,
                   id = user.Address.id
               },
               TotaldailyVisits= user.UserStats.Count(x=> x.When.Date == DateTime.Now.Date),
               TotalMonthlyVisits= user.UserStats.OrderBy(x => x.When).Count(x => (x.When - DateTime.Now).TotalDays <= 30)
           }).Skip(skip).Take(limit).ToList();
        }


        public List<UsersLocationViewModel> GetUsersLocation(string CompanyName,int skip, int limit)
        {
            if (String.IsNullOrEmpty(CompanyName))
            {
                var data = context.UsersLocations.Select(x => new UsersLocationViewModel()
                {
                    Browser = x.Browser,
                    id = x.id,
                    Location = x.Location,
                    IP = x.IP,
                    Time = x.Time,
                    Url = x.Url
                }).OrderByDescending(x=> x.Time).Skip(skip).Take(limit).ToList();
                return data;
            }
            else
            {
                var data1 = context.UsersLocations.Select(x => new UsersLocationViewModel()
                {
                    Browser = x.Browser,
                    id = x.id,
                    Location = x.Location,
                    IP = x.IP,
                    Time = x.Time,
                    Url = x.Url
                }).OrderByDescending(x=> x.Time).Where(x=> x.Url.ToLower().Contains(CompanyName.ToLower())).Skip(skip).Take(limit).ToList();
                return data1;
            }
         

        }

        public Tuple<List<ApplicationUserViewModel>, int> SearchUsers<T>(int skip, int limit, string searchList, T searchField)
        {
            int totalUsers = 0 ;
            var data = new List<ApplicationUser>();
            switch (searchList)
            {
                case "Name":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Name.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Name.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "Email":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Email.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Email.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "Id":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Id.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Id.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "PhoneNumber":
                    data = searchField != null ? Usermanager.Users.Where(x => x.PhoneNumber.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.PhoneNumber.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "Country":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Address.Country.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Address.Country.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "State":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Address.State.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Address.State.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "City":
                    data = searchField != null ? Usermanager.Users.Where(x => x.Address.City.ToLower().Contains(searchField.ToString().ToLower())).Skip(skip).Take(limit).ToList() : null;
                    totalUsers = Usermanager.Users.Where(x => x.Address.City.ToLower().Contains(searchField.ToString())).Count();
                    break;
                default:
                    data = Usermanager.Users.Skip(skip).Take(limit).ToList();
                    totalUsers = Usermanager.Users.Count();
                    break;
            }

            return new Tuple<List<ApplicationUserViewModel>, int>(data.Select(user => new ApplicationUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photopath = user.Photopath,
                Address = new AddressViewModel()
                {
                    City = user.Address.City,
                    Country = user.Address.Country,
                    State = user.Address.State,
                    StreetAddress = user.Address.StreetAddress,
                    id = user.Address.id
                }
            }).ToList(), totalUsers);
                      
        }

        public async Task<IdentityResult> delete(string id)
        {
            var user = await Usermanager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await Usermanager.GetRolesAsync(user);
            var RolesResult = await Usermanager.RemoveFromRolesAsync(user, roles);
            if (!RolesResult.Succeeded)
            {
                return RolesResult;
            }
            var result = await Usermanager.DeleteAsync(user);
            return result;
        }
    }
}
