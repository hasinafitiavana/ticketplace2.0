using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketPlace2._0.service;
using TicketPlace2._0.Service;

namespace TicketPlace2._0.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ReservationService _reservationService;
        // private readonly TicketService _ticketService;

        public ReservationController(ILogger<ReservationController> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
            // _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int utilisateurId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            int totalCount = await _reservationService.GetTotalCountAsync(search,utilisateurId);
            var items = await _reservationService.GetPlaceVendueAsync(search, pageNumber, pageSize, utilisateurId);
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewData["Search"] = search;

            return View(items);
        }

        // [HttpGet]
        // public IActionResult DownloadTicket(int idEvenement)
        // {
        //     string content = "Ticket for event " + idEvenement;
        //     if (string.IsNullOrEmpty(content))
        //     {
        //         return BadRequest("Content is required to generate the PDF.");
        //     }
        //     // var pdfBytes = _ticketService.GeneratePdfWithQrCode(content );
        //     // return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}