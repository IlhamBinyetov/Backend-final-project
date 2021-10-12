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
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.Include(x=>x.Products).ToList(); 

            return View(teams);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (team.ImageFile != null)
            {
                if (team.ImageFile.ContentType != "image/png" && team.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }

                if (team.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }
                string fileName = team.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length -64,64);
                }
                string newFileName = Guid.NewGuid().ToString() + fileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/teams", newFileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    team.ImageFile.CopyTo(stream);
                }

                team.Image = newFileName;
            }

            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null) return NotFound();

            return View(team);
        }

        [HttpPost]

        public IActionResult Edit(Team team)
        {
            if (!ModelState.IsValid) return View();

            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == team.Id);

            if (existTeam == null) return NotFound();

            string newFileName = null;
            if (team.ImageFile != null)
            {
                if (team.ImageFile.ContentType != "image/png" && team.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }

                if (team.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size can not be more than 2MB!");
                    return View();
                }

                newFileName = Guid.NewGuid().ToString() + team.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/teams", newFileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    team.ImageFile.CopyTo(stream);
                }
            }

            if (newFileName != null || team.Image == null)
            {
                if (existTeam.Image != null)
                {
                    string deletePath = Path.Combine(_env.WebRootPath, "uploads/teams", existTeam.Image);

                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }

                existTeam.Image = newFileName;
            }

            existTeam.Name = team.Name;
            existTeam.Image = team.Image;
            existTeam.Order = team.Order;
            existTeam.FacebookUrl = team.FacebookUrl;
            existTeam.LinkedinUrl = team.LinkedinUrl;
            existTeam.TwitterUrl = team.TwitterUrl;
           
            

            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult DeleteFetch(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null) return Json(new { status = 404 });

            try
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { status = 500 });
            }
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/teams", team.Image);

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }

            return Json(new { status = 200 });


        }

    }
}
