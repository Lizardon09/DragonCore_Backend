using ElasticSearch.Infrastructure.Services.Interfaces;
using ElasticSearch.Infrastructure.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Infrastructure.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void ConfigureElasticSearch2(this IServiceCollection services, ConnectionSettings connectionSettings)
        {
            var settings = connectionSettings;

            settings.ThrowExceptions(alwaysThrow: true);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
        }
    }
}
