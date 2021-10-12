using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Service> services = _context.Services.OrderBy(x=>x.Order).ToList();

            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Icons = _context.Services.ToList();
            return View();
        }

        [HttpPost]

        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
