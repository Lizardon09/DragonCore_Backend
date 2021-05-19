using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Infrastructure.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void ConfigureElasticSearch(this IServiceCollection services, string elasticUrl, string elasticDefaultIndex)
        {
            var settings = new ConnectionSettings(new Uri(
                elasticUrl
                ))
                .DefaultIndex(elasticDefaultIndex);

            settings.ThrowExceptions(alwaysThrow: true);

            var client = new ElasticClient(settings);

            services.AddSingleton(client);

        }

    }
}
