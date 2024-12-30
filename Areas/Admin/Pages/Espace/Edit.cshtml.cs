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

namespace TicketPlace2._0.Areas_Admin_Pages_Espace
{
    public class EditModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public EditModel(ticketplace.Data.ApplicationDbContext context)
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

            var espacemodel =  await _context.Espaces.FirstOrDefaultAsync(m => m.Id == id);
            if (espacemodel == null)
            {
                return NotFound();
            }
            EspaceModel = espacemodel;
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

            _context.Attach(EspaceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspaceModelExists(EspaceModel.Id))
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

        private bool EspaceModelExists(int id)
        {
            return _context.Espaces.Any(e => e.Id == id);
        }
    }
}
