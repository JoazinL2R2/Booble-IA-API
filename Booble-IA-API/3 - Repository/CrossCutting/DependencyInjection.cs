using Booble_IA_API._2___Services;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._2___Services.Models;
using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booble_IA_API._3___Repository.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<BoobleContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLiteConnection")));


            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IAmizadeRepository, AmizadeRepository>();

            return services;
        }
    }
}
