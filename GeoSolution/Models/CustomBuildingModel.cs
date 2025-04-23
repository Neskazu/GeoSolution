using NetTopologySuite.Geometries;

namespace GeoSolution.Models
{
    public class CustomBuildingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MultiPolygon Geometry { get; set; }
        public string? BuildingType { get; set; }
        public string Description { get; set; }
        public string? CurrentStatus { get; set; }
        public string? OpeningHours { get; set; }
        public string? Color { get; set; }
        public virtual ICollection<EntranceDataModel> Entrances { get; set; } = new List<EntranceDataModel>();

    }
}
