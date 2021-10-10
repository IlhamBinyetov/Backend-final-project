using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                Services = _context.Services.OrderBy(x => x.Order).Skip(3).Take(3).ToList(),
                AboutUses = _context.AboutUses.FirstOrDefault(),
                Abouts = _context.Abouts.OrderBy(x => x.Order).ToList(),    
                Teams = _context.Teams.OrderBy(x=>x.Order).Take(3).ToList()
            };


            return View(aboutVM);
        }
    }
}
