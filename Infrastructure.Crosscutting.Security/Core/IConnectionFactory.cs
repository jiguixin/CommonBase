/*
 *名称：IConnectionFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 10:46:44
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Core
{
    using System.Data;

    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// 给子类创建具体的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }
}