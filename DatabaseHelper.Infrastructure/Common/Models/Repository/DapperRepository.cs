using Dapper.Contrib.Extensions;
using DatabaseHelper.Domain.Common.Enums;
using DatabaseHelper.Domain.Common.Models;
using DatabaseHelper.Infrastructure.Common.Helpers;
using DatabaseHelper.Infrastructure.Common.Interfaces.Database;
using DatabaseHelper.Infrastructure.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper.Infrastructure.Common.Models.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConnectionDB _connectionDB;
        private string _baseConnectionName = string.Empty;

        public DapperRepository(IConnectionDB connectionDB)
        {
            _connectionDB = connectionDB;
        }

        public void SetBaseConnection(string name)
        {
            _baseConnectionName = name;
        }

        public async Task<DapperHelperResponse<TEntity>> FindAsync<TDataType, TEntity>(TDataType id) where TDataType : struct
                                                                                                     where TEntity : class
        {
            try
            {
                using IDbConnection _conn = _connectionDB.DbConnection(_baseConnectionName);

                var result = await _conn.GetAsync<TEntity>(id);

                var isSuccess = result is not null;

                return isSuccess ? ResponseHelper.GetDapperHelperResponse(StatusResponse.SUCCESS, idResponse: id, entity: result!) : ResponseHelper.GetDapperHelperResponse<TEntity>(StatusResponse.UNSUCCESSFUL, isSuccessful: false, idResponse: id);
            }
            catch (Exception ex)
            {
                var errors = ResponseHelper.GetErrorResponse(ex);

                return ResponseHelper.GetDapperHelperResponse<TEntity>(StatusResponse.INTERNAL_ERROR, isSuccessful: false, errors: errors, idResponse: id);
            }
        }

        public async Task<DapperHelperResponse<IEnumerable<TEntity>>> GetAllAsync<TEntity>() where TEntity : class
        {
            try
            {
                using IDbConnection _conn = _connectionDB.DbConnection(_baseConnectionName);

                var result = await _conn.GetAllAsync<TEntity>();

                return ResponseHelper.GetDapperHelperResponse(StatusResponse.SUCCESS, entity: result);
            }
            catch (Exception ex)
            {
                var errors = ResponseHelper.GetErrorResponse(ex);

                return ResponseHelper.GetDapperHelperResponse<IEnumerable<TEntity>>(StatusResponse.INTERNAL_ERROR, isSuccessful: false, errors: errors);
            }
        }

        public async Task<DapperHelperResponse<TEntity>> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                using IDbConnection _conn = _connectionDB.DbConnection(_baseConnectionName);

                var result = await _conn.InsertAsync(entity);

                var isSuccessResult = result > 0;

                return isSuccessResult ? ResponseHelper.GetDapperHelperResponse<TEntity>(StatusResponse.SUCCESS, idResponse: result) : ResponseHelper.GetDapperHelperResponse(StatusResponse.UNSUCCESSFUL, isSuccessful: false, entity: entity);

            }
            catch (Exception ex)
            {
                var errors = ResponseHelper.GetErrorResponse(ex);

                return ResponseHelper.GetDapperHelperResponse(StatusResponse.INTERNAL_ERROR, isSuccessful: false, entity: entity, errors: errors);
            }
        }

        public async Task<DapperHelperResponse<TEntity>> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                using IDbConnection _conn = _connectionDB.DbConnection(_baseConnectionName);

                var isUpdateSuccessful = await _conn.UpdateAsync(entity);

                return isUpdateSuccessful ? ResponseHelper.GetDapperHelperResponse(StatusResponse.SUCCESS, entity: entity) : ResponseHelper.GetDapperHelperResponse(StatusResponse.UNSUCCESSFUL, isSuccessful: false, entity: entity);
            }
            catch (Exception ex)
            {
                var errors = ResponseHelper.GetErrorResponse(ex);

                return ResponseHelper.GetDapperHelperResponse(StatusResponse.INTERNAL_ERROR, isSuccessful: false, entity: entity, errors: errors);
            }
        }
    }
}
