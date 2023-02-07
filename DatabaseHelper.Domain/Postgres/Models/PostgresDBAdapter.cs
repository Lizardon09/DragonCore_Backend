using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Interfaces;
using Npgsql;
using System.Data;

namespace DatabaseHelper.Domain.Postgres.Models
{
    public class PostgresDBAdapter : IDatabaseAdapter
    {
        public IDbConnection DbConnection(string connectionString) => new NpgsqlConnection(connectionString);

        public DatabaseAdapter DatabaseAdapter => DatabaseAdapter.POSTGRES;
    }
}
