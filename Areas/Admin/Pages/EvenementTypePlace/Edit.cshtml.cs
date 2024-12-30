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

namespace TicketPlace2._0.Areas_Admin_Pages_EvenementTypePlace
{
    public class EditModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public EditModel(ticketplace.Data.ApplicationDbContext context)
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

            var evenementtypeplacemodel =  await _context.EvenementTypePlaces.FirstOrDefaultAsync(m => m.Id == id);
            if (evenementtypeplacemodel == null)
            {
                return NotFound();
            }
            EvenementTypePlaceModel = evenementtypeplacemodel;
            
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description");
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Couleurs");
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

            _context.Attach(EvenementTypePlaceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementTypePlaceModelExists(EvenementTypePlaceModel.Id))
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

        private bool EvenementTypePlaceModelExists(int id)
        {
            return _context.EvenementTypePlaces.Any(e => e.Id == id);
        }
    }
}
