﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // REGISTER  
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            // CreateAsync is an Async method, so get .Result first and set it to result
            var result = _userManager.CreateAsync(new ApplicationUser() { Email = model.Email, UserName = model.Email }, model.Password).Result;
            
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);  
                }
                return View(model);
            }
            
            return RedirectToAction("Index","Home");
        }





        //LOGIN

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login"); 
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login", model);

            var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).Result;
            if (!result.Succeeded)
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return RedirectToAction("Index", "Item");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }




    
    
    }
}
