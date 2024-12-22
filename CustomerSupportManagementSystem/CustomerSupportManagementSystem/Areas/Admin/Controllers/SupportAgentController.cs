using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CustomerSupportManagementSystem.Areas.Admin.Models;
using CustomerSupportManagementSystem.Models;

namespace CustomerSupportManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupportAgentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SupportAgentController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterSupportAgent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSupportAgent([ModelBinder]RegisterSupportAgentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("SupportAgent"))
                        await _roleManager.CreateAsync(new IdentityRole("SupportAgent"));

                    await _userManager.AddToRoleAsync(user, "SupportAgent");

                    TempData["SuccessMessage"] = "Support Agent registered successfully!";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}

