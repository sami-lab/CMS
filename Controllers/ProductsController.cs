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
using WebBuilder.ViewModel.Products;

namespace WebBuilder.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUtilities utilities;
        public ProductsController(IProductRepository _productRepository, IUtilities _utilities, ICategoryRepository _categoryRepository)
        {
            productRepository = _productRepository;
            utilities = _utilities;
            categoryRepository = _categoryRepository;
        }
        //// GET: Products
        [Authorize(Roles = "Super Admin")]
        public ActionResult Index()
        {
            var products = productRepository.GetDetails();
            return View(products);
        }
        //Products in Company
        //Client Products main Page
        [AllowAnonymous]
        public ActionResult Products(string name, string userId, int? pageNo, int? limit)
        {
            CompaniesViewModel company = null;
            if (name != null)
            {
                company = utilities.getCompanyByName(name);
               
            }
            else
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
                ViewBag.userId = loggedInAdminId;
                //find user company id
                company = utilities.GetUserCompany(loggedInAdminId);
            }
            if (company == null)
            {
                ViewBag.Name = "Company";
                return View("ProductNotFound", name);
            }
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalProducts = productRepository.CountProducts(company.id);
            decimal totalpages = (decimal)totalProducts / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            if (skip >= totalProducts && totalProducts != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }

            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
                company = company,
                Products = productRepository.GetDetailsWithCompany(company.id, skip, (int)limit),
                Categories = categoryRepository.GetDetailsWithCompany(company.id),
                SpecialProducts = productRepository.GetSpecialProducts(company.id)
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PremiumProducts(string name, string userId)
        {
            CompaniesViewModel company = null;
            if (name != null)
            {
                company = utilities.getCompanyByName(name);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", name);
                }
            }
            else
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
                ViewBag.userId = loggedInAdminId;
                //find user company id
                company = utilities.GetUserCompany(loggedInAdminId);
            }

            var products = productRepository.GetSpecialProducts(company.id);
            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
                company = company,
                Products = products,
                Categories = categoryRepository.GetDetailsWithCompany(company.id),
                SpecialProducts = products
            };

            return View("Products", model);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        public ActionResult ListProducts(int? companyId,int? pageNo, int? limit)
        {
            var company = new CompaniesViewModel();
            if(!User.IsInRole("Super Admin"))
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                company = utilities.GetUserCompany(loggedInAdminId);
            }
            else
            {
                if (companyId == null) return Forbid();
                company = utilities.getCompanybyId((int)companyId);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", companyId);
                }
            }
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalUser = productRepository.CountProducts(company.id);
            decimal totalpages = (decimal)totalUser / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            if (skip >= totalUser && totalUser != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
                company = company,
                Products = productRepository.GetDetailsWithCompany(company.id, skip, (int)limit),
                Categories = categoryRepository.GetDetailsWithCompany(company.id),
                SpecialProducts = null
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        public ActionResult SearcProducts(int companyId,int? pageNo, int? limit, string searchList, string title, string details, string overviews)
        {
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;
            int skip = (int)((pageNo - 1) * limit);

            ViewBag.CurrentPage = pageNo;
            Tuple<List<ProductsViewModel>, int> data = new Tuple<List<ProductsViewModel>, int>(new List<ProductsViewModel>(), 0);
            switch (searchList)
            {

                case "title":
                    data = productRepository.SearchProducts<string>(skip, (int)limit, searchList, title);
                    break;
                case "details":
                    data = productRepository.SearchProducts<string>(skip, (int)limit, searchList, details);
                    break;
                case "overview":
                    data = productRepository.SearchProducts<string>(skip, (int)limit, searchList, overviews);
                    break;
                default:
                    data = productRepository.SearchProducts<string>(skip, (int)limit, "", "");
                    break;
            }
            if (skip >= data.Item2 && data.Item2 != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            ViewBag.TotalPages = Math.Ceiling((double)data.Item2 / (double)limit);
            var company = utilities.getCompanybyId(companyId);
            if (company == null)
            {
                ViewBag.Name = "Company";
                return View("ProductNotFound", companyId);
            }
            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
                company= company,
                Products = data.Item1,
                Categories = categoryRepository.GetDetailsWithCompany(companyId),
                SpecialProducts = null
            };
            return View("ListProducts", model);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        public ActionResult Sort(int companyId,int? pageNo, int? limit, string sortBy, string order)
        {
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalUsers = productRepository.CountProducts(companyId);

            decimal totalpages = (decimal)totalUsers / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            if (skip >= totalUsers && totalUsers != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            var data = productRepository.GetDetailsWithCompany(companyId, skip, (int)limit);

            switch (sortBy)
            {
                case "title":
                    data = order == "Des" ? data.OrderByDescending(x => x.title).ToList() : data.OrderBy(x => x.title).ToList();
                    break;
                case "CategoryName":
                    data = order == "Des" ? data.OrderByDescending(x => x.CategoryName).ToList() : data.OrderBy(x => x.CategoryName).ToList();
                    break;
                case "id":
                    data = order == "Des" ? data.OrderByDescending(x => x.id).ToList() : data.OrderBy(x => x.id).ToList();
                    break;
                default:
                    break;

            }
            var company = utilities.getCompanybyId(companyId);
            if (company == null)
            {
                ViewBag.Name = "Company";
                return View("ProductNotFound", companyId);
            }
            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
               company= company,
                Products = data,
                Categories = categoryRepository.GetDetailsWithCompany(companyId),
                SpecialProducts = null
            };
            return View("ListProducts",model);
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id,string name)
        {
            var data = productRepository.GetDetail(id);
            if (data != null)
            {
                if (String.IsNullOrEmpty(name))
                {
                    ViewBag.Name = "Product";
                    return View("ProductNotFound", id);
                }
                 ViewBag.Categories = categoryRepository.GetDetailsWithCompany(utilities.getCompanyByName(name).id);
                return View(data);
            }
            ViewBag.Name = "Product";
            return View("ProductNotFound", id);
        }

        [AllowAnonymous]
        public ActionResult ProductsInCategory(int categoryId, string userId, string name)
        {
            CompaniesViewModel company = null;
            if (name == null)
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
                ViewBag.userId = loggedInAdminId;
                //find user company id
                company = utilities.GetUserCompany(loggedInAdminId);
            }
            else
            {
                company = utilities.getCompanyByName(name);
                if (company == null)
                {
                    ViewBag.Name = "Company";
                    return View("ProductNotFound", name);
                }
            }

            ProductsCategoriesViewModel model = new ProductsCategoriesViewModel()
            {
                company = company,
                Products = productRepository.GetDetailWithCategory(categoryId, company.id),
                Categories = categoryRepository.GetDetailsWithCompany(company.id),
                SpecialProducts = productRepository.GetSpecialProducts(company.id)
            };
            return View("Products", model);
        }
        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? categoryId)
        {
            string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //find user company id
            var company = utilities.GetUserCompany(loggedInAdminId);
            if (company == null) return RedirectToAction("Company", "Create");
            ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
            if (categoryId != null)
            {
                ProductsViewModel model = new ProductsViewModel()
                {
                    CategoryId = (int)categoryId
                };
                return View(model);
            }
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductsViewModel model)
        {
            try
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //find user company id
                var company = utilities.GetUserCompany(loggedInAdminId);
                if(model.Photos == null)
                {
                    ModelState.AddModelError("", "Please Upload Images to continue");
                    ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                    return View(model);
                }
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    model.CompanyId = company.id;
                    int productId = productRepository.Add(model);
                    if (productId > 0)
                    {
                        return RedirectToAction("Details", new { id = productId ,name= company.CompanyName});
                    }
                    ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                    ModelState.AddModelError("", "Unable to Add Product! Please Try Again");
                    return View(model);
                }
                ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong Unable to Add Product! Please Try Again";
                return View("Error");
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var data = productRepository.GetDetail(id);
            if (data != null)
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //find user company id
                var company = utilities.GetUserCompany(loggedInAdminId);
                ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                return View(data);
            }
            ViewBag.Name = "Product";
            return View("ProductNotFound", id);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductsViewModel model)
        {
            try
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //find user company id
                var company = utilities.GetUserCompany(loggedInAdminId);
                // TODO: Add update logic here
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var data = productRepository.GetDetail(id);
                    if (data != null)
                    {
                        int modelId = productRepository.Update(id, model);
                        return RedirectToAction("Details", new { id = modelId,name= company.CompanyName });
                    }
                    ViewBag.Name = "Product";
                    return View("ProductNotFound", id);
                }
               
                ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                ModelState.AddModelError("", "Something Went Wrong");
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //find user company id
                var company = utilities.GetUserCompany(loggedInAdminId);
                ViewBag.categories = categoryRepository.GetDetailsWithCompany(company.id);
                return View(model);
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                // TODO: Add delete logic here
                bool result = productRepository.delete(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                //something Went Wrong
                return View("Error");
            }
            catch
            {
                //something Went Wrong
                return View("Error");
            }
        }

        [Authorize(Roles ="Admin,Super Admin")]
        public JsonResult RemoveImage(int imgId)
        {
            bool result = utilities.RemoveImage(imgId);
            if (result) return Json(true);
            else { return Json(false); }
        }

    }
}