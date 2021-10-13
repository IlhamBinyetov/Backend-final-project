using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<About> abouts = _context.Abouts.Include(x=>x.AboutUs).ToList();

            return View(abouts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Icons = _context.Services.ToList();
            List<About> abouts = _context.Abouts.Include(x => x.AboutUs).ToList();
            return View(abouts);
        }

        [HttpPost]

        public IActionResult Create(About about)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var mainImg = about.AboutUs.MainImageFile;
            var secondImg = about.AboutUs.SecondImageFile;


            if (mainImg != null)
            {
                
                if (mainImg.ContentType != "image/png" && mainImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (mainImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string fileName = mainImg.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                string newFileName = Guid.NewGuid().ToString() + fileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts/others", newFileName);


                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                mainImg.CopyTo(stream);
                }

                about.AboutUs.Image = newFileName;
                }

            if (secondImg != null)
            {

                if (secondImg.ContentType != "image/png" && secondImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (secondImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string fileName = secondImg.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                string newFileName = Guid.NewGuid().ToString() + fileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts/others", newFileName);


                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    secondImg.CopyTo(stream);
                }

                about.AboutUs.Image2 = newFileName;
            }

            _context.Abouts.Add(about);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            About about = _context.Abouts.Include(x=>x.AboutUs).FirstOrDefault(x => x.Id == id);
            ViewBag.Icons = _context.Abouts.ToList();

            if (about == null) return NotFound();

            return View(about);
        }


        [HttpPost]

        public IActionResult Edit(About about)
        {
            if (!ModelState.IsValid) return View();

            About existAbout = _context.Abouts.Include(x=>x.AboutUs).FirstOrDefault(x => x.Id == about.Id);


            if (existAbout == null) return NotFound();

            var mainImg = about.AboutUs.MainImageFile;
            var secondImg = about.AboutUs.SecondImageFile;

            if (mainImg != null)
            {

                if (mainImg.ContentType != "image/png" && mainImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (mainImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string fileName = mainImg.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                string newFileName = Guid.NewGuid().ToString() + fileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts/others", newFileName);


                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    mainImg.CopyTo(stream);
                }

                about.AboutUs.Image = newFileName;
            }

            if (secondImg != null)
            {

                if (secondImg.ContentType != "image/png" && secondImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (secondImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string fileName = secondImg.FileName;
                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                string newFileName = Guid.NewGuid().ToString() + fileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts/others", newFileName);


                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    secondImg.CopyTo(stream);
                }

                about.AboutUs.Image2 = newFileName;
            }

            existAbout.Title = about.Title;
            existAbout.Icon = about.Icon;
            existAbout.Order = about.Order;
            existAbout.AboutUs.Description1 = about.AboutUs.Description1;
            existAbout.AboutUs.Image = about.AboutUs.Image;
            existAbout.AboutUs.Description2 = about.AboutUs.Description2;
            existAbout.AboutUs.Image2 = about.AboutUs.Image2;
            existAbout.AboutUs.Title = about.AboutUs.Title;




            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult DeleteFetch(int id)
        {
            About about = _context.Abouts.Include(x=>x.AboutUs).FirstOrDefault(x => x.Id == id);

            if (about == null) return Json(new { status = 404 });

            try
            {
                _context.Abouts.Remove(about);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { status = 500 });
            }
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/abouts/others", about.AboutUs.Image);

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }

            return Json(new { status = 200 });


        }
    }
}
