namespace GeoSolution.Models
{
    public class MapViewModel
    {
        public string GeoServerUrl { get; set; }
        public string LayerName { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public int Zoom { get; set; }
    }
}
