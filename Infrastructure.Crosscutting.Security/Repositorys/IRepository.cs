/*
 *名称：IRepository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 09:50:37
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        int Add(TEntity item);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="sysId">Item to delete</param>
        int Delete(string sysId);
          
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="item"></param>
        int Update(TEntity item);

        /// <summary>
        /// 检查该记录是否存在
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns>如果存在返回：true 否则返回:false</returns>
        bool Exists(string sysId);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="sysId">entity key values, the order the are same of order in mapping.</param>
        /// <returns></returns>
        TEntity GetModel(string sysId);

        /// <summary>
        /// 分页查询
        /// 注意:多表联查,如果两个表有相同的列名,必须指定要查询的列名,不然会报错
        /// </summary>
        /// <param name="table">表名、视图,支持多表联查</param>
        /// <param name="fields">字段名,不指定为'*'</param>
        /// <param name="where">where条件,不需要加where</param>
        /// <param name="orderBy">排序条件，不需要加order by</param>
        /// <param name="currentPage">当前页,从1开始,不是0</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="getCount">获取的记录总数，0则获取记录总数，不为0则不获取</param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(
            string table,
            string fields,
            string where,
            string orderBy,
            int currentPage,
            int pageSize,
            int getCount,
            out int total);

        /// <summary>
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetList();

        IEnumerable<TEntity> GetList(string table, string fields="", string where="");

    }
}