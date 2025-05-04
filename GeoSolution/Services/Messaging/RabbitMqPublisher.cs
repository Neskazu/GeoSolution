using GeoSolution.Services.Messaging;
using RabbitMQ.Client;

public class RabbitMqPublisher : IMessagePublisher, IAsyncDisposable
{
    private readonly IConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqPublisher(IConnectionFactory factory)
    {
        _factory = factory;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_channel is not null) return;

        _connection = await _factory.CreateConnectionAsync();        
        _channel = await _connection.CreateChannelAsync();        
    }

    public async Task PublishAsync(string exchange, string routingKey, byte[] body)
    {
        await EnsureInitializedAsync();
        await _channel!
            .BasicPublishAsync(exchange: exchange,
                               routingKey: routingKey,
                               body: body);                   
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null) { await _channel.CloseAsync(); await _channel.DisposeAsync(); }
        if (_connection != null) { await _connection.CloseAsync(); await _connection.DisposeAsync(); }
    }
}