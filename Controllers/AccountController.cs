using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Data;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models.User;
using WebBuilder.ViewModel.Account;

namespace WebBuilder.Controllers
{
    public class AccountController : Controller
    {
        public readonly IUserRepository iUserRepository;
        IUtilities utilities;
        public AccountController(IUserRepository iuserRepository, IUtilities _utilities)
        {
            iUserRepository = iuserRepository;
            utilities = _utilities;
        }
      
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailexist(string email)
        {
            var user = await iUserRepository.IsEmailexist(email);
            return user == true ? Json($"Email {email} is already in Use") : Json(true);
        }
        
        [HttpGet]
        [Authorize(Roles = "Super Admin")]
        public IActionResult Register()
        {
            ViewBag.Countries = utilities.getCountries();
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Register(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await iUserRepository.Register(model);
                if (!result.Item2.Succeeded)
                {
                    foreach (var error in result.Item2.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ViewBag.Countries = utilities.getCountries();
                    return View(model);
                }
                if (result.Item1 != null)
                {
                    if(User.IsInRole("Super Admin")) return RedirectToAction("Create", "Company",new {userId = result.Item1 });
                    return RedirectToAction("Index","Home");
                }
                ViewBag.Countries = utilities.getCountries();
                ModelState.AddModelError("", "something Went wrong");
                return View(model);
            }
            //add Viewbag fields here
            ViewBag.Countries = utilities.getCountries();
            return View(model);

        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await iUserRepository.Login(model);
                if (user != null)
                {
                    await utilities.IncrementAccess(user.Id);
                    return RedirectToAction("Profile", "Account",new { id= user.Id});
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attemp");
                return View(model);
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await iUserRepository.Logout();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            var loginUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != null && !User.IsInRole("Super Admin") && id != loginUser) return Forbid();
            if (id == null)
            {
                id = loginUser;
            }
            var user = await iUserRepository.GetDetail(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {id} is invalid";
                return View("NotFound");
            }
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                if(User.IsInRole("Super Admin")) id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                else return Forbid();
            }
            var user = await iUserRepository.GetDetail(id);
            if (user == null)
            {                
                 ViewBag.ErrorMessage = $"The User ID {id} is invalid";
                 return View("NotFound");  
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel() { Id = user.Id };
            return View(model);       
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await iUserRepository.ChangePassword(model);
                if(result == null)
                {
                    ViewBag.ErrorMessage = $"The User ID {model.Id} is invalid";
                    return View("NotFound");
                }
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                return View("ChangePasswordConfirmation");

            }

            return View(model);
        }

        // GET: Account/Edit/5
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await iUserRepository.GetDetail(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {id} is invalid";
                return View("NotFound");
            }
            ViewBag.Countries = utilities.getCountries();
            return View(user);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<ActionResult> Edit(string id, ApplicationUserViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                var result = await iUserRepository.Edit(id, model);
                if (result == null)
                {
                    ViewBag.ErrorMessage = $"The User ID {id} is invalid";
                    return View("NotFound");
                }
                if (!result.Item2.Succeeded)
                {
                    foreach (var error in result.Item2.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ViewBag.Countries = utilities.getCountries();
                    return View(model);
                }
                return RedirectToAction("Profile",new { id= result.Item1});
            }
            catch
            {
                ViewBag.Countries = utilities.getCountries();
                 ModelState.AddModelError("","UnExpected Error Occur");
                return View(model);
            }
        }

       

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public JsonResult getStates(int country_id)
        {
            var list = utilities.getStates(country_id);
            return Json(list);
        }
        public JsonResult getCities(int state_id)
        {
            var list = utilities.getCities(state_id);
            return Json(list);
        }
    }
}