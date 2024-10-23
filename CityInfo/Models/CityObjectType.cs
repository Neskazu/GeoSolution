namespace CityInfo.Models
{
    public class CityObjectType
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public ICollection<CityObjectModel> CityObjectModels { get; set; }
    }
}
