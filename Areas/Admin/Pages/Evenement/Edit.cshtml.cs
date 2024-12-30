using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_Evenement
{
    public class EditModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public EditModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EvenementModel EvenementModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementmodel =  await _context.Evenements.FirstOrDefaultAsync(m => m.Id == id);
            if (evenementmodel == null)
            {
                return NotFound();
            }
            EvenementModel = evenementmodel;
           ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Attach(EvenementModel).State = EntityState.Modified;

            try
            {
                EvenementModel.Date = DateTime.SpecifyKind(EvenementModel.Date, DateTimeKind.Utc);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementModelExists(EvenementModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostWithFileAsync(IFormFile imageFile)
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

                // After saving the file, update the EvenementModel as needed
                EvenementModel.ImagePath = $"/images/{fileName}";
            }
            EvenementModel.OnUpdate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            EvenementModel.Date = DateTime.SpecifyKind(EvenementModel.Date, DateTimeKind.Utc);
            _context.Attach(EvenementModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementModelExists(EvenementModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EvenementModelExists(int id)
        {
            return _context.Evenements.Any(e => e.Id == id);
        }
    }
}
