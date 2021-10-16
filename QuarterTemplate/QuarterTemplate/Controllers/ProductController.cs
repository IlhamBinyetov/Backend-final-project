using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using QuarterTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ProductViewModel productVM = new ProductViewModel
            {
                Categories = _context.Categories.ToList(),
                Aminities = _context.Aminities.ToList(),
                Statuses = _context.Statuses.ToList(),
                Products = _context.Products.Include(x=>x.ProductImages).Include(x=>x.City).Include(x=>x.Category).Include(x=>x.Status).Include(x=>x.Team).Include(x=>x.productAminities).ThenInclude(x=>x.Aminity).ToList()
            };

            return View(productVM);
        }

        public IActionResult Details(int Id)
        {
            DetailViewModel detailVM = new DetailViewModel
            {
                Product = _context.Products.Include(x => x.ProductImages).Include(x => x.City).Include(x => x.Status).Include(x => x.Team).Include(x => x.productAminities).ThenInclude(x => x.Aminity).FirstOrDefault(x => x.Id == Id),
                Categories = _context.Categories.Include(x=>x.Products).ToList()
            };

            return View(detailVM);

        }

        public IActionResult AddToFavorites(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            FavoriteViewModel favoriteItem = null;

            if (product == null) return NotFound();

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            List<FavoriteViewModel> products = new List<FavoriteViewModel>();
            if (member == null)
            {
                string ProductStr;

                if (HttpContext.Request.Cookies["Product"] != null)
                {
                    ProductStr = HttpContext.Request.Cookies["Product"];
                    products = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(ProductStr);

                    favoriteItem = products.FirstOrDefault(x => x.ProductId == id);
                }
                if (favoriteItem == null)
                {
                    favoriteItem = new FavoriteViewModel
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Image = product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image,
                        Price = (double)product.Price,
                        Count = 1
                    };
                    products.Add(favoriteItem);
                }
                else
                {
                    favoriteItem.Count++;
                }
                ProductStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("Product", ProductStr);
            }
            else
            {
                FavoriteItem memberFavItem = _context.FavoriteItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (memberFavItem == null)
                {
                    memberFavItem = new FavoriteItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        Count = 1
                    };
                }

                _context.SaveChanges();
                products = _context.FavoriteItems.Select(x =>
                 new FavoriteViewModel
                 {
                     ProductId = x.ProductId,
                     Count = x.Count,
                     Name = x.Product.Name,
                     Price = (double)x.Product.Price,
                     Image = x.Product.ProductImages
                               .FirstOrDefault(bi => bi.IsPoster == true).Image
                 }).ToList();
            }
            return PartialView("_FavouritePartial", products);
        }


        public IActionResult ShowFavorites(int id)
        {
            var productsStr = HttpContext.Request.Cookies["ProductNames"];

            List<FavoriteViewModel> products = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(productsStr);

            return PartialView("_FavouritePartial", products);
        }



        public IActionResult Deletefavorites(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            FavoriteViewModel favoritItem = null;


            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            }

            List<FavoriteViewModel> products = new List<FavoriteViewModel>();


            if (member == null)
            {
                string ProductStr = HttpContext.Request.Cookies["Product"];
                products = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(ProductStr);

                favoritItem = products.FirstOrDefault(x => x.ProductId == id);



                products.Remove(favoritItem);


                ProductStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("Product", ProductStr);
            }
            else
            {
                FavoriteItem memberFavItem = _context.FavoriteItems.Include(x => x.Product).FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);



                _context.FavoriteItems.Remove(memberFavItem);



                _context.SaveChanges();

                products = _context.FavoriteItems.Include(x => x.Product).ThenInclude(bi => bi.ProductImages).Where(x => x.AppUserId == member.Id)
                    .Select(x => new FavoriteViewModel
                    { ProductId = x.ProductId, 
                      Count = x.Count, 
                      Name = x.Product.Name, 
                      Price = (double)x.Product.Price, 
                      Image = x.Product.ProductImages.FirstOrDefault(b => b.IsPoster == true).Image }).ToList();
            }

            return PartialView("_FavouritePartial", products);

        }

    }
}

