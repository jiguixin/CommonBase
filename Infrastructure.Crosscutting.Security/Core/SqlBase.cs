/*
 *名称：SqlBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 10:59:37
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Core
{
    using System.Collections.Generic;
    using System.Data;

    public abstract class SqlBase:ISql
    {
        private IAppContext appContext;

        protected SqlBase(IAppContext appContext)
        {
            this.appContext = appContext;
        }

        public virtual IEnumerable<T> GetPaged<T>(
            string table,
            string orderBy,
            out int total,
            string fields = "*",
            string where = "1=1",
            int currentPage = 1,
            int pageSize = 10,
            int getCount = 0,
            object param = null)
        {
            return appContext.GetPaged<T>(
                table,
                orderBy,
                out total,
                fields,
                where,
                currentPage,
                pageSize,
                getCount,
                param);
        }

        public abstract string AddSql { get;}

        public abstract string UpdateSql { get; }

        public IDbConnection Connection
        {
            get
            {
                return appContext.ConnectionFactory.CreateConnection();
            } 
        }

        public string ParameterPrefix
        {
            get
            {
                return appContext.ParameterPrefix;
            }
        }
    }
}