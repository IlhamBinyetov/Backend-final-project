using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    //[Authorize(Roles = "SuperAdmin, Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(string search =null)
        {
            var query = _context.Products.Include(x => x.Team).AsQueryable();
            ViewBag.CurrenSearch = search;

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search) || x.Team.Name.Contains(search));

            List<Product> products = _context.Products.Include(x=>x.productAminities).ThenInclude(x=>x.Aminity).Include(x=>x.City).Include(x=>x.Category).
                Include(x=>x.Status).Include(x=>x.ProductImages).Include(x=>x.Team).ToList();

            return View(products);
        }

        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Aminities = _context.Aminities.ToList();
            ViewBag.Statuses = _context.Statuses.ToList();
            ViewBag.Teams = _context.Teams.ToList();


            return View();
        }


        [HttpPost]

        public IActionResult Create(Product product)
        {
            
            if (!_context.Cities.Any(x => x.Id == product.CityId)) ModelState.AddModelError("CityId", "City not found!");
            if (!_context.Categories.Any(x => x.Id == product.CategoryId)) ModelState.AddModelError("CaregoryId", "Category not found!");
            if (!_context.Statuses.Any(x => x.Id == product.StatusId)) ModelState.AddModelError("StatusId", "Status not found!");
            if (!_context.Teams.Any(x => x.Id == product.TeamId)) ModelState.AddModelError("TeamId", "Team not found!");


            foreach (var aminityId in product.AminityIds)
            {
                Aminity aminity = _context.Aminities.FirstOrDefault(x => x.Id == aminityId);

                if (aminity == null)
                {
                    ModelState.AddModelError("AminityIds", "Aminity not found!");
                    return View();
                }
            }


            if (!ModelState.IsValid)
            {
                return View();
            }


            product.ProductImages = new List<ProductImage>();


            if (product.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "Poster file is required");
            }
            else
            {
                if (product.PosterFile.ContentType != "image/png" && product.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }

                if (product.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size can not be more than 2MB!");
                    return View();
                }

                string newFileName = Guid.NewGuid().ToString() + product.PosterFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/products", newFileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    product.PosterFile.CopyTo(stream);
                }

                ProductImage poster = new ProductImage
                {
                    Image = newFileName,
                    IsPoster= true,
                };

                product.ProductImages.Add(poster);
            }

            if (product.ImageFiles != null)
            {
                foreach (var file in product.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        continue;
                    }

                    if (file.Length > 2097152)
                    {
                        continue;
                    }

                    ProductImage image = new ProductImage
                    {
                        IsPoster = false,
                        Image = FileManager.SaveImage(_env.WebRootPath, "uploads/products", file)
                    };

                    product.ProductImages.Add(image);
                }
            }



            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).Include(x => x.productAminities).ThenInclude(x=>x.Aminity).FirstOrDefault(x => x.Id == id);

            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Aminities = _context.Aminities.ToList();
            ViewBag.Teams = _context.Teams.ToList();
            ViewBag.Statuses = _context.Statuses.ToList();


            product.AminityIds = product.productAminities.Select(x => x.AminityId).ToList();

            if (product == null) return NotFound();

            return View(product);
            
        }


        [HttpPost]

        public IActionResult Edit(Product product)
        {
            if (!_context.Cities.Any(x => x.Id == product.CityId)) ModelState.AddModelError("CityId", "City not found!");
            if (!_context.Categories.Any(x => x.Id == product.CategoryId)) ModelState.AddModelError("CaregoryId", "Category not found!");
            if (!_context.Statuses.Any(x => x.Id == product.StatusId)) ModelState.AddModelError("StatusId", "Status not found!");
            if (!_context.Teams.Any(x => x.Id == product.TeamId)) ModelState.AddModelError("TeamId", "Team not found!");


            if (!ModelState.IsValid) return View();

            Product existProduct = _context.Products.Include(x => x.ProductImages).Include(x => x.productAminities).FirstOrDefault(x => x.Id == product.Id);


            if (existProduct == null) return NotFound();


            if (product.PosterFile != null)
            {
                if (product.PosterFile.ContentType != "image/png" && product.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File type can be only jpeg,jpg or png!");
                    return View();
                }

                if (product.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size can not be more than 2MB!");
                    return View();
                }


                ProductImage poster = existProduct.ProductImages.FirstOrDefault(x => x.IsPoster == true);
                string newFileName = FileManager.SaveImage(_env.WebRootPath, "uploads/products", product.PosterFile);

                if (poster == null)
                {
                    poster = new ProductImage
                    {
                        IsPoster = true,
                        Image = newFileName,
                        ProductId = product.Id
                    };

                    _context.ProductImages.Add(poster);
                }
                else
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", poster.Image);
                    poster.Image = newFileName;
                }
            }
            existProduct.productAminities.RemoveAll((x => !product.AminityIds.Contains(x.AminityId)));

            if (product.AminityIds != null)
            {
                foreach (var aminityId in product.AminityIds.Where(x => !existProduct.productAminities.Any(bt => bt.AminityId == x)))
                {
                    ProductAminity productAminity = new ProductAminity
                    {
                        AminityId = aminityId,
                        ProductId = product.Id
                    };
                    existProduct.productAminities.Add(productAminity);
                }
            }


            existProduct.ProductImages.RemoveAll(x => x.IsPoster == false && !product.ProductImageIds.Contains(x.Id));

            if (product.ImageFiles != null)
            {
                foreach (var file in product.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        continue;
                    }

                    if (file.Length > 2097152)
                    {
                        continue;
                    }

                    ProductImage image = new ProductImage
                    {
                        IsPoster = false,
                        Image = FileManager.SaveImage(_env.WebRootPath, "uploads/products", file)
                    };

                    existProduct.ProductImages.Add(image);
                }
            }

            existProduct.Name = product.Name;
            existProduct.BuiltDate = product.BuiltDate;
            existProduct.Price = product.Price;
            existProduct.HomeArea = product.HomeArea;
            existProduct.BedCount = product.BedCount;
            existProduct.RoomCount   = product.RoomCount;
            existProduct.BathCount = product.BathCount;
            existProduct.Description = product.Description;
            existProduct.CategoryId = product.CategoryId;
            existProduct.CityId = product.CityId;
            existProduct.CityId = product.CityId;
            existProduct.StatusId = product.StatusId;
            existProduct.CreatedDate = product.CreatedDate;
            existProduct.Location = product.Location;
            existProduct.IsFeature = product.IsFeature;
            
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult DeleteProduct(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult DeleteFetch(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product == null) return Json(new { status = 404 });

            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { status = 500 });
            }

            List<ProductImage> images = product.ProductImages.Where(x => x.ProductId == id).ToList();
            foreach (ProductImage img in images)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "uploads/products", img.Image);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
            return Json(new { status = 200 });
        }

    }
}
