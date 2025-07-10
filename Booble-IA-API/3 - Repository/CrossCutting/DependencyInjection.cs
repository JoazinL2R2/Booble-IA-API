using Booble_IA_API._2___Services;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._2___Services.Models;
using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booble_IA_API._3___Repository.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            return services;
        }
    }
}
