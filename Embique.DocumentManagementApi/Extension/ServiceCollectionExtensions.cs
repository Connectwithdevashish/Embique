using System;
using Embique.DocumentManagementApi.Data;
using Embique.DocumentManagementApi.Services;
using Microsoft.EntityFrameworkCore;

namespace Embique.DocumentManagementApi.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger)
    {
        logger.LogInformation($"[{typeof(ServiceCollectionExtensions)}] : Registration of AppDbContext in extension method");
        services.AddDbContext<DocumentAppDbContext>(e => 
            e.UseNpgsql(configuration.GetConnectionString("DocumentDefaultConnection")));

        logger.LogInformation($"[{typeof(ServiceCollectionExtensions)}] : Registration of IDocumentService as Scoped service in extension method");
        services.AddScoped<IDocumentService, DocumentService>();
        return services;
    } 
}
