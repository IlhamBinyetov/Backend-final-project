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
    public class CityController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CityController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<City> cities = _context.Cities.ToList(); 

            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(City city)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);

            if (city == null) return NotFound();

            return View(city);
        }

        [HttpPost]

        public IActionResult Edit(City city)
        {
            if (!ModelState.IsValid) return View();

            City existCity = _context.Cities.FirstOrDefault(x => x.Id == city.Id);

            if (existCity == null) return NotFound();


            existCity.Name = city.Name;
           


            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult DeleteFetch(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);

            if (city == null) return Json(new { status = 404 });

            try
            {
                _context.Cities.Remove(city);
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
