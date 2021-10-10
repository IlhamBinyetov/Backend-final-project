using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        
        public IActionResult Index()
        {
            HomeViewModel homeVm = new HomeViewModel()
            {
                Sliders = _context.Sliders.ToList(),
                Services = _context.Services.OrderBy(x => x.Order).Skip(3).Take(3).ToList(),
                AboutUses = _context.AboutUses.FirstOrDefault(),
                Abouts = _context.Abouts.OrderBy(x => x.Order).ToList(),
                Cities = _context.Cities.ToList(),
                Statuses = _context.Statuses.ToList(),
                Categories = _context.Categories.ToList()

            };

            return View(homeVm);
        }

       

       
    }
}
