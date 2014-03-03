/*
 *名称：SqlServerConnectionFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:55:28
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Test
{
    using System.Data;

    using Infrastructure.Crosscutting.Security.Core;

    public class SqlServerConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return ConnectionFactory.CreateMsSqlConnection();
        }
    }
}