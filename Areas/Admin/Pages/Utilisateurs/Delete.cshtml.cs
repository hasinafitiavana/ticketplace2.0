using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ticketplace.Data;
using ticketplace.Models;

namespace TicketPlace2._0.Areas_Admin_Pages_Utilisateurs
{
    public class DeleteModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DeleteModel(ticketplace.Data.ApplicationDbContext context)
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

            var utilisateurmodel = await _context.Utilisateurs.FirstOrDefaultAsync(m => m.Id == id);

            if (utilisateurmodel == null)
            {
                return NotFound();
            }
            else
            {
                UtilisateurModel = utilisateurmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurmodel = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurmodel != null)
            {
                UtilisateurModel = utilisateurmodel;
                _context.Utilisateurs.Remove(UtilisateurModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
