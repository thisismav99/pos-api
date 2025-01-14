using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PosAPI.BLL.ServiceInterfaces.Cards;
using PosAPI.BLL.ServiceInterfaces.JwtToken;
using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.BLL.ServiceInterfaces.Transactions;
using PosAPI.BLL.Services.Cards;
using PosAPI.BLL.Services.JwtToken;
using PosAPI.BLL.Services.Products;
using PosAPI.BLL.Services.Transactions;
using PosAPI.DAL;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/pos-api-logs-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Assign the Serilog logger to the logging system
builder.Logging.ClearProviders();  // Clear default logging providers
builder.Logging.AddSerilog();  // Add Serilog as the logging provider
#endregion

#region Database Context
builder.Services.AddDbContext<PosDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PosDB"))
           .UseLazyLoadingProxies()
);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDB"))
           .UseLazyLoadingProxies()
);
#endregion

#region Services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { 
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PosAPI", Version = "v1" }); 
    
    // Configure Swagger to use the JWT bearer token
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme { 
        Name = "Authorization", 
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, 
        Scheme = "Bearer", BearerFormat = "JWT", 
        In = Microsoft.OpenApi.Models.ParameterLocation.Header, 
        Description = "Enter 'Bearer' followed by a space and the JWT token." 
    }); 
    
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement { 
        { 
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme 
            { 
                Reference = new Microsoft.OpenApi.Models.OpenApiReference 
                { 
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" 
                } 
            }, 
            
            new string[] {} 
        } 
    }); 
});

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => { 
    options.TokenValidationParameters = new TokenValidationParameters 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        ValidIssuer = builder.Configuration["Jwt:Issuer"], 
        ValidAudience = builder.Configuration["Jwt:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)) 
    }; 
  }); 

builder.Services.AddAuthorization();
#endregion

#region Repoistories
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
#endregion

#region Unit of Works
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
#endregion

#region Custom Services
builder.Services.AddScoped(typeof(ICardService<>), typeof(CardService<>));
builder.Services.AddScoped(typeof(IProductService<>), typeof(ProductService<>));
builder.Services.AddScoped(typeof(IProductTransactionService<>), typeof(ProductTransactionService<>));
builder.Services.AddScoped(typeof(ITransactionService<>), typeof(TransactionService<>));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
