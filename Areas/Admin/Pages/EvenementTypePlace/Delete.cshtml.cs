using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_EvenementTypePlace
{
    public class DeleteModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DeleteModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EvenementTypePlaceModel EvenementTypePlaceModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementtypeplacemodel = await _context.EvenementTypePlaces.FirstOrDefaultAsync(m => m.Id == id);

            if (evenementtypeplacemodel == null)
            {
                return NotFound();
            }
            else
            {
                EvenementTypePlaceModel = evenementtypeplacemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementtypeplacemodel = await _context.EvenementTypePlaces.FindAsync(id);
            if (evenementtypeplacemodel != null)
            {
                EvenementTypePlaceModel = evenementtypeplacemodel;
                _context.EvenementTypePlaces.Remove(EvenementTypePlaceModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
