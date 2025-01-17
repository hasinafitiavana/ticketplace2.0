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
using TicketPlace2._0.Service;
using Newtonsoft.Json;
using ticketplace.Models;
using System.Security.Claims;

namespace TicketPlace2._0.Controllers
{
    public class EvenementTypePlaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TicketService _ticketService;
        private readonly EmailService _emailService;

        public EvenementTypePlaceController(ApplicationDbContext context, TicketService ticketService, EmailService emailService)
        {
            _context = context;
            _ticketService = ticketService;
            _emailService = emailService;
        }

        // GET: EvenementTypePlace
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EvenementTypePlaces.Include(e => e.Evenement).Include(e => e.TypePlace);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EvenementTypePlace/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementTypePlaceModel = await _context.EvenementTypePlaces
                .Include(e => e.Evenement)
                .Include(e => e.TypePlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenementTypePlaceModel == null)
            {
                return NotFound();
            }

            return View(evenementTypePlaceModel);
        }

        // GET: EvenementTypePlace/Create
        public IActionResult Create()
        {

            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(evenements, "Id", "Nom");
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type");
            return View();
        }

        // POST: EvenementTypePlace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvenementId,TypePlaceId,NombreDePlaces,Prix,Emplacements,OnCreate,OnUpdate")] EvenementTypePlaceModel evenementTypePlaceModel)
        {
            Console.WriteLine(evenementTypePlaceModel.Emplacements + "-----------------");
            if (ModelState.IsValid)
            {
                _context.Add(evenementTypePlaceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(evenements, "Id", "Description", evenementTypePlaceModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", evenementTypePlaceModel.TypePlaceId);
            return View(evenementTypePlaceModel);
        }

        // GET: EvenementTypePlace/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementTypePlaceModel = await _context.EvenementTypePlaces.FindAsync(id);
            if (evenementTypePlaceModel == null)
            {
                return NotFound();
            }
            
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(evenements, "Id", "Nom", evenementTypePlaceModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", evenementTypePlaceModel.TypePlaceId);
            return View(evenementTypePlaceModel);
        }

        // POST: EvenementTypePlace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EvenementId,TypePlaceId,NombreDePlaces,Prix,Emplacements,OnCreate,OnUpdate")] EvenementTypePlaceModel evenementTypePlaceModel)
        {
            if (id != evenementTypePlaceModel.Id)
            {
                return NotFound();
            }
            Console.WriteLine( "-----------------" +evenementTypePlaceModel.Emplacements );

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evenementTypePlaceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenementTypePlaceModelExists(evenementTypePlaceModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
            ViewData["EvenementTypePlace"] = _context.EvenementTypePlaces.ToList();
            ViewData["EvenementId"] = new SelectList(evenements, "Id", "Description", evenementTypePlaceModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", evenementTypePlaceModel.TypePlaceId);
            return View(evenementTypePlaceModel);
        }

        // GET: EvenementTypePlace/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementTypePlaceModel = await _context.EvenementTypePlaces
                .Include(e => e.Evenement)
                .Include(e => e.TypePlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenementTypePlaceModel == null)
            {
                return NotFound();
            }

            return View(evenementTypePlaceModel);
        }

        // POST: EvenementTypePlace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evenementTypePlaceModel = await _context.EvenementTypePlaces.FindAsync(id);
            if (evenementTypePlaceModel != null)
            {
                _context.EvenementTypePlaces.Remove(evenementTypePlaceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ChoixPlace(int idEvenement, int? idPlaceAcheter)
        {
            var evenement = await _context.Evenements.Include(e => e.Espace).FirstOrDefaultAsync(e => e.Id == idEvenement);
            var evenementTypePlaces = await _context.EvenementTypePlaces.Include(e => e.TypePlace).Where(e => e.EvenementId == idEvenement).ToListAsync();
            var typePlaceAlreadySelected = await _context.PlaceVendues.Include(e => e.Utilisateur).Where(e => e.EvenementId == idEvenement).ToListAsync();
            if (evenement == null)
            {
                return NotFound();
            }
            var utilisateurId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewData["UtilisateurId"] = utilisateurId;
            ViewData["EvenementTypePlaces"] = evenementTypePlaces;
            ViewData["TypePlaceAlreadySelected"] = typePlaceAlreadySelected;
            if (idPlaceAcheter != null)
            {
                ViewData["IdPlaceAcheter"] = idPlaceAcheter;
            }

            return View(evenement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertPlaceVendu([Bind("Id,EvenementId,TypePlaceId,UtilisateurId,TypeReservation,NumeroDePlace,Prix,OnCreate,OnUpdate")] PlaceVendueModel placeVendueModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placeVendueModel);
                await _context.SaveChangesAsync();
                var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(e => e.Id == placeVendueModel.UtilisateurId);
                var evenement = await _context.Evenements.FirstOrDefaultAsync(e => e.Id == placeVendueModel.EvenementId);
                var typePlace = await _context.TypePlaces.FirstOrDefaultAsync(e => e.Id == placeVendueModel.TypePlaceId);
                if(placeVendueModel.TypeReservation == "ACHETER"){
                    _emailService.mailBody("Achat de ticket", utilisateur.Email, evenement.Nom, evenement.Date.ToString(), placeVendueModel.NumeroDePlace.ToString(), placeVendueModel.Prix.ToString(),placeVendueModel.EvenementId); 
                    //suprimer la place de la liste des places disponibles
                    var evenementTypePlaces = await _context.PlaceVendues
                        .Where(e => e.NumeroDePlace == placeVendueModel.NumeroDePlace 
                                    && e.EvenementId == placeVendueModel.EvenementId 
                                    && e.TypeReservation == "RESERVER")
                        .ToListAsync();
                    if (evenementTypePlaces.Any())
                    {
                        _context.RemoveRange(evenementTypePlaces);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction("ChoixPlace", new { idEvenement = placeVendueModel.EvenementId });
            }
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", placeVendueModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", placeVendueModel.TypePlaceId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "Id", "Email", placeVendueModel.UtilisateurId);
            return RedirectToAction("ChoixPlace", new { idEvenement = placeVendueModel.EvenementId });
        }
        
        [HttpGet]
        public async Task<IActionResult> DownloadTicket(int numeroTicket, int idEvenement)
        {
            var placeVendu = await _context.PlaceVendues
                                           .Include(e => e.Evenement)
                                           .Include(e => e.TypePlace)
                                           .Include(e => e.Utilisateur)
                                           .FirstOrDefaultAsync(e => e.NumeroDePlace == numeroTicket && e.EvenementId == idEvenement);

            if (placeVendu == null)
            {
                return NotFound("Ticket not found.");
            }

            var pdfBytes = _ticketService.GeneratePdfWithQrCode(placeVendu, "http://localhost:5125/EvenementTypePlace/ChoixPlace?idEvenement=" + placeVendu.EvenementId + "&idPlaceAcheter=" + placeVendu.NumeroDePlace);
            return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
        }
        private bool EvenementTypePlaceModelExists(int id)
        {
            return _context.EvenementTypePlaces.Any(e => e.Id == id);
        }
    }
}
