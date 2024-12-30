using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ticketplace.Data;
using ticketplace.Models;

namespace TicketPlace2._0.Areas_Admin_Pages_Utilisateurs
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
            return Page();
        }

        [BindProperty]
        public UtilisateurModel UtilisateurModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UtilisateurModel.DateDeNaissance = DateTime.SpecifyKind(UtilisateurModel.DateDeNaissance, DateTimeKind.Utc);

            _context.Utilisateurs.Add(UtilisateurModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
