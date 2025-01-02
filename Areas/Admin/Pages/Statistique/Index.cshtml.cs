using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.Areas.Admin.Pages.Statistique
{
    public class Index : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public Index(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
       public IList<EvenementModel> EvenementModel { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int currentPage = 1)
        {
            int pageSize = 10;

            var query = _context.Evenements.Include(e => e.Espace).AsNoTracking();

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            EvenementModel = await query
                .OrderBy(e => e.Id) 
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            CurrentPage = currentPage;
        }
    }
}