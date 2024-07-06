using GamesZone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesZone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() { Name = role.RoleName};
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var msg in result.Errors)
                    {
                        ModelState.AddModelError("", msg.Description);
                    }
                }
            }
            return View(role);
        }
    }
}
