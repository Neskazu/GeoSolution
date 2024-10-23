namespace CityInfo.Models
{
    public class CityModelTypeMapping
    {
        public int CityObjectModelId {  get; set; }
        public CityObjectModel CityObjectModel { get; set; }
        public int CityObjectTypeId { get; set; }
        public CityObjectType CityObjectType { get; set; }
       //pk for ef
        public override bool Equals(object obj)
        {
            if (obj is CityModelTypeMapping other)
            {
                return this.CityObjectModelId == other.CityObjectModelId && this.CityObjectTypeId == other.CityObjectTypeId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (CityObjectModelId, CityObjectTypeId).GetHashCode();
        }
    }
}
