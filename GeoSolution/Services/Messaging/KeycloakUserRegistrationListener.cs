using GeoSolution.Models.MQ;
using Npgsql;
using System.Text;
using System.Text.Json;
namespace GeoSolution.Services.Messaging
{
    public class KeycloakUserRegistrationListener : BackgroundService
    {
        private readonly IMessagePublisher _publisher;
        private readonly IConfiguration _config;
        private readonly ILogger<KeycloakUserRegistrationListener> _logger;

        public KeycloakUserRegistrationListener(IMessagePublisher publisher, IConfiguration config, ILogger<KeycloakUserRegistrationListener> logger)
        {
            _publisher = publisher;
            _config = config;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = _config.GetConnectionString("KeycloakDb");
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync(stoppingToken);

            conn.Notification += async (o, e) =>
            {
                using var doc = JsonDocument.Parse(e.Payload);
                var root = doc.RootElement;
                _logger.LogInformation("Root JSON: {Json}", root.GetRawText());

                var id = root.GetProperty("id").GetString();
                var username = root.GetProperty("username").GetString();
                var email = root.TryGetProperty("email", out var emailElement) ? emailElement.GetString() : null;

                var user = new UserRegisteredEvent(id!, username!, email);

                var message = JsonSerializer.SerializeToUtf8Bytes(user);

                await _publisher.PublishAsync(
                    exchange: "registration-exchange",
                    routingKey: "user.registered",
                    body: message
                );
            };



            using (var listenCmd = new NpgsqlCommand("LISTEN user_registered;", conn))
            {
                await listenCmd.ExecuteNonQueryAsync(stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await conn.WaitAsync(stoppingToken);
            }
        }
    }

}
