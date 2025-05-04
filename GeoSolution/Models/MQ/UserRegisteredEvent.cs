namespace GeoSolution.Models.MQ
{
    //In theory more secure cuz it immutable
    public record UserRegisteredEvent(string Id, string Username, string Email);
}
