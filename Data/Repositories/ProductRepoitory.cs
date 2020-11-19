using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models;
using WebBuilder.ViewModel.Products;

namespace WebBuilder.Data.Repositories
{
    public class ProductRepoitory : IProductRepository
    {
        public readonly ApplicationDbContext context;
        public readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ProductRepoitory(ApplicationDbContext _context, IHostingEnvironment _hostingEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            context = _context;
            hostingEnvironment = _hostingEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        public int Add(ProductsViewModel model)
        {
            Products c = new Products()
            {
                launch= DateTime.Now,
                title= model.title,
                details= WebUtility.HtmlEncode(model.details),
                overviews = WebUtility.HtmlEncode(model.overviews),
                CategoryId= model.CategoryId,
                CompanyId = model.CompanyId,
                isSpecial= model.isSpecial,
            };
            context.Products.Add(c);
            context.SaveChanges();

            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                
                // Loop thru each selected file
                foreach (IFormFile photo in model.Photos)
                {
                    // The file must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the injected
                    // IHostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Image","Products");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    Images i  = new Images()
                    {
                        Image_Path = uniqueFileName,
                        productId= c.id
                    };
                    context.Images.Add(i);

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                context.SaveChanges();
            }
           
            return c.id;
        }

        public bool delete(int id)
        {
            try
            {
                if (!httpContextAccessor.HttpContext.User.IsInRole("Super Admin"))
                {
                  string loginUser= httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                  var company = context.Companies.Include(x=> x.Products).FirstOrDefault(x => x.CompanyOwner == loginUser);
                  var product=   company.Products.FirstOrDefault(x => x.id == id);
                  if (product == null) return false;
                }
                Products b = new Products()
                {
                    id = id
                };
                context.Entry(b).State = EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ProductsViewModel> GetRecentProducts(int? companyId)
        {
            var products = new List<ProductsViewModel>();
            if (companyId != null)
            {
                products = context.Products.Include(x=> x.Categories).Include(x => x.Company).Select(x => new ProductsViewModel()
                {
                    id = x.id,
                    launch = x.launch,
                    title = x.title,
                    details = x.details,
                    isSpecial = x.isSpecial,
                    CompanyId = (int)x.CompanyId,
                    CompanyName= x.Company.CompanyName,

                    CategoryId = x.CategoryId,
                    CategoryName= x.Categories.CategoryName,
                    ImageName = x.Images.FirstOrDefault().Image_Path
                }).Where(x=> x.CompanyId== companyId).OrderByDescending(x => x.launch).Take(10).ToList();
            }
            else
            {
                products = context.Products.Select(x => new ProductsViewModel()
                {
                    id = x.id,
                    launch = x.launch,
                    title = x.title,
                    details = x.details,
                    isSpecial = x.isSpecial,
                    CompanyId = (int)x.CompanyId,
                    CompanyName = x.Company.CompanyName,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Categories.CategoryName,
                    ImageName = x.Images.FirstOrDefault().Image_Path
                }).OrderByDescending(x => x.launch).Take(10).ToList();
            }
          


            return products;
        }

        public ProductsViewModel GetDetail(int id)
        {
            var product = (from br in context.Products
                                        join
                                        company in context.Companies
                                        on br.CompanyId equals company.id
                                        join
                                        category in context.Categories
                                        on br.CategoryId equals category.id
                                        select new ProductsViewModel()
                                        {
                                            id= br.id,
                                            title= br.title,
                                            details= WebUtility.HtmlDecode(br.details),
                                            overviews= WebUtility.HtmlDecode(br.overviews),
                                            isSpecial= br.isSpecial,
                                            CompanyId= (int)br.CompanyId,
                                            CompanyName= company.CompanyName,
                                            CompanyAdd= company.CompanyAdd,
                                            CompanyEmail= company.CompanyEmail,
                                            CompanyPhone= company.CompanyPhone,
                                            CompanyTitle= company.CompanyTitle,
                                            CompanyDesc= company.CompanyDesc,
                                            CompanyLogoPath= company.CompanyLogo,
                                            FbProfile= company.FbProfile,
                                            twitterProfile= company.twitterProfile,
                                            linkedinProfile= company.linkedinProfile,
                                            CategoryId = br.CategoryId,
                                            CategoryName= category.CategoryName,
                                            Images = (from im in context.Images
                                                      where im.productId == br.id
                                                      select new ImagesViewModel()
                                                      {
                                                          id = im.id,
                                                          Image_Path = im.Image_Path,
                                                          productId = im.productId,
                                                      }).ToList()
                                        }).FirstOrDefault(x => x.id == id);
            return product;
        }

        public List<ProductsViewModel> GetDetailWithCategory(int categoryId, int companyId)
        {
            var products = (from br in context.Products
                           join
                           company in context.Companies
                           on br.CompanyId equals company.id
                           join
                           category in context.Categories
                           on br.CategoryId equals category.id
                           select new ProductsViewModel()
                           {
                               id = br.id,
                               launch= br.launch,
                               title = br.title,
                               details = br.details,
                               overviews = br.overviews,
                               isSpecial = br.isSpecial,
                               CompanyId = (int)br.CompanyId,
                               CompanyName = company.CompanyName,
                               FbProfile = company.FbProfile,
                               twitterProfile = company.twitterProfile,
                               linkedinProfile = company.linkedinProfile,
                               CategoryId = br.CategoryId,
                               CategoryName = category.CategoryName,
                               ImageName = (from im in context.Images
                                            where im.productId == br.id
                                            select im.Image_Path).FirstOrDefault()
                           }).OrderByDescending(x=> x.launch).Where(x => x.CategoryId == categoryId && x.CompanyId==companyId).ToList();
            return products;
        }
       
        public List<ProductsViewModel> GetDetailsWithCompany(int companyId, int skip, int limit)
        {
            var products = (from br in context.Products
                            join
                            company in context.Companies
                            on br.CompanyId equals company.id
                            join
                            category in context.Categories
                            on br.CategoryId equals category.id
                            select new ProductsViewModel()
                            {
                                id = br.id,
                                launch= br.launch,
                                title = br.title,
                                details = br.details,
                                overviews = br.overviews,
                                isSpecial = br.isSpecial,
                                CompanyId = (int)br.CompanyId,
                                CompanyName = company.CompanyName,
                                FbProfile = company.FbProfile,
                                twitterProfile = company.twitterProfile,
                                linkedinProfile = company.linkedinProfile,
                                CategoryId = br.CategoryId,
                                CategoryName = category.CategoryName,
                                ImageName = (from im in context.Images
                                             where im.productId == br.id
                                             select im.Image_Path).FirstOrDefault()
                            }).OrderByDescending(x => x.launch).Where(x => x.CompanyId == companyId)
                            .Skip(skip).Take(limit).ToList();
            return products;
        }

        public Tuple<List<ProductsViewModel>, int> SearchProducts<T>(int skip, int limit, string searchList, T searchField)
        {
            int totalProducts = 0;
            var data = new List<Products>();
            switch (searchList)
            {
                case "title":
                    data = searchField != null ? context.Products.Where(x => x.title.ToLower().Contains(searchField.ToString().ToLower())).OrderByDescending(x => x.launch).Skip(skip).Take(limit).ToList() : null;
                    totalProducts = context.Products.Where(x => x.title.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "details":
                    data = searchField != null ? context.Products.Where(x => x.title.ToLower().Contains(searchField.ToString().ToLower())).OrderByDescending(x => x.launch).Skip(skip).Take(limit).ToList() : null;
                    totalProducts = context.Products.Where(x => x.title.ToLower().Contains(searchField.ToString())).Count();
                    break;
                case "overviews":
                    data = searchField != null ? context.Products.Include(x=> x.Images).Where(x => x.title.ToLower().Contains(searchField.ToString().ToLower())).OrderByDescending(x => x.launch).Skip(skip).Take(limit).ToList() : null;
                    totalProducts = context.Products.Include(x=> x.Categories).Include(x => x.Company).Where(x => x.title.ToLower().Contains(searchField.ToString())).Count();
                    break;
                default:
                    data = context.Products.Skip(skip).Take(limit).ToList();
                    totalProducts = context.Products.Count();
                    break;
            }

            return new Tuple<List<ProductsViewModel>, int>(data.Select(product => new ProductsViewModel()
            {
                id = product.id,
                title = product.title,
                details = product.details,
                overviews = product.overviews,
                isSpecial = product.isSpecial,
                CompanyId = (int)product.CompanyId,
                CompanyName = product.Company.CompanyName,
                FbProfile = product.Company.FbProfile,
                twitterProfile = product.Company.twitterProfile,
                linkedinProfile = product.Company.linkedinProfile,
                CategoryId = product.CategoryId,
                CategoryName = product.Categories.CategoryName,
                ImageName= product.Images.FirstOrDefault().Image_Path,
            }).ToList(), totalProducts);

        }


        public List<ProductsViewModel> GetSpecialProducts(int companyId)
        {
            var products = (from br in context.Products
                            join
                            company in context.Companies
                            on br.CompanyId equals company.id
                            join
                            category in context.Categories
                            on br.CategoryId equals category.id                        
                            select new ProductsViewModel()
                            {
                                id = br.id,
                                title = br.title,
                                launch= br.launch,
                                details = br.details,
                                overviews = br.overviews,
                                isSpecial = br.isSpecial,
                                CompanyId = (int)br.CompanyId,
                                CompanyName = company.CompanyName,
                                FbProfile = company.FbProfile,
                                twitterProfile = company.twitterProfile,
                                linkedinProfile = company.linkedinProfile,
                                CategoryId = br.CategoryId,
                                CategoryName = category.CategoryName,
                                ImageName = (from im in context.Images
                                             where im.productId == br.id
                                             select im.Image_Path).FirstOrDefault()
                            }).OrderByDescending(x => x.launch).Where(x=> x.isSpecial && x.CompanyId== companyId).ToList();
            return products;
        }

        public IEnumerable<GroupByCompany> GetDetails()
        {
            var products = context.Products.OrderByDescending(x=> x.launch).GroupBy(x => new { x.CompanyId, x.Company.CompanyName }).Select(x => 
            new GroupByCompany()
            {
                CompanyId = (int)x.Key.CompanyId,
                CompanyName = x.Key.CompanyName,
                Products = x.Take(10).Select(br=> new ProductsViewModel()
                {
                    id = br.id,
                    title = br.title,
                    CategoryId = br.CategoryId,
                    ImageName= (from im in context.Images
                                where im.productId == br.id
                                select im.Image_Path).FirstOrDefault()
                   
                }),
                TotalProducts= x.Count()
            });
         
            return products;
        }

        public int CountProducts(int companyId)
        {
            return context.Products.Where(x=> x.CompanyId==companyId).Count();
        }
        public int Update(int id, ProductsViewModel model)
        {
            var product = context.Products.Find(id);
            if (product == null) return 0;

            product.title = model.title;
            product.details = model.details;
            product.overviews = model.overviews;
            product.isSpecial = model.isSpecial;
            product.CategoryId = model.CategoryId;
            //Handling Images Uploading  ExistingImages.Length - UpcomingPhotos.Length more Images
            if (model.Photos != null && model.Photos.Count > 0)
            {
                string uniqueFileName = null;
                int length = context.Images.Count(x => x.productId == id);
                int ImagesLength = model.Photos.Count;
                if (length + ImagesLength > 5)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        if (5 - length <= 0)
                        {
                            break;
                        }
                        else
                        {
                            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Image","Products");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            Images i = new Images()
                            {
                                productId = id,
                                Image_Path = uniqueFileName,
                            };
                            context.Images.Add(i);
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            // Use CopyTo() method provided by IFormFile interface to
                            // copy the file to wwwroot/images folder
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                            length++;
                        }
                    }
                   
                }
                else
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Image","Products");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        Images i = new Images()
                        {
                            productId = id,
                            Image_Path = uniqueFileName,
                        };
                        context.Images.Add(i);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
            }
            context.Products.Update(product);
            context.SaveChanges();
            return product.id;
        }
    }
}
