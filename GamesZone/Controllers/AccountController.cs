using GamesZone.Models;
using GamesZone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesZone.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel New)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = New.Email;
                user.UserName = New.UserName;
                IdentityResult result = await userManager.CreateAsync(user, New.Password); // make passwork is Hash
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Logout");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(New);
        }
        public IActionResult Login(string ReturnUrl = "~/Games/Index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    //create cookie if enter a viled password
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                        return LocalRedirect(ReturnUrl);
                    else
                        ModelState.AddModelError(string.Empty, "UserName OR Password Not Correct 😢");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Enter a viled UserName And Password 😡");
                }
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegistrationViewModel New)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = New.Email;
                user.UserName = New.UserName;
                IdentityResult result = await userManager.CreateAsync(user, New.Password); // make passwork is Hash
                if (result.Succeeded)
                {
                    //add role admin
                    IdentityResult res = await userManager.AddToRoleAsync(user, "Admin");
                    if (res.Succeeded)
                    {
                        //creat cookie
                        await signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Games");

                    }
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(New);
        }
    }
}
