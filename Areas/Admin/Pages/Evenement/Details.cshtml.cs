using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Areas_Admin_Pages_Evenement
{
    public class DetailsModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public DetailsModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public EvenementModel EvenementModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementmodel = await _context.Evenements.FirstOrDefaultAsync(m => m.Id == id);
            if (evenementmodel == null)
            {
                return NotFound();
            }
            else
            {
                EvenementModel = evenementmodel;
            }
            return Page();
        }
    }
}
