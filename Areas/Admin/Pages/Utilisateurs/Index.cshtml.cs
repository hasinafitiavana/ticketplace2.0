using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ticketplace.Data;
using ticketplace.Models;

namespace TicketPlace2._0.Areas_Admin_Pages_Utilisateurs
{
    public class IndexModel : PageModel
    {
        private readonly ticketplace.Data.ApplicationDbContext _context;

        public IndexModel(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UtilisateurModel> UtilisateurModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UtilisateurModel = await _context.Utilisateurs.ToListAsync();
        }
    }
}
