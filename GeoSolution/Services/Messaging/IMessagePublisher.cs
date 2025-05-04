namespace GeoSolution.Services.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync(string exchange, string routingKey, byte[] body);
    }
}
