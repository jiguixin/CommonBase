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

        internal virtual dynamic Mapping(TEntity item)
        {
            return item;
        }

        public virtual IDbConnection Connection
        {
            get { return ConnectionFactory.CreateMsSqlConnection(); }
        }

        public virtual int Add(TEntity item)
        {
            using (var connection = Connection)
            {
                var parameters = (object)Mapping(item);

                return connection.Execute(AddProc, parameters,
                    commandType: CommandType.StoredProcedure); 
            }
        }

        public virtual int Add(TEntity item, IDbTransaction trans)
        {
            var parameters = (object)Mapping(item);
            return trans.Connection.Execute(AddProc, parameters,trans,
                    commandType: CommandType.StoredProcedure); 
        }

        public virtual int Delete(string sysId)
        {
            using (var connection = Connection)
            { 
                var p = CreateSysIdDynamicParameters(sysId);

                return
                    connection.Execute(DeleteProc, p,
                        commandType: CommandType.StoredProcedure);
            }
        }

        public virtual int Delete(string sysId, IDbTransaction trans)
        {
            var p = CreateSysIdDynamicParameters(sysId);

            return trans.Connection.Execute(DeleteProc, p, trans,
                   commandType: CommandType.StoredProcedure); 
        }

        public virtual int Update(TEntity item)
        {
            using (var connection = Connection)
            {
                var parameters = (object)Mapping(item);

                return connection.Execute(UpdateProc, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public virtual int Update(TEntity item, IDbTransaction trans)
        {
            var parameters = (object) Mapping(item);

            return trans.Connection.Execute(UpdateProc, parameters, trans,
                                            commandType: CommandType.StoredProcedure);
        }

        public virtual int AddOrModifyTrans<TP, TC>(TP item, TC childValue, Func<TP, IDbTransaction, int> parent, Func<TC, IDbTransaction, int> child)
        {
            using (var connection = Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result;
                    if ((result = parent(item, tran)) > 0)
                    {
                        if ((result = child(childValue, tran)) > 0)
                        {
                            tran.Commit();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result;
                }
            }
        }

        public virtual int DeleteTrans(string sysId, Func<string, IDbTransaction, int> parent, Func<string, IDbTransaction, int> child)
        {
            using (var connection = Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。
                    int result;
                    if ((result = child(sysId, tran)) >= 0)
                    {
                        if ((result = parent(sysId, tran)) >= 0)
                        {
                            tran.Commit();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result;
                }
            }
        }

        /// <summary>
        /// 先删除孙子级，然后再删除儿子级，最后才删除父组数据
        /// </summary>
        /// <param name="sysId"></param>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="grandChild"></param>
        /// <returns></returns>
        public virtual int DeleteTrans(string sysId, Func<string, IDbTransaction, int> parent, Func<string, IDbTransaction, int> child, Func<string, IDbTransaction,int> grandChild)
        {
            using (var connection = Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。
                    if ((result = grandChild(sysId, tran)) >= 0)
                    {
                        if ((result = child(sysId, tran)) >= 0)
                        {
                            if ((result = parent(sysId, tran)) >= 0)
                            {
                                tran.Commit();
                                return result;
                            }
                            tran.Rollback();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result;
                }
            }
        }
         
        public virtual bool Exists(string sysId)
        {
            using (var connection = Connection)
            {
                var p = CreateSysIdDynamicParameters(sysId);
                return
                    connection.Query<TEntity>(ExistsProc, p, commandType: CommandType.StoredProcedure).FirstOrDefault()
                    != null;
            }
        }

        public virtual TEntity GetModel(string sysId)
        {
            using (var connection = Connection)
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
            using (var connection = Connection)
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
            using (var connection = Connection)
            {
                return connection.Query<TEntity>(GetListProc, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TEntity> GetList(string table, string fields = "", string where = "")
        {
            using (var connection = Connection)
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