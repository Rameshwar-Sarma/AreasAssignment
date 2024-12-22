
using CustomerSupportManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportManagementSystem.Data
{
    public class TicketContext : IdentityDbContext<ApplicationUser>
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
