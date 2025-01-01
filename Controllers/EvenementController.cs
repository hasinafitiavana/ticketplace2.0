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
        private readonly ChoixPlaceService _choixPlaceService;
        private readonly EvenementService _evenementService;

         public EvenementController(EvenementService evenementService, ChoixPlaceService choixPlaceService)
        {
            _evenementService = evenementService;
            _choixPlaceService = choixPlaceService;
        }
        public async Task<IActionResult> ListEvenementPaginated(string search, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int totalCount = await _evenementService.GetTotalCountAsync(search);
            var items = await _evenementService.GetEvenementsPaginatedAsync(search, pageNumber, pageSize);
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewData["Search"] = search;

            return View(items);
        }

        

        private bool EvenementModelExists(int id)
        {
            return _context.Evenements.Any(e => e.Id == id);
        }
    }
}
