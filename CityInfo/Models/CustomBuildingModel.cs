using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Models
{
    public class CustomBuildingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Geometry { get; set; }
        public string? BuildingType { get; set; }
        public string Description { get; set; }
        public string? CurrentStatus { get; set; }
        public string? OpeningHours { get; set; }
        public string? Color { get; set; } // building color
        [ForeignKey("GisOsmBuildingId")]
        public int GisOsmBuildingId { get; set; } // id for geodata
        public GisOsmBuildingModel? GisOsmBuilding { get; set; }
        //public virtual ICollection<EntranceDataModel> Entrances { get; set; }  = new List<EntranceDataModel>();
    }
}
