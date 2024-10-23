namespace CityInfo.Models
{
    public class EntranceDataModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string EntranceType { get; set; } 
        public string Color { get; set; } 
        public int CustomBuildingModelId { get; set; } 
        public virtual CustomBuildingModel CustomBuilding { get; set; }
    }
}
