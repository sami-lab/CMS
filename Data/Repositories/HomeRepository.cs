using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models.Contact;
using WebBuilder.Data.Models.User;
using WebBuilder.ViewModel.Account;
using WebBuilder.ViewModel.Home;

namespace WebBuilder.Data.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext context;

        public HomeRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        //will return companyId
        public int Add(ContactViewModel model)
        {
            Contact c = new Contact()
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Subject = model.Message,
                Phone = model.Phone,
            };
            if(model.CompanyId != null)
            {
                c.CompanyId = model.CompanyId;
            }
            context.Contact.Add(c);
            context.SaveChanges();
            return c.CompanyId?? 0;
        }

        public List<ContactViewModel> GetDetailsWithCompany(int? companyId)
        {
            if (companyId != null)
            {
                var contacts = context.Contact.Select(x => new ContactViewModel()
                {
                    id = x.id,
                    Email= x.Email,
                    Phone= x.Phone,
                    Message= x.Message,
                    Subject= x.Subject,
                    Name= x.Name,
                    CompanyId = x.CompanyId,
                }).Where(x=> x.CompanyId== companyId).ToList();
                return contacts;
            }
            var contact = context.Contact.Select(x => new ContactViewModel()
            {
                id = x.id,
                Email = x.Email,
                Phone = x.Phone,
                Message = x.Message,
                Subject = x.Subject,
                Name = x.Name,
                CompanyId = x.CompanyId,
            }).Where(x => x.CompanyId == null).ToList();
            return contact;
        }

        public bool delete(int id)
        {
            
            Contact c = new Contact()
            {
                id = id
            };
            context.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
            return true;
        }


      
    }
}
