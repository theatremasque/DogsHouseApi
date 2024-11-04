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

    #region AddAsyncTests

    [Test]
    public void AddAsync_CheckConstraintTailLength_ThrowsDbUpdateException()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        DogAddDto dto = new DogAddDto()
        {
            Name = "lesha",
            Color = "white",
            TailLength = 0,
            Weight = 15
        };
        // assert
        Assert.ThrowsAsync<DbUpdateException>( async () =>
        {
            // act
            await _service.AddAsync(dto, cancellationToken);
        });
    }
    
    [Test]
    public void AddAsync_CheckConstraintWeight_ThrowsDbUpdateException()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        DogAddDto dto = new DogAddDto()
        {
            Name = "lesha",
            Color = "white",
            TailLength = 15,
            Weight = 0
        };
        // assert
        Assert.ThrowsAsync<DbUpdateException>( async () =>
        {
            // act
            await _service.AddAsync(dto, cancellationToken);
        });
    }
    
    [Test]
    public async Task AddAsync_PassCheckConstraints_ReturnsDog()
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

    #region ListAsyncTests

    [Test]
    public async Task ListAsync_HaveNotDogsInDb_ReturnsEmptyArray()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var config = new MapperConfiguration(cfg => { cfg.CreateProjection<Dog, DogDto>(); });
        var mapper = config.CreateMapper();
        var dogService = new DogService(_testCtx, mapper);
        // act
        var actualDogs = await dogService.ListAsync(null, cancellationToken);
        // assert
        Assert.That(actualDogs, Is.Empty);
    }
    
    [Test]
    public async Task ListAsync_WithoutQueryParameters_ReturnsDogs()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var config = new MapperConfiguration(cfg => { cfg.CreateProjection<Dog, DogDto>(); });
        var mapper = config.CreateMapper();
        var dogsToDb = new List<Dog>()
        {
            new()
            {
                Id = 1, Name = "Jonny", Color = "red", TailLength = 11, Weight = 11,
            },
            new()
            {
                Id = 2, Name = "Wanny", Color = "blue", TailLength = 22, Weight = 22,
            },
            new()
            {
                Id = 3, Name = "Manny", Color = "black", TailLength = 33, Weight = 33,
            }
        };
        await _testCtx.Dogs.AddRangeAsync(dogsToDb, cancellationToken);
        await _testCtx.SaveChangesAsync(cancellationToken);
        IEnumerable<DogDto> exceptedDogs = new List<DogDto>()
        {
            new()
            {
                Id = 1, Name = "Jonny", Color = "red", TailLength = 11, Weight = 11,
            },
            new()
            {
                Id = 2, Name = "Wanny", Color = "blue", TailLength = 22, Weight = 22,
            },
            new()
            {
                Id = 3, Name = "Manny", Color = "black", TailLength = 33, Weight = 33,
            }
        };
        var dogService = new DogService(_testCtx, mapper);
        // act
        var actualDogs = await dogService.ListAsync(null, cancellationToken);
        // assert
        Assert.That(actualDogs, Is.EqualTo(exceptedDogs));
    }

    #endregion
}