using AutoMapper;
using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
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

    #region AddAsyncTests

    [Test]
    public void AddAsync_IsNull_ThrowsNullReferenceException()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        DogAddDto dto = null!;
        // assert
        Assert.ThrowsAsync<NullReferenceException>( async () =>
        {
            // act
            await _service.AddAsync(dto, cancellationToken);
        });
    }
    
    [Test]
    public async Task AddAsync_IsNotNull_ReturnsDog()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var dto = new DogAddDto {Color = "red", Name = "Test", TailLength = 1, Weight = 15 };
        var entity = new Dog {Id = 1, Color = "red", Name = "Test", TailLength = 1, Weight = 15 };
        _mockMapper.Setup(m => m.Map<Dog>(dto)).Returns(entity);
        // act
        var dog = await _service.AddAsync(dto, cancellationToken);
        // assert
        Assert.IsNotNull(dog);
    }
    
    [Test]
    public async Task AddAsync_SuccessfullyAddedToDb_ReturnsDog()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var dto = new DogAddDto {Color = "red", Name = "Test", TailLength = 1, Weight = 15 };
        var entity = new Dog {Id = 1, Color = "red", Name = "Test", TailLength = 1, Weight = 15 };
        _mockMapper.Setup(m => m.Map<Dog>(dto)).Returns(entity);
        // act
        var dog = await _service.AddAsync(dto, cancellationToken);
        // assert
        var dogInDb = await _testCtx.Dogs.FindAsync(entity.Id);
        Assert.That(dogInDb, Is.EqualTo(dog));
    }

    #endregion
}