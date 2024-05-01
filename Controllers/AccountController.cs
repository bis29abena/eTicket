using eTicket.Data;
using eTicket.Data.Static;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _db.Users.Where(user => user.UserName != "admin").ToListAsync();
            return View(users);
        }
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        //Login Into the movies Account
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginCredentials)
        {
            if (!ModelState.IsValid) return View(loginCredentials);

            var userEmail = await _userManager.FindByEmailAsync(loginCredentials.EmailAddress);
            if (userEmail != null)
            {
                var userPasswordCheck = await _userManager.CheckPasswordAsync(userEmail, loginCredentials.Password);
                if (userPasswordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(userEmail, loginCredentials.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                else
                {
                    TempData["Error"] = "Please User Credentials is Wrong. Please Try Again";

                    return View(loginCredentials);
                }
            }

            TempData["Error"] = "Please User Credentials is Wrong. Please Try Again";

            return View(loginCredentials);
        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }


        //Sign Up for the movies Account
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerCredentials)
        {
            if (!ModelState.IsValid) return View(registerCredentials);

            var userEmail = await _userManager.FindByEmailAsync(registerCredentials.EmailAddress);

            if (userEmail != null)
            {
                TempData["Error"] = "Email already exist please change it";
                return View(registerCredentials);
            }

            var username = Tools.GenerateUserName(registerCredentials.FirstName, registerCredentials.MiddleName, registerCredentials.SurName);

            var newUser = new ApplicationUser()
            {
                FirstName = registerCredentials.FirstName,
                SurnName = registerCredentials.SurName,
                MiddleName = registerCredentials.MiddleName,
                Email = registerCredentials.EmailAddress,
                UserName = username
            };

            var response = await _userManager.CreateAsync(newUser, registerCredentials.Password);

            if (!response.Succeeded)
            {
                {
                    TempData["Error"] = "Password Should be alphanumeric and contains characters and numbers";
                    return View(registerCredentials);
                }
            }

            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRole.User);
            }

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
