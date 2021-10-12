using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ProductController(AppDbContext context)
        {
            _context = context;
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
    }
}

