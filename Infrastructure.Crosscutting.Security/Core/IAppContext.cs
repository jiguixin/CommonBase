/*
 *名称：IAppContext
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 10:48:42
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 应用程序上下文，主要根据具体的实现构造，ISql中的，GetPaged，Connection，ParameterPrefix值
    /// </summary>
    public interface IAppContext
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
        
        /// <summary>
        /// 数据库连接工厂
        /// </summary>
        IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        /// 参数前缀，用于替换字符串中参数
        /// </summary>
        string ParameterPrefix { get; }

    }
}