using System.Threading.RateLimiting;
using DogsHouse.API.Infrastructure;
using DogsHouse.API.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PetsDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Pets")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddRateLimiter(opt => opt
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 10; // ten requests per second
        options.Window = TimeSpan.FromSeconds(10); // waiting ten seconds on response form server
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

#region Services
// added service via Scoped life cycle, so we will create new object of the service for every http method call
// also it be isolation
builder.Services.AddScoped<IDogService, DogService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();