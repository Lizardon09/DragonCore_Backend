using DatabaseHelper.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Infrastructure.Common.Interfaces.Repository
{
    public interface IDapperRepository
    {
        Task<DapperHelperResponse<TEntity>> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<DapperHelperResponse<IEnumerable<TEntity>>> GetAllAsync<TEntity>() where TEntity : class;
        Task<DapperHelperResponse<TEntity>> FindAsync<TDataType, TEntity>(TDataType id) where TDataType : struct
                                                                                        where TEntity : class;
        Task<DapperHelperResponse<TEntity>> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        void SetBaseConnection(string name);
    }
}
