using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.Common.Models
{
    public class Errors
    {
        public string Message { get; init; }
        public Exception InnerMessage { get; init; }
    }
}
