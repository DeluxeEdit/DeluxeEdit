using Extensions.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Extensions
{

    /// <summary>
    /// This our addition to Dapper,  where we add support for batched DML queries
    /// </summary>
    public class DapperBatchHelper
    {
        /// <summary>
        ///  This our addition to Dapper,  where we add support for batched DML queries
        /// </summary>
        /// <param name="connection"></param>
        public DapperBatchHelper(IDbConnection connection)
        {
            this.Connection = connection;
            batched = new List<SqlItem>();
        }
        /// <summary>
        /// This our addition to Dapper,  where we add support for batched DML queries 
        /// </summary>
        public IDbConnection  Connection { get; set; }

        private List<SqlItem> batched;
        /// <summary>
        ///Clear the list of batched queries
        /// </summary>
        public void ClearBatched()
        {
            batched.Clear();
        }
        public void AddBatched(string sql, object param)
        {
            batched.Add(new SqlItem { Sql = sql, Params = param } );
        }
        public int ExecuteBatchedDml()
        {
            var sql = String.Join(";\r\n", batched.Select(p => p.Sql));;
            var param = batched.Select(p => p.Params).ToArray();

            var result=Connection.Execute(sql, param);
            ClearBatched();
            return result;
        }
    }
}
