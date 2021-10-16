using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Member")]
        public IActionResult CreateOrder(int id)
        {
            AppUser member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            Order order = new Order
            {
                ProductId = product.Id,
                AppUserId = member.Id,
                FullName = member.Fullname,
                Email = member.Email,
                CreatedAt = DateTime.UtcNow,
                Status = Models.Enums.OrderStatus.Pending,
                Price = (double)product.Price

            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("profile", "account");
        }
        
        

    }
}
