using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuilder.Data;
using WebBuilder.Data.Interfaces;
using WebBuilder.ViewModel.Account;

namespace WebBuilder.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationRepository IadministrationRepository;
        private readonly IUtilities utilities;
        private IHomeRepository homeRepository;
        public readonly ApplicationDbContext context;
        public AdministrationController(IAdministrationRepository administrationRepository,IUtilities utilities,IHomeRepository homeRepository)
        {
            IadministrationRepository = administrationRepository;
            this.homeRepository = homeRepository;
            this.utilities = utilities;
        }
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Admin()
        {

            if (User.IsInRole("Admin"))
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var company = utilities.GetUserCompany(loggedInAdminId);
                if (company == null) return RedirectToAction("Company","Create", new { userId = loggedInAdminId });
                var data = IadministrationRepository.Admin(company.id,company.CompanyName);
                return View(data);
            }
            var datas = IadministrationRepository.Admin(null,null);
            return View(datas);
        }

        public IActionResult Contacts()
        {
            if (!User.IsInRole("Super Admin"))
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var company = utilities.GetUserCompany(loggedInAdminId);
                var contacts= homeRepository.GetDetailsWithCompany(company.id);
                return View(contacts);
            }
            var contact = homeRepository.GetDetailsWithCompany(null);
            return View(contact);
        }

        public IActionResult DeleteContact(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var result =  homeRepository.delete(id);
                if (!result)
                {
                    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                    return View("NotFound");
                }
                return RedirectToAction("Contacts");
            }
            catch
            {
                //something Went Wrong
                return View("Error");
            }
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult GetAllRoles()
        {
            var model = IadministrationRepository.GetAllRoles();
            return View(model);
        }
        [Authorize(Roles = "Super Admin")]
        public IActionResult ListUsers(int? pageNo, int? limit)
        {
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalUser = IadministrationRepository.CountUsers();
            decimal totalpages = (decimal)totalUser / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            if (skip >= totalUser && totalUser != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            var model = IadministrationRepository.GetAllUsers(skip, (int)limit);
            return View(model);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        public IActionResult UsersStats(string CompanyName,int? pageNo, int? limit)
        {
            string Companyname = null;
            if (!User.IsInRole("Super Admin"))
            {
                string loggedInAdminId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var company = utilities.GetUserCompany(loggedInAdminId);
                Companyname = company.CompanyName;
            }
            else
            {
                Companyname = CompanyName;
            }
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalVisits = IadministrationRepository.CountVisits(Companyname);
            decimal totalpages = (decimal)totalVisits / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            ViewBag.CompanyName = Companyname;
            if (skip >= totalVisits && totalVisits != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            var model = IadministrationRepository.GetUsersLocation(Companyname, skip, (int)limit);
            return View(model);
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult Search(int? pageNo, int? limit, string searchList, string Name,string Email,string Id,string PhoneNumber,string Country,string State,string City)
        {
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;
            int skip = (int)((pageNo - 1) * limit);

            ViewBag.CurrentPage = pageNo;
            Tuple<List<ApplicationUserViewModel>, int> data = new Tuple<List<ApplicationUserViewModel>, int>(new List<ApplicationUserViewModel>(),0);
            switch (searchList)
            {

                case "Name":
                    data=   IadministrationRepository.SearchUsers<string>(skip, (int)limit, searchList, Name);
                    break;
                case "Email":
                    data = IadministrationRepository.SearchUsers<string>(skip, (int)limit, searchList, Email);
                    break;
                case "Id":
                    data = IadministrationRepository.SearchUsers<string>(skip, (int)limit, searchList, Id);
                    break;
                case "PhoneNumber":
                    data = IadministrationRepository.SearchUsers<string>(skip, (int)limit, searchList, PhoneNumber); ;
                    break;
                case "City":
                    data = IadministrationRepository.SearchUsers<string>(skip, (int)limit, searchList, City);
                   
                    break;
                default:
                    data = IadministrationRepository.SearchUsers<string>(skip, (int)limit, "", "");
                    break;
            }
            if (skip >= data.Item2 && data.Item2 != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            ViewBag.TotalPages =Math.Ceiling((double)data.Item2 / (double)limit);
            return View("ListUsers", data.Item1);
        }

        [Authorize(Roles = "Super Admin")]
        public ActionResult Sort(int? pageNo, int? limit, string sortBy, string order)
        {
            if (pageNo == null) pageNo = 1;
            if (limit == null) limit = 20;

            int skip = (int)((pageNo - 1) * limit);
            int totalUsers = IadministrationRepository.CountUsers();

            decimal totalpages = (decimal)totalUsers / (decimal)limit;
            ViewBag.TotalPages = Math.Ceiling(totalpages);
            ViewBag.CurrentPage = pageNo;
            if (skip >= totalUsers && totalUsers != 0)
            {
                ViewBag.ErrorMessage = "The Page you are Looking For Could not be found";
                return View("NotFound");
            }
            var data= IadministrationRepository.GetAllUsers(skip, (int)limit);
           
            switch (sortBy)
            {
                case "Name":
                    data = order == "Des" ? data.OrderByDescending(x => x.Name).ToList() : data.OrderBy(x => x.Name).ToList();
                    break;
                case "Email":
                    data = order == "Des" ? data.OrderByDescending(x => x.Email).ToList() : data.OrderBy(x => x.Email).ToList();
                    break;
                case "Id":
                    data = order == "Des" ? data.OrderByDescending(x => x.Id).ToList() : data.OrderBy(x => x.Id).ToList();
                    break;
                case "PhoneNumber":
                    data = order == "Des" ? data.OrderByDescending(x => x.PhoneNumber).ToList() : data.OrderBy(x => x.PhoneNumber).ToList();
                    break;
                case "Country":
                    data = order == "Des" ? data.OrderByDescending(x => x.Address.Country).ToList() : data.OrderBy(x => x.Address.Country).ToList();
                    break;
                case "State":
                    data = order == "Des" ? data.OrderByDescending(x => x.Address.State).ToList() : data.OrderBy(x => x.Address.State).ToList();
                    break;
                case "City":
                    data = order == "Des" ? data.OrderByDescending(x => x.Address.City).ToList() : data.OrderBy(x => x.Address.City).ToList();
                    break;
                default:
                    break;

            }
            return View("ListUsers", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                var result = await IadministrationRepository.delete(id);
                if (result == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                    return View("NotFound");
                }
                if (!result.Succeeded)
                {
                    //something Went Wrong
                    return View("Error");
                }
                return RedirectToAction("ListUsers", "Administration");
            }
            catch
            {
                //something Went Wrong
                return View("Error");
            }
        }
    }
}