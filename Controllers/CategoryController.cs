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
using WebBuilder.ViewModel.Categories;

namespace WebBuilder.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUtilities utilities;
        public CategoryController(ICategoryRepository _categoryRepository, IUtilities _utilities)
        {
            categoryRepository = _categoryRepository;
            utilities = _utilities;
        }
        // GET: Category
        [Authorize(Roles = "Super Admin")]
        public ActionResult Index()
        {
            var data = categoryRepository.GetDetails().GroupBy(x => new  { x.CompanyName,x.CompanyId})
                              .Select(x => new GroupByCompany() {CompanyId=x.Key.CompanyId, CompanyName = x.Key.CompanyName, Categories = x });
            return View(data);
        }
        //Categories in Company of LoggedIN user
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult Categories(string userId)
        {
            string loggedInAdminId;
            if (!User.IsInRole("Super Admin"))
            {
                loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                if (userId == null) return Forbid();
                loggedInAdminId = userId;
            }
            //find user company id
            var company = utilities.GetUserCompany(loggedInAdminId);
            if (company == null)
            {
                ViewBag.Name = "Company";
                return View("ProductNotFound", company.CompanyName);
            }
            var categories = categoryRepository.GetDetailsWithCompany(company.id).GroupBy(x => new { x.CompanyName, x.CompanyId })
                 .Select(x => new GroupByCompany() { CompanyId = x.Key.CompanyId, CompanyName = x.Key.CompanyName, Categories = x });
            return View("Index",categories);
        }
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var data = categoryRepository.GetDetail(id);
            if (data != null)
            {
                return View(data);
            }
            ViewBag.Name = "Category";
            return View("ProductNotFound", id);        
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    //find user company id
                    var company = utilities.GetUserCompany(loggedInAdminId);
                    if (company == null) return RedirectToAction("Company", "Create");
                    model.CompanyId = company.id;
                    int id = categoryRepository.Add(model);
                    if (id > 0)
                    {
                        return RedirectToAction("Details", new { id = id });
                    }
                    ModelState.AddModelError("", "Unable to Add Product! Please Try Again");
                    return View(model);
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var data = categoryRepository.GetDetail(id);
            if (data != null)
            {
                return View(data);
            }
            ViewBag.Name = "Category";
            return View("ProductNotFound", id);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoriesViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var data = categoryRepository.GetDetail(id);
                    if (data != null)
                    {
                        int modelId = categoryRepository.Update(id, model);
                        return RedirectToAction("Details", new { id = modelId });
                    }
                    ViewBag.Name = "Category";
                    return View("ProductNotFound", id);
                }
                ModelState.AddModelError("", "Something Went Wrong");
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin,Super Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = categoryRepository.delete(id);
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