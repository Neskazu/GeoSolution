using GeoSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoSolution.Components
{
    public class MapViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(MapViewModel model)
        {
            return View(model);
        }
    }
}
