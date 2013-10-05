/*
 *名称：Repository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 14:05:09
 *修改时间：
 *备注：
 */

using System;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
      
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public abstract class  Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    { 
        #region Abstract Property
           
        public abstract string TableName { get; }

        #endregion

        private ISql sql;

        protected Repository(ISql sql)
        {
            this.sql = sql;
        }


        #region Public Method

        internal virtual dynamic Mapping(TEntity item)
        {
            return item;
        }

        public virtual IDbConnection Connection
        {
            get { return ConnectionFactory.CreateMsSqlConnection(); }
        }

        /// <summary>
        /// 添加时可以不给实体的SysId赋值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int Add(TEntity item)
        {
            using (var connection = Connection)
            {
                ChkSysId(item);

                var parameters = (object)Mapping(item);

                return connection.Execute(sql.AddSql, parameters,
                    commandType: CommandType.Text); 
            }
        }
         
        /// <summary>
        /// 添加时可以不给实体的SysId赋值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public virtual int Add(TEntity item, IDbTransaction trans)
        {
            ChkSysId(item);
            var parameters = (object)Mapping(item);
            return trans.Connection.Execute(sql.AddSql, parameters, transaction: trans,
                    commandType: CommandType.Text); 
        }

        public virtual int Delete(string sysId)
        {
            using (var connection = Connection)
            {
                var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}'",Constant.ColumnSysId, sysId)); 
                return
                    connection.Execute(sqlDelete,
                        commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 根据条件删除表数据，不用加where
        /// </summary>
        /// <param name="condition">格式为[列]=‘a’</param> 
        public int DeleteByWhere(string condition)
        {
            using (var connection = Connection)
            {
                var sqlDelete = CreateDeleteSql(condition);
                  
                return
                    connection.Execute(sqlDelete,
                        commandType: CommandType.Text);
            }
        }

        public virtual int Delete(string sysId, IDbTransaction trans)
        {
            var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}'", Constant.ColumnSysId, sysId));

            return trans.Connection.Execute(sqlDelete, transaction: trans,
                   commandType: CommandType.Text); 
        }

        /// <summary>
        /// 根据条件删除表数据，不用加where
        /// </summary>
        /// <param name="condition">格式为[列]=‘a’</param> 
        public virtual int DeleteByWhere(string condition, IDbTransaction trans)
        {
            var sqlDelete = CreateDeleteSql(condition);

            return trans.Connection.Execute(sqlDelete, transaction: trans,
                   commandType: CommandType.Text);
        }

        public virtual int Update(TEntity item)
        {
            using (var connection = Connection)
            {
                var parameters = (object)Mapping(item);

                return connection.Execute(sql.UpdateSql, parameters,
                    commandType: CommandType.Text);
            }
        }

        public virtual int Update(TEntity item, IDbTransaction trans)
        {
            var parameters = (object) Mapping(item);

            return trans.Connection.Execute(sql.UpdateSql, parameters, transaction: trans,
                                            commandType: CommandType.Text);
        }

        public virtual int AddOrModifyTrans<TP, TC>(TP item, TC childValue, Func<TP, IDbTransaction, int> parent, Func<TC, IDbTransaction, int> child)
        {
            using (var connection = Connection)
            {
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
            IEnumerable<int> lstResult = GetList<int>(Constant.SqlCount, CreateSysIdCondition(sysId));

            if (lstResult.FirstOrDefault() > 0)
            {
                return true;
            }
            return false;
        }

        public virtual TEntity GetModel(string sysId)
        {
            return GetList(string.Empty, CreateSysIdCondition(sysId)).FirstOrDefault();
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
            return  sql.GetPaged<TEntity>(table, orderBy, out total, fields, where, currentPage, pageSize, getCount);
        }

        public IEnumerable<TEntity> GetList()
        {
            return GetList(string.Empty, string.Empty);
        }

        /// <summary>
        /// 注：必须要配置子类TableName
        /// </summary> 
        public IEnumerable<TEntity> GetList(string fields = null, string where = null)
        {
            using (var connection = Connection)
            { 
                return connection.Query<TEntity>(CreateSelectSql(TableName,fields,where), commandType: CommandType.Text);
            }
        }

        public IEnumerable<T> GetList<T>(string fields = null, string where = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(CreateSelectSql(TableName, fields, where), commandType: CommandType.Text);
            }
        }

        public IEnumerable<T> GetList<T>(string table, string fields = null, string where = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(CreateSelectSql(table, fields, where), commandType: CommandType.Text);
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// 在添加时检查SysId是否有值，如果没有值就默认赋值
        /// </summary>
        /// <param name="item"></param>
        private static void ChkSysId(TEntity item)
        {
            if (string.IsNullOrEmpty(item.SysId))
            {
                item.SysId = Util.NewId();
            }
        }

        /// <summary>
        /// 结果为：SysId='sysId'
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public string CreateSysIdCondition(string sysId)
        {
            return string.Format("{0}='{1}'", Constant.ColumnSysId, sysId);
        }

        private static DynamicParameters CreateSysIdDynamicParameters(string sysId)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysId, sysId, DbType.String, ParameterDirection.Input, 50);
            return p;
        }

        //创建调用proc_Delete_By_Where的参数
        public string CreateDeleteSql(string where)
        {
            where = string.IsNullOrEmpty(where) ? "1=1" : where;

            string sql = string.Format("delete from {0} where {1}", TableName, where);

            return sql;
        }

        public string CreateSelectSql(string tableName,string fields, string where)
        {
            fields = string.IsNullOrEmpty(fields) ? "*" : fields;
            where = string.IsNullOrEmpty(where) ? "1=1" : where;

            string sql = string.Format("select {0} from {1} where {2}", fields, tableName, where);

            return sql;
        }


        #endregion
         
    }
}