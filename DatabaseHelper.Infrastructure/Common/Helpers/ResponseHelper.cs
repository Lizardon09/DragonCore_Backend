using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Infrastructure.Common.Helpers
{
    public class ResponseHelper
    {
        public static DapperHelperResponse<TEntity> GetDapperHelperResponse<TEntity>(StatusResponse statusResponse, bool isSuccessful = true, dynamic idResponse = null, TEntity entity = default, Errors errors = null)
            where TEntity : class
        {
            return new DapperHelperResponse<TEntity>()
            {
                IDResponse = idResponse,
                StatusResponse = statusResponse,
                IsSuccess = isSuccessful,
                ReturnObject = entity,
                Errors = errors
            };
        }

        public static Errors GetErrorResponse(Exception exception)
        {
            return new Errors
            {
                InnerMessage = exception.InnerException,
                Message = exception.Message
            };
        }
    }
}
