using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(x=>x.productAminities).Include(x=>x.City).Include(x=>x.Category).
                Include(x=>x.Status).Include(x=>x.ProductImages).Include(x=>x.Team).ToList();

            return View(products);
        }

        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Aminities = _context.Aminities.ToList();
            ViewBag.Statuses = _context.Statuses.ToList();


            return View();
        }


        [HttpPost]

        public IActionResult Create(Product product)
        {
            
            if (!_context.Cities.Any(x => x.Id == product.CityId)) ModelState.AddModelError("CityId", "City not found!");
            if (!_context.Categories.Any(x => x.Id == product.CategoryId)) ModelState.AddModelError("CaregoryId", "Category not found!");
            if (!_context.Statuses.Any(x => x.Id == product.StatusId)) ModelState.AddModelError("StatusId", "Status not found!");
            if (!_context.Teams.Any(x => x.Id == product.TeamId)) ModelState.AddModelError("TeamId", "Team not found!");


            if (!ModelState.IsValid)
            {
                return View();
            }






            List<Product> products = _context.Products.Include(x => x.Status).Include(x => x.Team).Include(x => x.Category).
                Include(x => x.productAminities).Include(x => x.ProductImages).Include(x => x.City).ToList();

            return RedirectToAction("index");
        }
    }
}
