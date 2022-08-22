using DragonCore.API.Extensions;
using Elasticsearch.Net;
using ElasticSearch.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Cryptography.X509Certificates;
using Nest;
using BasicHelpers.Infrastructure.Extensions;
using BasicHelpers.Infrastructure.Settings;
using System.IO;

namespace DragonCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DragonCore API", Version = "v1" });
            });
            services.ConfigureCors(Configuration);
            services.ConfigureControllers();

            var elasticConnectionSettings = new ConnectionSettings(new Uri(
                Configuration.GetSection("Elastic").GetSection("Elastic_URL").Get<string>()
                ))
                .DefaultIndex(Configuration.GetSection("Elastic").GetSection("Elastic_Default_Index").Get<string>())
                .ServerCertificateValidationCallback(CertificateValidations.AuthorityIsRoot(new X509Certificate(Configuration.GetSection("Elastic").GetSection("Elastic_CA_Path").Get<string>())))
                .BasicAuthentication(Configuration.GetSection("Elastic").GetSection("Elastic_User").Get<string>(), Configuration.GetSection("Elastic").GetSection("Elastic_Password").Get<string>());

            services.ConfigureElasticSearch(elasticConnectionSettings.DisableDirectStreaming());

            services.ConfigureBasicHelpers(
                new BasicHelperSettings()
                    .IOHelper()
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DragonCore");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(Configuration.GetSection("CorsPolicy_Default").Get<string>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
