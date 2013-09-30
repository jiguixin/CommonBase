using System;
using System.Data;

using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;

    using Dapper;

    using DapperExtensions;

    public class SysPrivilegeRepository : DapperExtenRepository<SysPrivilege>
    {
        
        public bool DeleteSysPrivilegeByMaster(string sysId, int masterValue, IDbTransaction trans = null)
        {
            IDbConnection cn = trans != null ? trans.Connection : this.Connection;

            using (cn)
            { 
                var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                 
                pg.Predicates.Add(Predicates.Field<SysPrivilege>(f => f.PrivilegeMaster, Operator.Eq, masterValue));
                pg.Predicates.Add(Predicates.Field<SysPrivilege>(f => f.PrivilegeMasterKey, Operator.Eq, sysId));
                 
                return this.Delete(pg);
            } 
        } 

        public bool DeleteSysPrivilegeByAccess(string sysId, int accessValue, IDbTransaction trans = null)
        {  
            IDbConnection cn = trans != null ? trans.Connection : this.Connection;

            using (cn)
            {
                var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                pg.Predicates.Add(Predicates.Field<SysPrivilege>(f => f.PrivilegeAccess, Operator.Eq, accessValue));
                pg.Predicates.Add(Predicates.Field<SysPrivilege>(f => f.PrivilegeAccessKey, Operator.Eq, sysId));

                return this.Delete(pg);
            } 
        }
         

        #region Helper

        //删除用户时要删除用户角色表同时要删除用户对应的权限数据
        //删除菜单时要删除按钮表同时要删除菜单对应的权限数据
        /// <summary>
        /// 删除2级关系表数据
        /// </summary> 
        /// <returns></returns>
        public int DeletePrivilegeTrans(string sysId, int enumValue, Func<string, IDbTransaction, int> parent, Func<string,int,IDbTransaction, int> child)
        {
            using (var connection = Connection)
            {
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result = 0;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。
                    if ((result += child(sysId,enumValue, tran)) >= 0)
                    {
                        if ((result += parent(sysId, tran)) >= 0)
                        {
                            tran.Commit();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result; 
                }
            }
        }

        //删除用户时要删除用户角色表同时要删除用户对应的权限数据
        //删除菜单时要删除按钮表同时要删除菜单对应的权限数据
        /// <summary>
        /// 删除3级关系表数据
        /// </summary> 
        /// <returns></returns>
        public int DeletePrivilegeTrans(string sysId, int enumValue, Func<string, IDbTransaction, int> parent, Func<string, IDbTransaction, int> child, Func<string, int, IDbTransaction, int> grandChild)
        {
            using (var connection = Connection)
            {
               
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result = 0;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。

                    if ((result += grandChild(sysId, enumValue, tran)) >= 0)
                    {
                        if ((result += child(sysId, tran)) >= 0)
                        {
                            if ((result += parent(sysId, tran)) >= 0)
                            {
                                tran.Commit();
                                return result;
                            }
                            tran.Rollback();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result;
                }
            }
        }

        /// <summary>
        /// 删除4级关系表数据
        /// </summary> 
        /// <returns></returns>
        public int DeletePrivilegeTrans(string sysId, int enumValue, Func<string, IDbTransaction, int> parent, Func<string, IDbTransaction, int> child, Func<string, IDbTransaction, int> grandChild, Func<string, int, IDbTransaction, int> reGrandChild)
        {
            using (var connection = Connection)
            { 
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result = 0;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。

                    if ((result += reGrandChild(sysId, enumValue, tran)) >= 0)
                    {
                        if ((result += grandChild(sysId, tran)) >= 0)
                        {
                            if ((result += child(sysId, tran)) >= 0)
                            {
                                if ((result += parent(sysId, tran)) >= 0)
                                {
                                    tran.Commit();
                                    return result;
                                }
                                tran.Rollback();
                                return result;
                            }
                            tran.Rollback();
                            return result;
                        }
                        tran.Rollback();
                        return result;
                    }
                    tran.Rollback();
                    return result;
                }
            }
        }

        #endregion

    }
}
