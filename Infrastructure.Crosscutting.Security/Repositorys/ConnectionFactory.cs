/*
 *名称：ConnectionFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-03 16:08:36
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;
    using System.Data.SqlClient;

    public class ConnectionFactory
    {
        public static IDbConnection CreateMsSqlConnection()
        {
            const string ConnString = "Data Source=192.168.1.30;Initial Catalog=BaseDB;User Id = sa;Password=123456;";
            //const string ConnString = @"Data Source=THINKPADHOME\SQLEXPRESS;Initial Catalog=BaseDB;Integrated Security=true;";

            var conn = new SqlConnection(ConnString);
            conn.Open();
            return conn;
        }
    }
}