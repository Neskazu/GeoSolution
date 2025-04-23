using GeoSolution.Data;
using GeoSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoSolution.Controllers
{
    public class CustomBuildingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomBuildingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var buildings = await _context.CustomBuildings.Include(b => b.Entrances)
                .ToListAsync();
            return View(buildings);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new CustomBuildingModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CustomBuildingModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var building = await _context.CustomBuildings
                .Include(b => b.Entrances)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (building == null) return NotFound();

            return View(building);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CustomBuildingModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(model.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var building = await _context.CustomBuildings.FindAsync(id);
            if (building == null) return NotFound();

            _context.CustomBuildings.Remove(building);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BuildingExists(int id)
        {
            return _context.CustomBuildings.Any(e => e.Id == id);
        }
    }
}
