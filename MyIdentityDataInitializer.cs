using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using WebBuilder.Data.Models;

namespace WebBuilder
{
    public class MyIdentityDataInitializer
    {
        public MyIdentityDataInitializer(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void SeedData
    (UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public void SeedUsers
    (UserManager<ApplicationUser> userManager)
        {
            try
            {


                if (userManager.FindByEmailAsync("85575687@qq.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    //user.UserName = Configuration["Email"];
                    //user.Email = Configuration["Email"];
                    //user.Name = Configuration["Name"];
                    //user.PhoneNumber = Configuration["Phone"];
                    //user.EmailConfirmed = true;
                    //user.Address = new Address()
                    //{
                    //    Country = Configuration["Country"],
                    //    State = Configuration["State"],
                    //    City = Configuration["City"],
                    //    StreetAddress = Configuration["Address"]
                    //};
                    user.UserName = "85575687@qq.com";
                    user.Email = "85575687@qq.com";
                    user.Name = "薛凡伟";
                    user.PhoneNumber = "18101078787";
                    user.EmailConfirmed = true;
                    user.Address = new Address()
                    {
                        Country = "China",
                        State = "Beijing",
                        City = "Beijing",
                        StreetAddress = "Beijing China"
                    };

                    IdentityResult result = userManager.CreateAsync
                    (user, "Admin1234@").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user,
                                            "Super Admin").Wait();
                    }
                }
            }
            catch 
            {

            }
        }

        public void SeedRoles
    (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Super Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Super Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
