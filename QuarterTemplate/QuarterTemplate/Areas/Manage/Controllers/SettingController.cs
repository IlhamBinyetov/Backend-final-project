using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.Helpers;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            Setting settings = _context.Settings.FirstOrDefault();
            return View(settings);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            

            if (setting == null) return NotFound();
            return View(setting);
        }

        [HttpPost]

        public IActionResult Edit(Setting setting)
        {

            if (!ModelState.IsValid) return View();

            Setting existSetting = _context.Settings.FirstOrDefault();

            if (existSetting == null) return NotFound();

            var headerImg = setting.HeaderImg;
            var footerImg = setting.FooterImg;
            var bgImg = setting.BgImg;

            if (headerImg != null)
            {

                if (headerImg.ContentType != "image/png" && headerImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (headerImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string oldImage  = existSetting.HeaderLogo;

                if (oldImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/settings", oldImage);
                }

                string newFileName = FileManager.SaveImage(_env.WebRootPath, "uploads/settings", setting.HeaderImg);

                

                existSetting.HeaderLogo = newFileName;
            }

            if (footerImg != null)
            {

                if (footerImg.ContentType != "image/png" && footerImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (footerImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string oldImage = existSetting.FooterLogo;

                if (oldImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/settings", oldImage);
                }

                string newFileName = FileManager.SaveImage(_env.WebRootPath, "uploads/settings", setting.HeaderImg);

                existSetting.FooterLogo = newFileName;
            }

            if (bgImg != null)
            {

                if (bgImg.ContentType != "image/png" && bgImg.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }
                if (bgImg.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                string oldImage = existSetting.BackgroundImage;

                if (oldImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/settings", oldImage);
                }


                string newFileName = FileManager.SaveImage(_env.WebRootPath, "uploads/settings", setting.BgImg);

                existSetting.BackgroundImage = newFileName;
            }

           
          
            existSetting.MainPhone = setting.MainPhone;
            existSetting.SecondPhone = setting.SecondPhone;
            existSetting.MainEmail = setting.MainEmail;
            existSetting.SecondEmail = setting.SecondEmail;
            existSetting.Location1 = setting.Location1;
            existSetting.Location2 = setting.Location2;
            existSetting.Location3 = setting.Location3;
            existSetting.LocationImage = setting.LocationImage;
            existSetting.LocationLogo = setting.LocationLogo;
            existSetting.PhoneImage = setting.PhoneImage;
            existSetting.PhoneLogo = setting.PhoneLogo;
            existSetting.EmailImage = setting.EmailImage;
            existSetting.EmailLogo = setting.EmailLogo;


            _context.SaveChanges();

            return RedirectToAction("edit");
        }
    }
}
