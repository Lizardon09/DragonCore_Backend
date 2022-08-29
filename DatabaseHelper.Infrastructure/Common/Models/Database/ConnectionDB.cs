using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Interfaces;
using DatabaseHelper.Domain.Common.Models;
using DatabaseHelper.Domain.Postgres.Models;
using DatabaseHelper.Domain.SQL.Models;
using DatabaseHelper.Infrastructure.Common.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Infrastructure.Common.Models.Database
{
    public class ConnectionDB : IConnectionDB
    {
        private static List<IDatabaseAdapter> _databaseAdapters = new List<IDatabaseAdapter>
            {
                {new PostgresDBAdapter()},
                {new SQLServerAdapter()}
            };
        private static Dictionary<string, ConnectionDetail> _connections = new Dictionary<string, ConnectionDetail>();

        public IDbConnection DbConnection(string name)
        {
            if (!_connections.TryGetValue(name, out ConnectionDetail? connection))
            {
                throw new Exception("Can not create db connection.");//sql exception
            }

            var adapter = GetDBAdapter(connection.ConnectionType);

            if (adapter is null)
            {
                throw new Exception("Can not create db connection.");//sql exception
            }

            return adapter.DbConnection(connection.ConnectionString);
        }

        private static IDatabaseAdapter? GetDBAdapter(DatabaseAdapter databaseAdapter)
        {
            return _databaseAdapters.Where(x => x.DatabaseAdapter == databaseAdapter)?.FirstOrDefault();
        }

        public static void RegisterConnections(List<ConnectionDetail> connections)
        {

            connections.ForEach(conn => _connections.Add(conn.Name, conn));
        }
    }
}
