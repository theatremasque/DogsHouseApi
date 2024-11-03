using AutoMapper;
using DogsHouse.API.Infrastructure;
using DogsHouse.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DogsHouse.UnitTests;

public class DogServiceTests
{
    private PetsDbContext _testCtx;
    private Mock<IMapper> _mockMapper;
    private DogService _service;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PetsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestPetsDb")
            .Options;

        _testCtx = new PetsDbContext(options);

        _mockMapper = new Mock<IMapper>();

        _service = new DogService(_testCtx, _mockMapper.Object);
    }
    
    [TearDown]
    public void TearDown()
    {
        _testCtx.Database.EnsureDeleted();
        _testCtx.Dispose();
    }
    
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
}