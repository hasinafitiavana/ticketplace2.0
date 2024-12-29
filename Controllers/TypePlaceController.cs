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
    public class TypePlaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypePlaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypePlace
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypePlaces.ToListAsync());
        }

        // GET: TypePlace/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePlaceModel = await _context.TypePlaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typePlaceModel == null)
            {
                return NotFound();
            }

            return View(typePlaceModel);
        }

        // GET: TypePlace/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePlace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Couleurs")] TypePlaceModel typePlaceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typePlaceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typePlaceModel);
        }

        // GET: TypePlace/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePlaceModel = await _context.TypePlaces.FindAsync(id);
            if (typePlaceModel == null)
            {
                return NotFound();
            }
            return View(typePlaceModel);
        }

        // POST: TypePlace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Couleurs")] TypePlaceModel typePlaceModel)
        {
            if (id != typePlaceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typePlaceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypePlaceModelExists(typePlaceModel.Id))
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
            return View(typePlaceModel);
        }

        // GET: TypePlace/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePlaceModel = await _context.TypePlaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typePlaceModel == null)
            {
                return NotFound();
            }

            return View(typePlaceModel);
        }

        // POST: TypePlace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typePlaceModel = await _context.TypePlaces.FindAsync(id);
            if (typePlaceModel != null)
            {
                _context.TypePlaces.Remove(typePlaceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypePlaceModelExists(int id)
        {
            return _context.TypePlaces.Any(e => e.Id == id);
        }
    }
}
