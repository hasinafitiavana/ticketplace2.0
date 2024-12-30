using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketPlace2._0.Models;
using ticketplace.Data;
using Microsoft.EntityFrameworkCore;

namespace TicketPlace2._0.Areas_Admin_Pages_EvenementTypePlace
{
    public class CreateModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public CreateModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();
            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Nom");
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type");
            return Page();
        }

        [BindProperty]
        public EvenementTypePlaceModel EvenementTypePlaceModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EvenementTypePlaces.Add(EvenementTypePlaceModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
