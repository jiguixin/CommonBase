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
    public interface ISql
    {
        IEnumerable<T> GetPaged<T>(string table, string orderBy, out int total, string fields = "*",
                                   string where = "1=1", int currentPage = 1, int pageSize = 10,
                                   int getCount = 0);

        string AddSql { get; }

        string UpdateSql { get; }

    }
}