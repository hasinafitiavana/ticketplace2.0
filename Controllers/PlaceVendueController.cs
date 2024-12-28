using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketPlace2._0.Models;
using ticketplace.Data;

namespace TicketPlace2._0.Controllers
{
    public class PlaceVendueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceVendueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaceVendue
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlaceVendues.Include(p => p.Evenement).Include(p => p.TypePlace).Include(p => p.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlaceVendue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeVendueModel = await _context.PlaceVendues
                .Include(p => p.Evenement)
                .Include(p => p.TypePlace)
                .Include(p => p.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeVendueModel == null)
            {
                return NotFound();
            }

            return View(placeVendueModel);
        }

        // GET: PlaceVendue/Create
        public IActionResult Create()
        {
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description");
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type");
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "Id", "Email");
            return View();
        }

        // POST: PlaceVendue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvenementId,TypePlaceId,UtilisateurId,NombreDePlaces,Prix,OnCreate,OnUpdate")] PlaceVendueModel placeVendueModel)
        {
            if (ModelState.IsValid)
            {
                placeVendueModel.OnCreate = DateTime.UtcNow;
                placeVendueModel.OnUpdate = DateTime.UtcNow;
                _context.Add(placeVendueModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", placeVendueModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", placeVendueModel.TypePlaceId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "Id", "Email", placeVendueModel.UtilisateurId);
            return View(placeVendueModel);
        }

        // GET: PlaceVendue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeVendueModel = await _context.PlaceVendues.FindAsync(id);
            if (placeVendueModel == null)
            {
                return NotFound();
            }
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", placeVendueModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", placeVendueModel.TypePlaceId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "Id", "Email", placeVendueModel.UtilisateurId);
            return View(placeVendueModel);
        }

        // POST: PlaceVendue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EvenementId,TypePlaceId,UtilisateurId,NombreDePlaces,Prix,OnCreate,OnUpdate")] PlaceVendueModel placeVendueModel)
        {
            if (id != placeVendueModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    placeVendueModel.OnCreate = DateTime.UtcNow;
                    placeVendueModel.OnUpdate = DateTime.UtcNow;
                    _context.Update(placeVendueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceVendueModelExists(placeVendueModel.Id))
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
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", placeVendueModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", placeVendueModel.TypePlaceId);
            ViewData["UtilisateurId"] = new SelectList(_context.Utilisateurs, "Id", "Email", placeVendueModel.UtilisateurId);
            return View(placeVendueModel);
        }

        // GET: PlaceVendue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeVendueModel = await _context.PlaceVendues
                .Include(p => p.Evenement)
                .Include(p => p.TypePlace)
                .Include(p => p.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeVendueModel == null)
            {
                return NotFound();
            }

            return View(placeVendueModel);
        }

        // POST: PlaceVendue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeVendueModel = await _context.PlaceVendues.FindAsync(id);
            if (placeVendueModel != null)
            {
                _context.PlaceVendues.Remove(placeVendueModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceVendueModelExists(int id)
        {
            return _context.PlaceVendues.Any(e => e.Id == id);
        }
    }
}
