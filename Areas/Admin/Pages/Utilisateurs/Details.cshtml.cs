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
    public class DetailsModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DetailsModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
