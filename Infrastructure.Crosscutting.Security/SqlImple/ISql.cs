/*
 *名称：ISql
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 09:19:02
 *修改时间：
 *备注：
 */

using System.Collections.Generic;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using System.Data;

    public interface ISql
    {
        /// <summary>
        /// 分页数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="orderBy"></param>
        /// <param name="total"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="getCount">如果为0那么会查询记录的总数，并返回total，建议第二次查询时getCount=total，减少查询次数</param>
        /// <param name="param">如果使用了 param 那么
        /// where格式为：[列]=Constant.SqlReplaceParameterPrefix[列]</param> 
        /// <returns></returns>
        IEnumerable<T> GetPaged<T>(string table, string orderBy, out int total, string fields = "*",
                                   string where = "1=1", int currentPage = 1, int pageSize = 10,
                                   int getCount = 0, object param = null);

        string AddSql { get; }

        string UpdateSql { get; }

        IDbConnection Connection { get; }

        /// <summary>
        /// 参数前缀，用于替换字符串中参数
        /// </summary>
        string ParameterPrefix { get; }
         
    }
}