using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoSolution.Shared.Options;
using System.Text.Json;
using GeoSolution.Shared.Services.Messaging.Abstractions;
using GeoSolution.Shared.Models.MQ;

namespace GeoSolution.Consumer.Services.Messaging
{
    public class RabbitMqNotificationConsumer : BackgroundService
    {
        private readonly IConnectionFactory _factory;
        private readonly DeffaultQueueOptions _queueOptions;
        private readonly RabbitMqOptions _options;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IServiceProvider _sp;
        private readonly ILogger<RabbitMqNotificationConsumer> _logger;

        public RabbitMqNotificationConsumer(
            IConnectionFactory factory,
            IOptions<DeffaultQueueOptions> queueOptions,
            IOptions<RabbitMqOptions> options,
            IServiceScopeFactory scopeFactory,
            IServiceProvider serviceProvider,
            ILogger<RabbitMqNotificationConsumer> logger)
        {
            _factory = factory;
            _options = options.Value;
            _sp = serviceProvider;
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

            var handlerRegistry = _sp.GetRequiredService<Dictionary<string, IEventHandler>>();
            var consumer = new AsyncEventingBasicConsumer(channel);
            _logger.LogInformation("Consumer: Exchange  объявлен."+ queueName);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                _logger.LogInformation("RabbitMqNotificationConsumer: Received message deliveryTag={Tag}", ea.DeliveryTag);

                var json = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation("RabbitMqNotificationConsumer: Payload: {Json}", json);

                try
                {
                    var envelope = JsonSerializer.Deserialize<Envelope<JsonElement>>(json);
                    if (envelope == null || string.IsNullOrWhiteSpace(envelope.Type))
                    {
                        _logger.LogWarning("Cannot parse Envelope or Type is missing");
                        await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
                        return;
                    }
                    if (handlerRegistry.TryGetValue(envelope.Type, out var handler))
                    {
                        await handler.HandleAsync(envelope.Payload, stoppingToken);
                        _logger.LogInformation("Event '{Type}' succeed", envelope.Type);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    else
                    {
                        _logger.LogWarning("No handler for '{Type}'", envelope.Type);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
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
