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
    public class EvenementTypePlaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvenementTypePlaceController(ApplicationDbContext context)
        {
            _context = context;
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
            ViewData["EvenementId"] = new SelectList(evenements, "Id", "Nom");
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type");
            return View();
        }

        // POST: EvenementTypePlace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvenementId,TypePlaceId,NombreDePlaces,Prix,OnCreate,OnUpdate")] EvenementTypePlaceModel evenementTypePlaceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evenementTypePlaceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var evenements = _context.Evenements.Include(e=>e.Espace).ToList();

            ViewData["Evenement"] = evenements;
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
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", evenementTypePlaceModel.EvenementId);
            ViewData["TypePlaceId"] = new SelectList(_context.TypePlaces, "Id", "Type", evenementTypePlaceModel.TypePlaceId);
            return View(evenementTypePlaceModel);
        }

        // POST: EvenementTypePlace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EvenementId,TypePlaceId,NombreDePlaces,Prix,OnCreate,OnUpdate")] EvenementTypePlaceModel evenementTypePlaceModel)
        {
            if (id != evenementTypePlaceModel.Id)
            {
                return NotFound();
            }

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
            ViewData["EvenementId"] = new SelectList(_context.Evenements, "Id", "Description", evenementTypePlaceModel.EvenementId);
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

        private bool EvenementTypePlaceModelExists(int id)
        {
            return _context.EvenementTypePlaces.Any(e => e.Id == id);
        }
    }
}
