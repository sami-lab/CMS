using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Data;
using WebBuilder.Data.Interfaces;
using WebBuilder.ViewModel.Home;

namespace WebBuilder.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHomeRepository homeRepository;
        private readonly IUtilities utilities;
        private readonly ICompanyRepository companyRepository;

        public HomeController(IUtilities utilities, IHomeRepository homeRepository, ICompanyRepository _companyRepository)
        {
            this.utilities = utilities;
            this.homeRepository = homeRepository;
            companyRepository = _companyRepository;
        }
        public IActionResult Index()
        {
            var companies = companyRepository.GetDetails();
            return View(companies);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult contactus(int? id,string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var company = utilities.getCompanyByName(name);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", name);
                }
                return View(company);
            }
            if (id != null)
            {
                var company = utilities.getCompanybyId((int)id);
                if(company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", id);
                }
                return View(company);
            }
            return View();
        }               
        [HttpPost]
        public IActionResult contactus(ContactViewModel model)
        {
            int companyId=  homeRepository.Add(model);
            TempData["Message"] = "Thanks For Contacting Us! we will respond very Soon";
            if(companyId == 0) return RedirectToAction("contactus");
            return RedirectToAction("contactus",companyId);
        }
        public IActionResult aboutus(int? id,string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var company = utilities.getCompanyByName(name);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", name);
                }
                return View(company);
            }
            if (id != null)
            {
                var company = utilities.getCompanybyId((int)id);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", id);
                }
                return View(company);
            }
            return View();
        }

        public async Task<JsonResult> GetProfile()
        {
            var user = await utilities.getProfile();
            return Json(user);
        }
        public async Task<JsonResult> GetCompanyName()
        {
            var userId= User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var company = await utilities.getCompanyName(userId);
            return Json(company);
        }


    }
}
