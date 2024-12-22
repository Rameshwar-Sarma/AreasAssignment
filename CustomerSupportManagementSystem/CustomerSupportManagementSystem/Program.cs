using CustomerSupportManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CustomerSupportManagementSystem.Services;
using CustomerSupportManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TicketContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Adjust according to your requirements
})
.AddEntityFrameworkStores<TicketContext>()
.AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

var app = builder.Build();

SeedApplication.SeedRoles(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=SupportAgent}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "SupportAgentArea",
    areaName: "SupportAgent",
    pattern: "SupportAgent/{controller=Tickets}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "CustomerArea",
    areaName: "Customer",
    pattern: "Customer/{controller=Tickets}/{action=Index}/{id?}");

// Default route to handle non-area specific controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
