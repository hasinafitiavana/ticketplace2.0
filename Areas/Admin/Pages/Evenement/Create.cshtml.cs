using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_Evenement
{
    public class CreateModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public CreateModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom");
            return Page();
        }

        [BindProperty]
        public EvenementModel EvenementModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            } 
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                // Vérifier si le répertoire existe, sinon le créer
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                // Générer un nom de fichier unique pour éviter les conflits
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(imagesDirectory, fileName);

                // Enregistrer le fichier sur le serveur
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Assigner le chemin du fichier à la propriété ImagePath
                EvenementModel.ImagePath = $"/images/{fileName}";
            }

            EvenementModel.OnCreate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            EvenementModel.OnUpdate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            EvenementModel.Date = DateTime.SpecifyKind(EvenementModel.Date, DateTimeKind.Utc);
            _context.Evenements.Add(EvenementModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
