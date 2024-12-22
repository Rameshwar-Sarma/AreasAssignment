using CustomerSupportManagementSystem.Areas.Customer.Models;
using CustomerSupportManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportManagementSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([ModelBinder]RegisterCustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = customer.Email,
                    Email = customer.Email
                };
                var result = await _userManager.CreateAsync(user, customer.Password);

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

            return RedirectToAction("Index", "Ticket");
        }
    }
}
