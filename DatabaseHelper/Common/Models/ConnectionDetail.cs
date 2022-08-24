using DatabaseHelper.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.Common.Models
{
    public class ConnectionDetail
    {
        public string Name { get; init; }
        public DatabaseAdapter ConnectionType { get; init; }
        public string ConnectionString { get; init; }
        public bool DefaultConnection { get; init; } = false;
    }
}
