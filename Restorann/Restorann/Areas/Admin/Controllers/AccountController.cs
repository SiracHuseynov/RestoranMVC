using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restorann.Core.Models;
using Restorann.ViewModels;

namespace Restorann.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login() 
        {
            return View();
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole identityRole = new IdentityRole("SuperAdmin");
        //    IdentityRole identityRole1 = new IdentityRole("Admin");
        //    IdentityRole identityRole2 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(identityRole);
        //    await _roleManager.CreateAsync(identityRole1);
        //    await _roleManager.CreateAsync(identityRole2);

        //    return Ok("Rollar yarandi!");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = new AppUser()
        //    {
        //        UserName = "Sirachh",
        //        FullName = "Sirac Huseynov"
        //    };

        //    await _userManager.CreateAsync(appUser, "Sirac123@");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Ok("Admin yarandi!");
        //}

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            AppUser appUser = await _userManager.FindByNameAsync(adminLoginVm.Username);

            if(appUser == null)
            {
                ModelState.AddModelError("Username", "Username or password is invalid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(appUser,adminLoginVm.Password, adminLoginVm.IsPersistent, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Username or Password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
