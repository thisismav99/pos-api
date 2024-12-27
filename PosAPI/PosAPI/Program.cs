using Microsoft.EntityFrameworkCore;
using PosAPI.DAL;
using PosAPI.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Database Contexts
builder.Services.AddDbContext<PosDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PosDB"))
           .UseLazyLoadingProxies()
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Repositories
builder.Services.AddScoped(typeof(IPosRepository<>), typeof(PosRepository<,>));

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
