using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Team> Teams = _context.Teams.OrderBy(x => x.Order).ToList();
            return View(Teams);
        }
    }
}
