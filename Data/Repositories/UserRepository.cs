using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models;
using WebBuilder.Data.Models.User;
using WebBuilder.ViewModel.Account;

namespace WebBuilder.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserManager<ApplicationUser> Usermanager { get; }
        public RoleManager<IdentityRole> Rolemanager { get; }
        public SignInManager<ApplicationUser> Signinmanager { get; }
        public IUtilities utilities { get; }
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IUtilities _utilities)
        {
            Usermanager = usermanager;
            Signinmanager = signinmanager;
            Rolemanager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            this.hostingEnvironment = hostingEnvironment;
            utilities = _utilities;
        }

        //if user exist return true
        public async Task<bool> IsEmailexist(string email)
        {
            var user = await Usermanager.FindByEmailAsync(email);
            if (user != null)
                return true;
            else
                return false;
        }

        public async Task<Tuple<string, IdentityResult>> Register(ApplicationUserViewModel model)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Photopath = utilities.ProcessPhotoproperty(model.Photo, "User"),
                UserName = model.Email,
                Address = new Address()
                {
                    Country = model.Address.Country,
                    State = model.Address.State,
                    City = model.Address.City,
                    StreetAddress = model.Address.StreetAddress
                }

            };
            var result = await Usermanager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new Tuple<string, IdentityResult>(null, result);
               
            }
            if (Signinmanager.IsSignedIn(_httpContextAccessor.HttpContext.User) && _httpContextAccessor.HttpContext.User.IsInRole("Super Admin"))
            {
                await Usermanager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await Usermanager.AddToRoleAsync(user, "User");
            }
            return new Tuple<string, IdentityResult>(user.Id, result);
        }

        public async Task<ApplicationUserViewModel> Login(LoginViewModel model)
        {
            var user = await Usermanager.FindByEmailAsync(model.Email);


            if (user == null || !(await Usermanager.CheckPasswordAsync(user, model.Password)))
            {
                return null;
                //ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                //return View(model);
            }
            var result = await Signinmanager.PasswordSignInAsync(
                user, model.Password, false, false);

            if (result.Succeeded)
            {
                var AppUser = new ApplicationUserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Photopath = user.Photopath,
                };
                return AppUser;
            }
            return null;
            //ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }

        public async Task Logout()
        {
            await Signinmanager.SignOutAsync();
        }

        public async Task<ApplicationUserViewModel> GetDetail(string id)
        {
            var user = await Usermanager.Users
                  .Include(x => x.Address)
                  .SingleAsync(x => x.Id == id);
            int CountryId = utilities.getCountries().FirstOrDefault(x => x.name == user.Address.Country).id;
            int StateId = utilities.getStates(CountryId).FirstOrDefault(x => x.name == user.Address.State).id;
            int CityId = utilities.getCities(StateId).FirstOrDefault(x => x.name == user.Address.City).id;
            return new ApplicationUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photopath = user.Photopath,
                Address = new AddressViewModel()
                {
                    CountryId = CountryId,
                    Country = user.Address.Country,
                    StateId = StateId,
                    State = user.Address.State,
                    CityId = CityId,
                    City = user.Address.City,
                    StreetAddress = user.Address.StreetAddress,
                    id = user.Address.id
                }
            };
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await Usermanager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return null;
            }
            // ChangePasswordAsync changes the user password
            var result = await Usermanager.ChangePasswordAsync(user,
                model.CurrentPassword, model.NewPassword);

            // The new password did not meet the complexity rules or
            // the current password is incorrect. Add these errors to
            // the ModelState and rerender ChangePassword view
            if (result.Succeeded)
            {
                await Signinmanager.RefreshSignInAsync(user);
            }
            return result;
        }

        public async Task<Tuple<string,IdentityResult>> Edit(string id, ApplicationUserViewModel model)
        {
            var user = await Usermanager.Users.Include(x => x.Address)
                  .SingleAsync(x => x.Id == id);
            if (user == null) return null;
            
            user.Email = model.Email;
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Email;
            user.Address = new Address()
            {
                Country = model.Address.Country,
                State = model.Address.State,
                City = model.Address.City,
                StreetAddress = model.Address.StreetAddress
            };

            if (model.Photo != null)
            {
                if (user.Photopath != null)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath, "Image", "User", user.Photopath);
                    System.IO.File.Delete(filepath);
                }
                user.Photopath = utilities.ProcessPhotoproperty(model.Photo, "User");
            }
            var result = await Usermanager.UpdateAsync(user);
            return new Tuple<string,IdentityResult>(user.Id, result);
        }


    }
}

