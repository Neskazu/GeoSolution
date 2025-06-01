using GeoSolution.Data;
using GeoSolution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

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
            var model = new CustomBuildingEditViewModel
            {
                Id = building.Id,
                Name = building.Name,
                Description = building.Description,
                BuildingType = building.BuildingType,
                Color = building.Color,
                GeometryText = building.Geometry?.AsText() ?? "", 
                Entrances = building.Entrances.ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CustomBuildingEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var building = await _context.CustomBuildings
                .Include(b => b.Entrances)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (building == null) return NotFound();

            // 1) Мапим простые поля
            building.Name = model.Name;
            building.Description = model.Description;
            building.BuildingType = model.BuildingType;
            building.CurrentStatus = model.CurrentStatus;
            building.OpeningHours = model.OpeningHours;
            building.Color = model.Color;

            // 2) Парсим WKT в MultiPolygon
            try
            {
                // создаём GeometryFactory с нужным SRID (4326)
                var gf = NetTopologySuite.NtsGeometryServices.Instance
                            .CreateGeometryFactory(srid: 4326);
                var reader = new NetTopologySuite.IO.WKTReader(gf);
                var geom = reader.Read(model.GeometryText);

                if (geom is NetTopologySuite.Geometries.MultiPolygon mp)
                {
                    // на всякий случай принудительно ставим SRID
                    mp.SRID = gf.SRID;
                    building.Geometry = mp;

                    // И вот эта строка заставит EF заметить, что Geometry изменился:
                    _context.Entry(building)
                            .Property(b => b.Geometry)
                            .IsModified = true;
                }
                else
                {
                    ModelState.AddModelError(
                        nameof(model.GeometryText),
                        "Геометрия должна быть типа MULTIPOLYGON."
                    );
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    nameof(model.GeometryText),
                    $"Не удалось распознать WKT: {ex.Message}"
                );
                return View(model);
            }

            // 4) Сохраняем изменения
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CustomBuildings.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
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
