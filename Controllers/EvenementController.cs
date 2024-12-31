using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;
using TicketPlace2._0.service;

namespace TicketPlace2._0.Controllers
{
    public class EvenementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EvenementService _evenementService;

         public EvenementController(EvenementService evenementService)
        {
            _evenementService = evenementService;
        }
        public async Task<IActionResult> ListEvenementPaginated(string search, int? page)
        {
            // var evenements = from m in _context.Evenements
            //                 select m;

            // if (!String.IsNullOrEmpty(search))
            // {
            //     evenements = evenements.Where(s => s.Nom.Contains(search));
            // }

            // int pageSize = 10;
            // int pageNumber = page ?? 1;

            // // Get the total count of items
            // int totalCount = await evenements.CountAsync();

            // // Get the items for the current page
            // var items = await evenements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // // Set ViewData or ViewBag for pagination
            // ViewData["CurrentPage"] = pageNumber;
            // ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            // ViewData["Search"] = search;

            // return View(items);
            int pageSize = 10;
            int pageNumber = page ?? 1;

            // Get the total count of items
            int totalCount = await _evenementService.GetTotalCountAsync(search);

            // Get the items for the current page
            var items = await _evenementService.GetEvenementsPaginatedAsync(search, pageNumber, pageSize);

            // Set ViewData or ViewBag for pagination
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewData["Search"] = search;

            return View(items);
        }

        public async Task<IActionResult> ChoixPlace(int idEvenement)
        {
            var evenement = await _context.Evenements.Include(e => e.Espace).FirstOrDefaultAsync(e => e.Id == idEvenement);
            var evenementTypePlaces = await _context.EvenementTypePlaces.Include(e => e.TypePlace).Where(e => e.EvenementId == idEvenement).ToListAsync();
            if (evenement == null)
            {
                return NotFound();
            }
            ViewData["EvenementTypePlaces"] = evenementTypePlaces;

            return View(evenement);
        }

        private bool EvenementModelExists(int id)
        {
            return _context.Evenements.Any(e => e.Id == id);
        }
    }
}
