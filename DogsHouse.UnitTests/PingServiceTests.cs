using DogsHouse.API.Services;

namespace DogsHouse.UnitTests;

public class PingServiceTests
{
    private PingService _service;

    [SetUp]
    public void Setup()
    {
        _service = new PingService();
    }

    #region PingTests

    [Test]
    public void Ping_ReturnsMessage()
    {
        // arrange
        string exceptedMessage = "Dogshouseservice.Version1.0.1";
        string actualMessage = "";
        // act
        actualMessage = _service.Ping();
        // assert
        Assert.That(actualMessage, Is.EqualTo(exceptedMessage));
    }

    #endregion
}