using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.SQL.Models
{
    public class SQLServerAdapter : IDatabaseAdapter
    {
        public IDbConnection DbConnection(string connectionString) => new SqlConnection(connectionString);

        public DatabaseAdapter DatabaseAdapter => DatabaseAdapter.SQL_SERVER;
    }
}
