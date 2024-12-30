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
    public class IndexModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public IndexModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EspaceModel> EspaceModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            EspaceModel = await _context.Espaces.ToListAsync();
        }
    }
}
