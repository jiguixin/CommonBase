/*
 *名称：SqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 09:22:15
 *修改时间：
 *备注：
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Crosscutting.Security.Repositorys;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public abstract class SqlServer : ISql
    {
        public IEnumerable<T> GetPaged<T>(string table, string orderBy, out int total, string fields = "*", string where = "1=1", int currentPage = 1, int pageSize = 10,
                                       int getCount = 0)
        {
            fields = string.IsNullOrEmpty(fields) ? "*" : fields;
            where = string.IsNullOrEmpty(where) ? "1=1" : where;
            currentPage = currentPage == 0 ? 1 : currentPage;
            pageSize = pageSize == 0 ? 10 : pageSize;

            using (var cn = ConnectionFactory.CreateMsSqlConnection())
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

                    if (
                        cn.Query<int>(string.Format("select count(*) from sysobjects where [name]='{0}'", tempTable))
                          .FirstOrDefault() == 0)
                    {
                        throw new Exception("查询的表不存在");
                    }

                    orderBy = cn.Query<string>(
                        string.Format(@"select d.name from sysindexes a,sysobjects b,sysindexkeys c,syscolumns d 
        where c.id = object_id('{0}') and c.id = b.parent_obj   
            and a.name = b.name and b.xtype= 'PK ' and a.indid = 1 and d.colid = c.colid and d.id = c.id", tempTable))
                                .FirstOrDefault();
                    if (string.IsNullOrEmpty(orderBy))
                    {
                        throw new Exception("必须指定Order By 字段");
                    }
                }

                #endregion

                where = " WHERE " + where;

                int startRow = (currentPage - 1) * pageSize+1;
                int endRow = currentPage*pageSize;

                if (getCount == 0)
                {
                    string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM {0}{1}", table, where);

                    total = cn.Query<int>(sqlQueryCount).FirstOrDefault();
                }
                else
                {
                    total = getCount;
                }

                string sqlQuery = string.Format(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS rownumber,{1} FROM {2}{3}) AS tempdt WHERE rownumber BETWEEN {4} AND {5}",orderBy,fields,table,where,startRow,endRow);

               return cn.Query<T>(sqlQuery); 
            } 
        }

        public abstract string AddSql { get;}
        public abstract string UpdateSql { get;  }
    }
}