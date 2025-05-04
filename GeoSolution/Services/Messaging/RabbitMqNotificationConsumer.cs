using GeoSolution.Models.MQ;
using GeoSolution.Options;
using GeoSolution.Services.Notification;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace GeoSolution.Services.Messaging
{
    public class RabbitMqNotificationConsumer : BackgroundService
    {
        private readonly IConnectionFactory _factory;
        private readonly UserRegistrationQueueOptions _queueOptions;
        private readonly RabbitMqOptions _options;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMqNotificationConsumer> _logger;

        public RabbitMqNotificationConsumer(
            IConnectionFactory factory,
            IOptions<UserRegistrationQueueOptions> queueOptions,
            IOptions<RabbitMqOptions> options,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMqNotificationConsumer> logger)
        {
            _factory = factory;
            _options = options.Value;
            _queueOptions = queueOptions.Value;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var queueName = _queueOptions.QueueName;
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                _logger.LogInformation("RabbitMqNotificationConsumer: Received message deliveryTag={Tag}", ea.DeliveryTag);

                var json = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation("RabbitMqNotificationConsumer: Payload: {Json}", json);

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var notificationManager = scope.ServiceProvider.GetRequiredService<NotificationManager>();

                    var evt = JsonSerializer.Deserialize<UserRegisteredEvent>(json);
                    if (evt != null)
                    {
                        await notificationManager.HandleAsync(evt, stoppingToken);
                        _logger.LogInformation("NotificationManager successfully processed event for {Username}", evt.Username);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to deserialize UserRegisteredEvent from JSON");
                    }

                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process notification event: {Json}", json);
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer
            );

            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (TaskCanceledException)
            {
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
