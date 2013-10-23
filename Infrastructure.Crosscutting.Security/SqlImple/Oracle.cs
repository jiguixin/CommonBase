/*
 *名称：Oracle
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-08 13:56:11
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Repositorys;
    using Infrastructure.Data.Ado.Dapper;

    public abstract class Oracle:ISql
    {
        public virtual IEnumerable<T> GetPaged<T>(
            string table,
            string orderBy,
            out int total,
            string fields = "*",
            string @where = "1=1",
            int currentPage = 1,
            int pageSize = 10,
            int getCount = 0, object param = null)
        {
            fields = string.IsNullOrEmpty(fields) ? "*" : fields;
            where = string.IsNullOrEmpty(where) ? "1=1" : where;
            currentPage = currentPage == 0 ? 1 : currentPage;
            pageSize = pageSize == 0 ? 10 : pageSize;

            using (var cn = ConnectionFactory.CreateOracleConnection())
            {
                #region 对Order By 进行处理

                if (string.IsNullOrEmpty(orderBy))
                {
                    string tempTable;
                    //多表联查如果没有提供排序字段,自动找第一个表的主键进行排序
                    if (table.IndexOf(" on ", System.StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        tempTable = table.Substring(0, table.IndexOf(" ", System.StringComparison.Ordinal));
                    }
                    else if (table.IndexOf(",", System.StringComparison.Ordinal) > 0)
                    {
                        tempTable = table.Substring(0, table.IndexOf(",", System.StringComparison.Ordinal));

                        //如果有别名如Article a,User u
                        if (tempTable.IndexOf(" ", System.StringComparison.Ordinal) > 0)
                        {
                            tempTable = tempTable.Substring(0, tempTable.IndexOf(" ", System.StringComparison.Ordinal));
                        }
                    }
                    else
                        tempTable = table; //单表查询

                    //将表名转换为大写，因为oracle的表名表都是大写形式
                    tempTable = tempTable.ToUpper();

                    if (
                        cn.Query<int>(string.Format("select count(*) from user_tables where table_name='{0}'", tempTable))
                          .FirstOrDefault() == 0)
                    {
                        throw new Exception("查询的表不存在");
                    }

                    orderBy = cn.Query<string>(
                        string.Format(@"select cu.column_name 
from user_cons_columns cu, 
user_constraints au where cu.constraint_name = au.constraint_name and
 au.constraint_type = 'P' and au.table_name = '{0}'", tempTable))
                                .FirstOrDefault();
                    if (string.IsNullOrEmpty(orderBy))
                    {
                        throw new Exception("必须指定Order By 字段");
                    }
                }

                #endregion

                where = " WHERE " + where;

                int startRow = (currentPage - 1) * pageSize + 1;
                int endRow = currentPage * pageSize;

                if (getCount == 0)
                {
                    string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM {0}{1}", table, where);

                    sqlQueryCount = Util.ReplaceParameterPrefix(param, sqlQueryCount, ParameterPrefix);

                    total = cn.Query<int>(sqlQueryCount).FirstOrDefault();
                }
                else
                {
                    total = getCount;
                }

                string sqlQuery = string.Format(@"
                 SELECT {0} FROM 
                    ( 
                    SELECT T.*, ROWNUM RN 
                    FROM (SELECT {0} FROM {1} {2} ORDER BY {3}) T 
                    WHERE ROWNUM <= {5} 
                    ) 
                    WHERE RN >= {4} ", fields, table, where, orderBy, startRow, endRow);

                sqlQuery = Util.ReplaceParameterPrefix(param, sqlQuery, ParameterPrefix);

                return cn.Query<T>(sqlQuery);
            }
             
        }

        public abstract string AddSql { get; }
        public abstract string UpdateSql { get;}

        public IDbConnection Connection
        {
            get
            {
                return ConnectionFactory.CreateOracleConnection();
            }
        }

        public string ParameterPrefix
        {
            get
            {
                return Constant.OracleParameterPrefix;
            }
        }
         
    }
}