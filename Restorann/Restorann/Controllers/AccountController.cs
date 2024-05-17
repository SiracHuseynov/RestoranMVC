using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restorann.Core.Models;
using Restorann.ViewModels;

namespace Restorann.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVm)
        {
            AppUser appUser = null;

            appUser = await _userManager.FindByNameAsync(memberRegisterVm.Username);

            if(appUser != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            appUser = await _userManager.FindByEmailAsync(memberRegisterVm.Email);

            if (appUser != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            appUser = new AppUser() 
            { 
                UserName = memberRegisterVm.Username,
                FullName = memberRegisterVm.FullName,
                Email = memberRegisterVm.Email,
            };

            var result = await _userManager.CreateAsync(appUser, memberRegisterVm.Password);


            if(!result.Succeeded)
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVm memberLoginVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var appUser = await _userManager.FindByEmailAsync(memberLoginVm.Email);

            if(appUser == null)
            {
                ModelState.AddModelError("Email", "Email or password is invalid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, memberLoginVm.Password, memberLoginVm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Email or password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
