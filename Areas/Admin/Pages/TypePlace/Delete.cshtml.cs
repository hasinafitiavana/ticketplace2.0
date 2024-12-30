using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_TypePlace
{
    public class DeleteModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DeleteModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TypePlaceModel TypePlaceModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeplacemodel = await _context.TypePlaces.FirstOrDefaultAsync(m => m.Id == id);

            if (typeplacemodel == null)
            {
                return NotFound();
            }
            else
            {
                TypePlaceModel = typeplacemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeplacemodel = await _context.TypePlaces.FindAsync(id);
            if (typeplacemodel != null)
            {
                TypePlaceModel = typeplacemodel;
                _context.TypePlaces.Remove(TypePlaceModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
