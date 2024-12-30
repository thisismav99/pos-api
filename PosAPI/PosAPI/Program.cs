using Microsoft.EntityFrameworkCore;
using PosAPI.DAL;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/pos-api-logs-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Assign the Serilog logger to the logging system
builder.Logging.ClearProviders();  // Clear default logging providers
builder.Logging.AddSerilog();  // Add Serilog as the logging provider

// Add Database Contexts
builder.Services.AddDbContext<PosDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PosDB"))
           .UseLazyLoadingProxies()
);

// Add Services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Repositories
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

// Add Unit of Works
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
