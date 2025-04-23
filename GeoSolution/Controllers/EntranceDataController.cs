using GeoSolution.Data;
using GeoSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoSolution.Controllers
{
    public class EntranceDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntranceDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var entrances = await _context.EntranceDatas.Include(e => e.CustomBuilding).ToListAsync();
            return View(entrances);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int buildingId)
        {
            var model = new EntranceDataModel();
            model.CustomBuildingModelId = buildingId;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntranceDataModel model)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "CustomBuilding", new { id = model.CustomBuildingModelId });
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var entrance = await _context.EntranceDatas.FindAsync(id);

            if (entrance == null)
                return NotFound();

            return View(entrance);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EntranceDataModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EntranceDatas.Any(e => e.Id == model.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction("Edit", "CustomBuilding", new { id = model.CustomBuildingModelId });
            }

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var entrance = await _context.  EntranceDatas.FindAsync(id);

            if (entrance == null)
                return NotFound();

            _context.EntranceDatas.Remove(entrance);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "CustomBuilding", new { id = entrance.CustomBuildingModelId });
        }
    }
}
