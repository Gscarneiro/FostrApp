using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fostr.Data;
using Fostr.Models;
using Microsoft.AspNetCore.Identity;
using Fostr.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Fostr.Models.ViewModels;
using System.IO;

namespace Fostr.Controllers
{
    public class AnimalTutorsController : Controller
    {
        private readonly FostrContext _context;
        private readonly UserManager<FostrUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AnimalTutorsController(FostrContext context, UserManager<FostrUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            webHostEnvironment = hostEnvironment;

        }

        // GET: AnimalTutors
        public async Task<IActionResult> Index()
        {
            ViewBag.UserLogado = _userManager.GetUserId(User); 
            var fostrContext = _context.AnimalTutor.Include(a => a.Animal);
            return View(await fostrContext.ToListAsync());
        }

        // GET: AnimalTutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalTutor = await _context.AnimalTutor
                .Include(a => a.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalTutor == null)
            {
                return NotFound();
            }

            return View(animalTutor);
        }

        // GET: AnimalTutors/Create
        public IActionResult Create()
        {
            bool UserIsLogged = _userManager.GetUserId(User) != null;
            if (UserIsLogged)
            {
                ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id");
                return View();
            }
            return NotFound();
        }

        // POST: AnimalTutors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Animal animal = new Animal
                {
                    Name = model.Name,
                    Description = model.Description,
                    Gender = model.Gender,
                    Size = model.Size,
                    Species = model.Species,
                    Race = model.Race,
                    Image = uniqueFileName
                };

                _context.Add(animal);
                await _context.SaveChangesAsync();

                AnimalTutor animalTutor = new AnimalTutor
                {
                    AnimalId = animal.Id,
                    UserId = _userManager.GetUserId(User)
                };

                _context.Add(animalTutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(AnimalViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: AnimalTutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalTutor = await _context.AnimalTutor.FindAsync(id);
            if (animalTutor == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", animalTutor.AnimalId);
            return View(animalTutor);
        }

        // POST: AnimalTutors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,UserId,Id")] AnimalTutor animalTutor)
        {
            if (id != animalTutor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalTutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalTutorExists(animalTutor.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", animalTutor.AnimalId);
            return View(animalTutor);
        }

        // GET: AnimalTutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalTutor = await _context.AnimalTutor
                .Include(a => a.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalTutor == null)
            {
                return NotFound();
            }

            return View(animalTutor);
        }

        // POST: AnimalTutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalTutor = await _context.AnimalTutor.FindAsync(id);
            _context.AnimalTutor.Remove(animalTutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalTutorExists(int id)
        {
            return _context.AnimalTutor.Any(e => e.Id == id);
        }
    }
}
