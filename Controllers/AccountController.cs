using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ticketplace.Models;
using System.Threading.Tasks;

namespace TicketPlace2._0.Controllers
{
    
    public class AccountController : Controller
    {
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
                // Simuler l'authentification de l'utilisateur
                // Vous devez remplacer cette logique par une vérification réelle des informations d'identification de l'utilisateur
                if (model.Email == "test@example.com" && model.Password == "Password123")
                {
                    // Création des revendications utilisateur
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email)
                    };

                    // Création de l'identité des revendications avec le schéma d'authentification par cookies
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Propriétés d'authentification
                    var authProperties = new AuthenticationProperties
                    {
                        // Redirection après la connexion
                        RedirectUri = Url.Action("Index", "Home")
                    };

                    // Connexion de l'utilisateur et création du cookie d'authentification
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Redirection vers la page d'accueil après la connexion
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Si les informations d'identification sont incorrectes, ajouter une erreur de modèle
                    ModelState.AddModelError(string.Empty, "Login failed: Invalid email or password.");
                }
            }

            // Si nous arrivons ici, quelque chose a échoué, réafficher le formulaire avec les erreurs
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Déconnexion de l'utilisateur et suppression du cookie d'authentification
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            // Afficher la vue de refus d'accès
            return View();
        }
    }
}