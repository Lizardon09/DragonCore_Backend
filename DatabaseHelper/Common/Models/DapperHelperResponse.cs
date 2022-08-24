using DatabaseHelper.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Domain.Common.Models
{
    public class DapperHelperResponse<TEntity>
        where TEntity : class
    {
        public dynamic IDResponse { get; init; }
        public bool IsSuccess { get; init; }
        public StatusResponse StatusResponse { get; init; }
        public TEntity ReturnObject { get; init; }
        public Errors Errors { get; init; }
    }
}
