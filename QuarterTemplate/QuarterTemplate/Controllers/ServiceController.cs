using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ServiceViewModel serviceVM = new ServiceViewModel
            {
                Services = _context.Services.OrderBy(x => x.Order).ToList()
            };
            return View(serviceVM);
        }
    }
}
