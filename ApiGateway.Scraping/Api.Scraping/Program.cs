using AccesoDatos.Scraping.Contexto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositorios.Scraping.Implementaciones;
using Repositorios.Scraping.Interfaces;
using Repositorios.Scraping.Mappers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


const string corsConfiguration = "scraping";
var jwtSetting = builder.Configuration.GetSection("JwtSettings");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ScraperdbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("bdscraping"));
});

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IArchivoRepositorio, ArchivoRepositorio>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(typeof(UsuarioProfile).Assembly);
});

builder.Services.AddCors(policy =>
{
    policy.AddPolicy(corsConfiguration, config =>
    {
        config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSetting["Issuer"],
            ValidAudience = jwtSetting["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["SecretKey"]!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(corsConfiguration);

app.Run();
