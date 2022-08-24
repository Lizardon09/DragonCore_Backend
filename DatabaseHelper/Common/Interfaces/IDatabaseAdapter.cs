using DatabaseHelper.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.Common.Interfaces
{
    public interface IDatabaseAdapter
    {
        IDbConnection DbConnection(string connectionString);
        DatabaseAdapter DatabaseAdapter { get; }
    }
}
