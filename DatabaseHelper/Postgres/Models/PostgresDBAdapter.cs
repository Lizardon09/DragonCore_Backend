using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.Postgres.Models
{
    public class PostgresDBAdapter : IDatabaseAdapter
    {
        public IDbConnection DbConnection(string connectionString) => new NpgsqlConnection(connectionString);

        public DatabaseAdapter DatabaseAdapter => DatabaseAdapter.POSTGRES;
    }
}
