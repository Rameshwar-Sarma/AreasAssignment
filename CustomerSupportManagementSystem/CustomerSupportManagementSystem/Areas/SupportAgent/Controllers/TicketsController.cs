using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerSupportManagementSystem.Data;
using CustomerSupportManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CustomerSupportManagementSystem.Areas.SupportAgent.Controllers
{
    [Area("SupportAgent")]
    public class TicketsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TicketContext _context;

        public TicketsController(TicketContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: SupportAgent/Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public IActionResult UpdateStatus()
        {
            return View();
        }

        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var ticket = await this.GetTicket(id);
            if (ticket != null)
            {
                ticket.Status= status;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<Ticket> GetTicket(int? id)
        {
            return  await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
