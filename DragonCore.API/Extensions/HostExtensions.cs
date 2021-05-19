using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DragonCore.API.Extensions
{
    public static class HostExtensions
    {
		public static void CreateHost(string[] args)
		{
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.ConfigureAppConfiguration(configuration =>
				{
					configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
					configuration.AddJsonFile(
						$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
						optional: true);
				});
	}
}
