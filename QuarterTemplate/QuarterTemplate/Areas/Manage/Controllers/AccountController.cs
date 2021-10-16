using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterTemplate.Areas.Manage.ViewModels;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
           _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {

            //return Ok(new
            //{
            //    UserName = User.Identity.Name,
            //    IsLogged = User.Identity.IsAuthenticated
            //});



            //AppUser admin = new AppUser
            //{
            //    UserName = "SuperAdmin",
            //    Fullname = "Super Admin"
            //};

            // var result = await _userManager.CreateAsync(admin, "Admin123");



            //IdentityRole role1 = new IdentityRole("SuperAdmin");
            //await _roleManager.CreateAsync(role1);
            //IdentityRole role2 = new IdentityRole("Admin");
            //await _roleManager.CreateAsync(role2);
            //IdentityRole role3 = new IdentityRole("Member");

            //var result1 = await _roleManager.CreateAsync(role3);

            //AppUser appUser = await _userManager.FindByNameAsync("SuperAdmin");
            //await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

            return View();
        }

        [HttpGet]
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser admin = _userManager.Users.FirstOrDefault(x => x.UserName == loginVM.UserName && x.IsAdmin == true);

            if (admin == null)
            {
                ModelState.AddModelError("", "istifadeci adi ve ya sifre yanlisdir!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, loginVM.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "istifadeci adi ve ya sifre yanlisdir!");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateAdmin()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> CreateAdmin(AdminViewModel AdminVM)
        {
            if (!ModelState.IsValid) return View();

            //AppUser admin = _userManager.Users.FirstOrDefault(x => x.UserName == AdminVM.FullName);


            AppUser admin = await _userManager.FindByNameAsync(AdminVM.UserName);
            if (admin != null)
            {
                ModelState.AddModelError("UserName", "UserName has  already been taken!");
                return View();
            }

            admin = await _userManager.FindByEmailAsync(AdminVM.Email);
            if (admin != null)
            {
                ModelState.AddModelError("Email", "Email has already been taken!");
                return View();
            }


             admin = new AppUser
            {
                IsAdmin = true,
                Fullname = AdminVM.FullName,
                UserName = AdminVM.UserName,
                Email = AdminVM.Email

            };

           var result =  await _userManager.CreateAsync(admin, AdminVM.Password);
           var roleResult =  await _userManager.AddToRoleAsync(admin, "Admin");


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }


            return RedirectToAction("index", "dashboard");
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> EditAdmin(string id)
        {
            AppUser admin = await _userManager.FindByIdAsync(id);
            if (admin == null) return NotFound();

            return View(admin);
        }



        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> EditAdmin(string id, AdminViewModel AdminVM)
        {

            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByIdAsync(id);

            if (existUser == null) return NotFound();

            existUser.Fullname = AdminVM.FullName;
            existUser.UserName = AdminVM.UserName;
            existUser.Email = AdminVM.Email;

            await _userManager.UpdateAsync(existUser);
            
            return RedirectToAction("index");
        }


        public async Task<IActionResult> Delete(string name)
        {

            AppUser deleteadmin = _userManager.Users.FirstOrDefault(x => x.UserName == name);

            await _userManager.DeleteAsync(deleteadmin);


            return RedirectToAction("index");
        }


    }
}
