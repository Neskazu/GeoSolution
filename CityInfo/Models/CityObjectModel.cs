namespace CityInfo.Models
{
    public class CityObjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public List<CityModelTypeMapping> CityModelTypeMappings { get; set; } = new List<CityModelTypeMapping>();
    }
}
