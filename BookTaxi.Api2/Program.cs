using BookTaxi.Common2.Models.Jwt;
using BookTaxi.Services.Api;
using BookTaxi.Services.JwtService;
using BookTaxi.Services.SMS;
using BookTaxiEntyties.Context;
using BookTaxiEntyties.Contracts;
using BookTaxiEntyties.Repositiries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Taxi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connenctionString = builder.Configuration.GetConnectionString("Connection");
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

// Add services to the container.
builder.Services.AddDbContext<BookTaxiDbContext>(options =>
{
    options.UseNpgsql(connenctionString);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer {token} \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = securityKey,
        ClockSkew = TimeSpan.FromDays(1),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,
    };
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

// Scoped services
builder.Services.AddScoped<BookTaxiDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISeatInfoRepository, SeatInfoRepository>();
builder.Services.AddScoped<IUserCarOrderRepository, UserCarOrderRepository>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();
builder.Services.AddScoped<IUserCarRepository, UserCarRepository>();
builder.Services.AddScoped<ICarInfoRepository, CarInfoRepository>();
builder.Services.AddScoped<UserCarOrderService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<CarInfoService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<DriverService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<DestinationService>();
builder.Services.AddScoped<PaymentService>();

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
