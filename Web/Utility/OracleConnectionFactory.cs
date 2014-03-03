﻿/*
 *名称：OracleConnectionFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 11:29:09
 *修改时间：
 *备注：
 */

namespace Web.Utility
{
    using System.Data;

    using Infrastructure.Crosscutting.Security.Core;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class OracleConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return ConnectionFactory.CreateOracleConnection();
        }
    }
}