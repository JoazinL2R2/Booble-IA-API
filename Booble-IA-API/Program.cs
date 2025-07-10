using Booble_IA_API._2___Services;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._2___Services.Models;
using Booble_IA_API._3___Repository;
using Booble_IA_API._3___Repository.CrossCutting;
using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<BoobleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Configuração do JWT, se necessário
// builder.Services.AddAuthentication(...);

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IHabitoService, HabitoService>();
builder.Services.AddScoped<IHabitoRepository, HabitoRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAmizadeService, AmizadeService>();
builder.Services.AddScoped<IAmizadeRepository, AmizadeRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// app.UseAuthentication(); // Descomente se usar autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();