using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class AboutUs : Controller
    {
        private readonly AppDbContext _context;

        public AboutUs(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
