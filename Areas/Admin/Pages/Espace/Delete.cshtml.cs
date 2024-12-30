using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_Espace
{
    public class DeleteModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DeleteModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EspaceModel EspaceModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espacemodel = await _context.Espaces.FirstOrDefaultAsync(m => m.Id == id);

            if (espacemodel == null)
            {
                return NotFound();
            }
            else
            {
                EspaceModel = espacemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espacemodel = await _context.Espaces.FindAsync(id);
            if (espacemodel != null)
            {
                EspaceModel = espacemodel;
                _context.Espaces.Remove(EspaceModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
