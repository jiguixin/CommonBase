/*
 *名称：Repository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 14:05:09
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Data.Ado.Dapper;

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    { 

        #region Abstract Property

        public abstract string ExistsProc { get; }
         
        public abstract string AddProc { get;}

        public abstract string DeleteProc { get; }

        public abstract string GetListProc { get; }

        public abstract string GetModelProc { get; }
         
        public abstract string UpdateProc { get; }
         
        #endregion

        #region Public Method

        public virtual int Add(TEntity item)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                return connection.Execute(AddProc, item,
                    commandType: CommandType.StoredProcedure); 
            }
        }

        public virtual int Delete(string sysId)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            { 
                var p = CreateSysIdDynamicParameters(sysId);

                return
                    connection.Execute(DeleteProc, p,
                        commandType: CommandType.StoredProcedure);
            }
        }

        public virtual int Update(TEntity item)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                return connection.Execute(UpdateProc, item,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public virtual bool Exists(string sysId)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = CreateSysIdDynamicParameters(sysId);
                return
                    connection.Query<TEntity>(ExistsProc, p, commandType: CommandType.StoredProcedure).FirstOrDefault()
                    != null;
            }
        }

        public virtual TEntity GetModel(string sysId)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = CreateSysIdDynamicParameters(sysId);

                return
                    connection.Query<TEntity>(GetModelProc, p, commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        } 

        public virtual IEnumerable<TEntity> GetPaged(
            string table,
            string fields,
            string where,
            string orderBy,
            int currentPage,
            int pageSize,
            int getCount,
            out int total)
        {
            total = 0;
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@Table", table, DbType.String, ParameterDirection.Input, 1000);
                p.Add("@Fields", fields, DbType.String, ParameterDirection.Input, 2000);
                p.Add("@Where", where, DbType.String, ParameterDirection.Input, 1000);
                p.Add("@OrderBy", orderBy, DbType.String, ParameterDirection.Input, 50);
                p.Add("@CurrentPage", currentPage, DbType.Int32, ParameterDirection.Input);
                p.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
                p.Add("@GetCount", getCount, DbType.Int32, ParameterDirection.Input);
                p.Add("@Count", total, DbType.String, ParameterDirection.Output);

                return connection.Query<TEntity>(Constant.ProcGetPaged, p, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TEntity> GetList()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                return connection.Query<TEntity>(GetListProc, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TEntity> GetList(string table, string fields = "", string where = "")
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@Table", table, DbType.String, ParameterDirection.Input, 1000);
                p.Add("@Fields", fields, DbType.String, ParameterDirection.Input, 2000);
                p.Add("@Where", where, DbType.String, ParameterDirection.Input, 1000);

                return connection.Query<TEntity>(Constant.ProcGetList, p, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Helper
        
        private static DynamicParameters CreateSysIdDynamicParameters(string sysId)
        {
            var p = new DynamicParameters();
            p.Add("@SysId", sysId, DbType.String, ParameterDirection.Input, 50);
            return p;
        }

        #endregion
    }
}