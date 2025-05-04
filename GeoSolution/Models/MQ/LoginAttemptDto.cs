namespace GeoSolution.Models.MQ
{
    public class LoginAttemptDto
    {
        public string Event { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
