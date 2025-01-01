using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ticketplace.Data;
using TicketPlace2._0.service;
using TicketPlace2._0.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<EvenementService>();
builder.Services.AddTransient<ChoixPlaceService>();
builder.Services.AddTransient<TicketService>();
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        // options.Conventions.AddAreaPageRoute("Admin", "/Utilisateurs/Index", "/admin/utilisateurs");
        // options.Conventions.AddAreaPageRoute("Admin", "/Utilisateurs", "/admin/utilisateurs");

    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de la session avant expiration
    options.Cookie.HttpOnly = true; // Rend le cookie de session accessible uniquement par HTTP
    options.Cookie.IsEssential = true; // Rend le cookie de session essentiel
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Chemin de la page de login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Chemin de la page d'accès refusé
    });

builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
