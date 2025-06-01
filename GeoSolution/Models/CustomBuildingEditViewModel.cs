using System.ComponentModel.DataAnnotations;

namespace GeoSolution.Models
{
    public class CustomBuildingEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? BuildingType { get; set; }
        public string Description { get; set; }
        public string? CurrentStatus { get; set; }
        public string? OpeningHours { get; set; }
        public string? Color { get; set; }

        [Required]
        public string GeometryText { get; set; } = string.Empty;

        public List<EntranceDataModel> Entrances { get; set; } = new();
    }
}
