namespace DogsHouse.API.Services;

public class PingService : IPingService
{
    public string Ping()
    {
        var message = "Dogshouseservice.Version1.0.1";

        return message;
    }
}