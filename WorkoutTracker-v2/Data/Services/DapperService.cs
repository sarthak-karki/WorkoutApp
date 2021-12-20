using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace WorkoutTracker_v2.Services
{
    public interface IDapperService
    {
        public IEnumerable<T> ExecuteQuery<T>(string sql);
        public IEnumerable<T> ExecuteQuery<T>(string sql, object param);
        public int Insert(string sql);
        public int Insert(string sql, object param);
    }

    public class DapperService : IDapperService
    {
        private readonly IConfiguration _config;
        private string ConnectionString = "DefaultConnection";

        public DapperService(IConfiguration config)
        {
            _config = config;
         }

        public IEnumerable<T> ExecuteQuery<T>(string sql)
        {
            return ExecuteQuery<T>(sql, null);
        }

        public IEnumerable<T> ExecuteQuery<T>(string sql, object param)
        {
            try
            {
                using (var connection = new SqlConnection(_config.GetConnectionString(ConnectionString)))
                {
                    return connection.Query<T>(sql, param);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public int Insert(string sql)
        {
            try
            {
                using (var connection = new SqlConnection(_config.GetConnectionString(ConnectionString)))
                {
                    return connection.ExecuteScalar<int>(sql);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public int Insert(string sql, object param)
        {
            try
            {
                using (var connection = new SqlConnection(_config.GetConnectionString(ConnectionString)))
                {
                    return connection.ExecuteScalar<int>(sql, param);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}
