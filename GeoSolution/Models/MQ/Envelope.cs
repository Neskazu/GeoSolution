namespace GeoSolution.Models.MQ
{
    public class Envelope<T>
    {
        public string Type { get; set; }

        public T Payload { get; set; }
    }
}
