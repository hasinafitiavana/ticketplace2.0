using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ticketplace.Data;
using ticketplace.Models;

namespace TicketPlace2._0.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilisateursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateurs.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurModel = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateurModel == null)
            {
                return NotFound();
            }

            return View(utilisateurModel);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,MotDePasse,DateDeNaissance,OnCreate,OnUpdate")] UtilisateurModel utilisateurModel)
        {
            if (ModelState.IsValid)
            {
                
                utilisateurModel.OnCreate = DateTime.UtcNow;
                utilisateurModel.OnUpdate = DateTime.UtcNow;
                // Convertir DateDeNaissance en UTC
                utilisateurModel.DateDeNaissance = DateTime.SpecifyKind(utilisateurModel.DateDeNaissance, DateTimeKind.Utc);
                _context.Add(utilisateurModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateurModel);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurModel = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurModel == null)
            {
                return NotFound();
            }
            return View(utilisateurModel);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,MotDePasse,DateDeNaissance,OnCreate,OnUpdate")] UtilisateurModel utilisateurModel)
        {
            if (id != utilisateurModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    utilisateurModel.OnCreate = DateTime.UtcNow;
                    utilisateurModel.OnUpdate = DateTime.UtcNow;
                    utilisateurModel.DateDeNaissance = DateTime.SpecifyKind(utilisateurModel.DateDeNaissance, DateTimeKind.Utc);
                    _context.Update(utilisateurModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurModelExists(utilisateurModel.Id))
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
            return View(utilisateurModel);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateurModel = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateurModel == null)
            {
                return NotFound();
            }

            return View(utilisateurModel);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilisateurModel = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurModel != null)
            {
                _context.Utilisateurs.Remove(utilisateurModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurModelExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
