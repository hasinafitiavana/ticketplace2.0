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
    public class EvenementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvenementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evenement
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Evenements.Include(e => e.Espace);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Evenement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementModel = await _context.Evenements
                .Include(e => e.Espace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenementModel == null)
            {
                return NotFound();
            }

            return View(evenementModel);
        }

        // GET: Evenement/Create
        public IActionResult Create()
        {
            ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom");
            return View();
        }

        // POST: Evenement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EspaceId,Nom,Description,Date,Heure,Lieu,ImagePath,OnCreate,OnUpdate")] EvenementModel evenementModel,IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(imagesDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    evenementModel.ImagePath = $"/images/{fileName}";
                }
                evenementModel.Date = DateTime.SpecifyKind(evenementModel.Date, DateTimeKind.Utc);
                evenementModel.OnCreate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                evenementModel.OnUpdate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                _context.Add(evenementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom", evenementModel.EspaceId);
            return View(evenementModel);
        }

        // GET: Evenement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementModel = await _context.Evenements.FindAsync(id);
            if (evenementModel == null)
            {
                return NotFound();
            }
            ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom", evenementModel.EspaceId);
            return View(evenementModel);
        }

        // POST: Evenement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EspaceId,Nom,Description,Date,Heure,Lieu,ImagePath,OnCreate,OnUpdate")] EvenementModel evenementModel, IFormFile imageFile)
        {
            if (id != evenementModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     if (imageFile != null && imageFile.Length > 0)
                    {
                        var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                        if (!Directory.Exists(imagesDirectory))
                        {
                            Directory.CreateDirectory(imagesDirectory);
                        }

                        var fileName = Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(imagesDirectory, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        evenementModel.ImagePath = $"/images/{fileName}";
                    }
                    evenementModel.Date = DateTime.SpecifyKind(evenementModel.Date, DateTimeKind.Utc);
                    evenementModel.OnUpdate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                    
                    _context.Update(evenementModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenementModelExists(evenementModel.Id))
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
            ViewData["EspaceId"] = new SelectList(_context.Espaces, "Id", "Nom", evenementModel.EspaceId);
            return View(evenementModel);
        }

        // GET: Evenement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenementModel = await _context.Evenements
                .Include(e => e.Espace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenementModel == null)
            {
                return NotFound();
            }

            return View(evenementModel);
        }

        // POST: Evenement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evenementModel = await _context.Evenements.FindAsync(id);
            if (evenementModel != null)
            {
                _context.Evenements.Remove(evenementModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListEvenementPaginated(string search, int? page)
        {
            var evenements = from m in _context.Evenements
                            select m;

            if (!String.IsNullOrEmpty(search))
            {
                evenements = evenements.Where(s => s.Nom.Contains(search));
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            // Get the total count of items
            int totalCount = await evenements.CountAsync();

            // Get the items for the current page
            var items = await evenements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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
