using Booble_IA_API._2___Services;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._2___Services.Models;
using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;

namespace Booble_IA_API._3___Repository.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<BoobleContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgress")));


            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
            .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddProfileService<CustomProfileService>()
            .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
            .AddDeveloperSigningCredential(); // In production, use AddSigningCredential with a proper certificate


            // Add JWT Bearer Authentication for API endpoints
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["IdentityServer:Authority"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
                options.Audience = configuration["IdentityServer:ApiName"];
                
                // Also support legacy JWT tokens
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? "")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Application Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IHabitoService, HabitoService>();
            services.AddScoped<IHabitoRepository, HabitoRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAmizadeService, AmizadeService>();
            services.AddScoped<IAmizadeRepository, AmizadeRepository>();
            services.AddScoped<IRankingRepository, RankingRepository>();
            services.AddScoped<IRankingService, RankingService>();

            // Identity Server Services
            services.AddScoped<IProfileService, CustomProfileService>();
            services.AddScoped<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();

            return services;
        }
    }
}
