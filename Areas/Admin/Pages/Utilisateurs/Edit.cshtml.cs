using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ticketplace.Data;
using ticketplace.Models;

namespace TicketPlace2._0.Areas_Admin_Pages_Utilisateurs
{
    public class EditModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public EditModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UtilisateurModel UtilisateurModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurmodel =  await _context.Utilisateurs.FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateurmodel == null)
            {
                return NotFound();
            }
            UtilisateurModel = utilisateurmodel;
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
            UtilisateurModel.DateDeNaissance = DateTime.SpecifyKind(UtilisateurModel.DateDeNaissance, DateTimeKind.Utc);

            _context.Attach(UtilisateurModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurModelExists(UtilisateurModel.Id))
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

        private bool UtilisateurModelExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
