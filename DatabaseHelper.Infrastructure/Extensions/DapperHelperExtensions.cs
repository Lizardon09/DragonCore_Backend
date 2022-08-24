using DatabaseHelper.Domain.Common.Models;
using DatabaseHelper.Infrastructure.Common.Interfaces.Database;
using DatabaseHelper.Infrastructure.Common.Interfaces.Repository;
using DatabaseHelper.Infrastructure.Common.Models.Database;
using DatabaseHelper.Infrastructure.Common.Models.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Infrastructure.Extensions
{
    public static class DapperHelperExtensions
    {
        public static void ConfigureDatabaseHelper(this IServiceCollection services, List<ConnectionDetail> connections)
        {
            ConnectionDB.RegisterConnections(connections);
            services.AddSingleton<IConnectionDB, ConnectionDB>();
            services.AddScoped<IDapperRepository, DapperRepository>();
        }
    }
}
