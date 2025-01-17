using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ticketplace.Models;
using ticketplace.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TicketPlace2._0.Controllers
{
    
    public class AccountController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Affiche la vue de login
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var getUser = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Email == model.Email && u.MotDePasse == model.Password);
                if (getUser != null)
                {
                    //save user data in session
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(getUser));
                    // Création des revendications utilisateur
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, getUser.Nom + " " + getUser.Prenom),
                        new Claim(ClaimTypes.Role, getUser.EstAdmin ? "Admin" : "User"),
                        new Claim(ClaimTypes.NameIdentifier, getUser.Id.ToString()),
                    };

                    // Création de l'identité des revendications avec le schéma d'authentification par cookies
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Propriétés d'authentification
                    var authProperties = new AuthenticationProperties
                    {
                        // Redirection après la connexion
                        // RedirectUri = Url.Action("Index", "Home")
                    };

                    // Connexion de l'utilisateur et création du cookie d'authentification
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                     if (getUser.EstAdmin)
                    {
                        // Redirect to the Admin area
                        // return RedirectToAction("Index", "Utilisateurs", new { area = "Admin" });
                        return Redirect("/admin/utilisateurs");

                    }
                    else
                    {
                        // Redirect to the Home page for regular users
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Si les informations d'identification sont incorrectes, ajouter une erreur de modèle
                    ModelState.AddModelError(string.Empty, "Login failed: Invalid email or password.");
                }
            }
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            // Afficher la vue de refus d'accès
            return View();
        }
    }
}