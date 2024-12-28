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
    public class EspaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Espace
        public async Task<IActionResult> Index()
        {
            return View(await _context.Espaces.ToListAsync());
        }

        // GET: Espace/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espaceModel = await _context.Espaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (espaceModel == null)
            {
                return NotFound();
            }

            return View(espaceModel);
        }

        // GET: Espace/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Espace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Adresse,Ville,CodePostal,Capacite,OnCreate,OnUpdate")] EspaceModel espaceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espaceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(espaceModel);
        }

        // GET: Espace/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espaceModel = await _context.Espaces.FindAsync(id);
            if (espaceModel == null)
            {
                return NotFound();
            }
            return View(espaceModel);
        }

        // POST: Espace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Adresse,Ville,CodePostal,Capacite,OnCreate,OnUpdate")] EspaceModel espaceModel)
        {
            if (id != espaceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(espaceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspaceModelExists(espaceModel.Id))
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
            return View(espaceModel);
        }

        // GET: Espace/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espaceModel = await _context.Espaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (espaceModel == null)
            {
                return NotFound();
            }

            return View(espaceModel);
        }

        // POST: Espace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var espaceModel = await _context.Espaces.FindAsync(id);
            if (espaceModel != null)
            {
                _context.Espaces.Remove(espaceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspaceModelExists(int id)
        {
            return _context.Espaces.Any(e => e.Id == id);
        }
    }
}
