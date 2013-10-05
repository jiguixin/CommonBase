/*
 *名称：DapperExtenRepository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-30 16:03:53
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Data;
    
    using Infrastructure.Crosscutting.Security.Model;

    using DapperExtensions;

    public class DapperExtenRepository<T> where T : EntityBase
    {
        public virtual IDbConnection Connection
        {
            get { return ConnectionFactory.CreateMsSqlConnection(); }
        }

        public virtual decimal Add(T item, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn) 
            {
                return cn.Insert(item, transaction, commandTimeout);
            } 
        }

        public virtual void Add(IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                  cn.Insert(entities, transaction, commandTimeout);
            }
        }

        public virtual bool Update(T item, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                return cn.Update(item, transaction, commandTimeout);
            } 
        }

        public virtual bool Delete(T item, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                return cn.Delete(item, transaction, commandTimeout);
            }
        }

        public virtual bool Delete(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn  = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                return cn.Delete(predicate, transaction, commandTimeout);
            }
        }
         

        public bool Exists(string sysId)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            { 
                return cn.Get<T>(id, transaction, commandTimeout); 
            }  
        }
          
        public IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false)
        {
             
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                return cn.GetList<T>(predicate, sort, transaction, commandTimeout, buffered);
            }   
        }
         
    }
}