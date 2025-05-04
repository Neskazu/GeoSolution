namespace GeoSolution.Models.MQ
{
    public class LoginEvent
    {
        public int Id { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
