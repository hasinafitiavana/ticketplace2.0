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

namespace TicketPlace2._0.Areas_Admin_Pages_TypePlace
{
    public class EditModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public EditModel(ticketplace.Data.ApplicationDbContext context)
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

            var typeplacemodel =  await _context.TypePlaces.FirstOrDefaultAsync(m => m.Id == id);
            if (typeplacemodel == null)
            {
                return NotFound();
            }
            TypePlaceModel = typeplacemodel;
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

            _context.Attach(TypePlaceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypePlaceModelExists(TypePlaceModel.Id))
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

        private bool TypePlaceModelExists(int id)
        {
            return _context.TypePlaces.Any(e => e.Id == id);
        }
    }
}
