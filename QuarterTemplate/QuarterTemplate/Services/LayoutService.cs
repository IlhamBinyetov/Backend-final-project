using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using QuarterTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutService(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }


        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public List<SocialNetwork> GetSocialNetwork()
        {
            return _context.SocialNetworks.ToList();
        }

        public List<FavoriteViewModel> GetFavItems()
        {
            List<FavoriteViewModel> items = new List<FavoriteViewModel>();


            string itemsStr = _contextAccessor.HttpContext.Request.Cookies["ProductNames"];

         
                if (itemsStr != null)
                {
                    items = JsonConvert.DeserializeObject<List<FavoriteViewModel>>(itemsStr);

                    foreach (var item in items)
                    {
                        Product product = _context.Products.Include(c => c.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                        if (product != null)
                        {
                            item.Name = product.Name;
                            item.Price = (double)product.Price;
                            item.Image = product.ProductImages.FirstOrDefault(x => x.IsPoster == true)?.Image;
                        }
                    }
                }
            
            return items;
        }
    }
}
