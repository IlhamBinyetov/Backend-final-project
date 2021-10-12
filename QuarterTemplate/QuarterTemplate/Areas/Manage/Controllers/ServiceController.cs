using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            ViewBag.Icons = _context.Services.ToList();

            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (!ModelState.IsValid) return View();

            Service existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null) return NotFound();


            existService.Title = service.Title;
            existService.Description = service.Description;
            existService.Order = service.Order;
            existService.Icon = service.Icon;
            

            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult DeleteFetch(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);

            if (service == null) return Json(new { status = 404 });

            try
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { status = 500 });
            }
            

            return Json(new { status = 200 });


        }

    }
}
