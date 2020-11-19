using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Data;
using WebBuilder.Data.Interfaces;
using WebBuilder.ViewModel.Companies;

namespace WebBuilder.Controllers
{
    public class CompanyController : Controller
    {
        ICompanyRepository companyRepository;
        IUtilities utilities;
        public CompanyController(ICompanyRepository _companyRepository, IUtilities _utilities)
        {
            companyRepository = _companyRepository;
            utilities = _utilities;
        }
        // GET: Company
        [Authorize(Roles = "Super Admin")]
        public ActionResult Index()
        {
            var companies = companyRepository.GetDetails();
            return View(companies);
        }

        // GET: Company/Details/name
        [AllowAnonymous]
        public ActionResult Details(int? id, string name)
        {
            //Searching for Admin Company
            if (id == null && name == null && User.IsInRole("Admin"))
            {
                var loginUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var companyy = utilities.GetUserCompany(loginUser);
                if (companyy != null)
                {
                    var data = companyRepository.GetDetail(companyy.id);
                    if (data != null) return View(data);
                }
                ViewBag.Name = "Company";
                return View("ProductNotFound", id);
            }
            //Searching  Company with id
            if (id != null)
            {
                var data = companyRepository.GetDetail((int)id);
                if (data != null) return View(data);
                ViewBag.Name = "Company";
                return View("ProductNotFound", id);
            }
            //Searching  Company with name
            var company = utilities.getCompanyByName(name);
            if (company != null)
            {
                var data = companyRepository.GetDetail(company.id);
                if (data != null) return View(data);
            }
            ViewBag.Name = "Company";
            return View("ProductNotFound", name);
        }

        // GET: Company/Create
        [Authorize(Roles = "Super Admin, Admin")]
        public ActionResult Create(string userId)
        {
            string loggedInAdminId;
            if (!User.IsInRole("Super Admin"))
            {
                loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                if (String.IsNullOrEmpty(userId)) return Forbid();
                loggedInAdminId = userId;
            }
            var companyy = utilities.GetUserCompany(loggedInAdminId);
            if (companyy != null) return RedirectToAction("Update", new { id = companyy.id });

            CompaniesViewModel model = new CompaniesViewModel()
            {
                CompanyOwner = loggedInAdminId
            };
            return View(model);


        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin, Admin")]//Super Admin Should define owner id
        public ActionResult Create(CompaniesViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (model.CompanyLogo == null || model.CompanyBackgorund == null || model.whyCooseUsImage == null)
                    {
                        ModelState.AddModelError("", "Please Upload all Required Images");
                        return View(model);
                    }
                    if (User.IsInRole("Super Admin"))
                    {
                        if (model.CompanyOwner == null)
                        {
                            ModelState.AddModelError("", "Owner Data is not defined");
                            return View(model);
                        }
                    }
                    else
                    {
                        model.CompanyOwner = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    }
                    string companyname = companyRepository.Add(model);
                    if (companyname != null)
                    {
                        return RedirectToAction("Details", new { name = companyname });
                    }
                    ModelState.AddModelError("", "Unable to Add Company! Please Try Again");
                    return View(model);
                }
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Unable to Add Company! Please Try Again");
                return View(model);
            }
        }

        // GET: Company/Edit/5
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult Edit(int? id)
        {
            int companyId = 0;
            if (User.IsInRole("Admin"))
            {
                var loginUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var companyy = utilities.GetUserCompany(loginUser);
                if (companyy == null) return RedirectToAction("Create", new { userId = loginUser });
                companyId = companyy.id;
            }
            else
            {
                if (id == null) return Forbid();
                companyId = (int)id;
            }

            //if user is admin we will search logged in user company else with id
            var data = companyRepository.GetDetail(companyId);
            //User is in Admin Role and dont have company registered yet
            if (data == null && User.IsInRole("Admin")) return RedirectToAction("Create");
            if (data != null)
            {
                return View(data);
            }
            ViewBag.Name = "Company";
            return View("ProductNotFound", id);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin,Admin")]
        //Policy should be added here so only admin can edit their company only
        public ActionResult Edit(int id, CompaniesViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = companyRepository.GetDetail(id);
                    if (data != null)
                    {
                        string companyname = companyRepository.Update(id, model);
                        if (companyname != null)
                        {
                            return RedirectToAction("Details", new { name = companyname });
                        }
                        ModelState.AddModelError("", "Unable to Add Product! Please Try Again");
                        return View(model);
                    }
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", id);
                }
                ModelState.AddModelError("", "Something Went Wrong");
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }


        // POST: Company/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = companyRepository.delete(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                //something Went Wrong
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}