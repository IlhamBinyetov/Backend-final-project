using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using QuarterTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(HomeViewModel homeVm)
        {
            List<Order> orders = _context.Orders.Where(x => x.Status == Models.Enums.OrderStatus.Accepted).ToList();

            double sumOfAcceotedOrders = orders.Count();


            ViewBag.AcceptedOrders = sumOfAcceotedOrders;


            string str = HttpContext.Request.Cookies["Product"];
            ViewBag.Favorites = null;
            if (str != null)
            {
                ViewBag.Favorites = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(str);
            }



            homeVm = new HomeViewModel()
            {
                Sliders = _context.Sliders.ToList(),
                Services = _context.Services.OrderBy(x => x.Order).Skip(3).Take(3).ToList(),
                AboutUses = _context.AboutUses.FirstOrDefault(),
                Abouts = _context.Abouts.OrderBy(x => x.Order).ToList(),
                Cities = _context.Cities.ToList(),
                Settings = _context.Settings.ToList(),
                Statuses = _context.Statuses.ToList(),
                Categories = _context.Categories.ToList(),
                Aminities = _context.Aminities.ToList(),
                IsFeatured = _context.Products.Where(x => x.IsFeature == true).ToList(),
                Products = _context.Products.Include(x => x.Status).Include(x => x.ProductImages).Include(x => x.Team).ToList(),

                LastSoldProduct = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault().Product

            };

            return View(homeVm);
        }

        public IActionResult AddProduct(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == id);

            List<FavoriteViewModel> favorites = new List<FavoriteViewModel>();
            string namesStr;
            FavoriteViewModel favorite = null;

            if (HttpContext.Request.Cookies["ProductNames"] != null)
            {
                namesStr = HttpContext.Request.Cookies["ProductNames"];
                favorites = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(namesStr);
                favorite = favorites.FirstOrDefault(x => x.ProductId == product.Id);
            }
            if(favorite==null)
            {
                favorite = new FavoriteViewModel
                {
                    Count = 1,
                    ProductId = product.Id,
                    Image = product.ProductImages.FirstOrDefault(x => x.IsPoster)?.Image,
                    Name = product.Name,
                    Price = (double)product.Price
                };
                favorites.Add(favorite);

            }
            else
            {
                favorite.Count++;
            }

            namesStr = JsonConvert.SerializeObject(favorites);
            HttpContext.Response.Cookies.Append("ProductNames", namesStr);


            return PartialView("_FavouritePartial", favorites);
        }

        public IActionResult GetProduct(int id)
        {
            Product product = _context.Products
                .Include(x => x.ProductImages).Include(x => x.Category)
                .Include(x => x.Status).Include(x => x.City)
                .Include(x => x.productAminities).ThenInclude(x => x.Aminity)
                .FirstOrDefault(x => x.Id == id);

            return PartialView("_ProductModalPartial", product);
        }



    }
}
