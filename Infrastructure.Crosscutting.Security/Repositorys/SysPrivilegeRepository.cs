using System;
using System.Data;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysPrivilegeRepository:Repository<SysPrivilege>
    {
        #region 属性

        public SysPrivilegeRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysPrivilegeSql"))
        {
        }

        public override string TableName
        {
            get { return Constant.TableSysPrivilege; }
        }

        #endregion
         
        public int DeleteSysPrivilegeByMaster(PrivilegeMaster master, string sysId)
        {
            using (var connection = Connection)
            {
                var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}' AND {2}='{3}'", Constant.ColumnSysPrivilegePrivilegeMaster, (int)master, Constant.ColumnSysPrivilegePrivilegeMasterKey, sysId));
                 
                return
                    connection.Execute(sqlDelete, 
                        commandType: CommandType.Text);
            }
        }

        public int DeleteSysPrivilegeByMaster(string sysId,PrivilegeMaster master,IDbTransaction trans)
        {
            var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}' AND {2}='{3}'", Constant.ColumnSysPrivilegePrivilegeMaster, (int)master, Constant.ColumnSysPrivilegePrivilegeMasterKey, sysId));
              
            return
                trans.Connection.Execute(sqlDelete, transaction: trans,
                                         commandType: CommandType.Text);
        }

        public int DeleteSysPrivilegeByMaster(string sysId, int masterValue, IDbTransaction trans)
        {
            var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}' AND {2}='{3}'", Constant.ColumnSysPrivilegePrivilegeMaster, masterValue, Constant.ColumnSysPrivilegePrivilegeMasterKey, sysId));

            return
                trans.Connection.Execute(sqlDelete,transaction:trans,
                                         commandType: CommandType.Text);
        }

        public int DeleteSysPrivilegeByAccess(PrivilegeAccess access, string sysId)
        {
            using (var connection = Connection)
            {
                var sqlDelete = CreateDeleteSql(string.Format("{0}='{1}' AND {2}='{3}'", Constant.ColumnSysPrivilegePrivilegeAccess, (int)access, Constant.ColumnSysPrivilegePrivilegeAccessKey, sysId));

                return
                    connection.Execute(sqlDelete,
                        commandType: CommandType.Text);
            }
        }

        public int DeleteSysPrivilegeByAccess(string sysId, PrivilegeAccess access, IDbTransaction trans)
        {
            var sqlDelete =
                CreateDeleteSql(
                    string.Format(
                        "{0}='{1}' AND {2}='{3}'",
                        Constant.ColumnSysPrivilegePrivilegeAccess,
                        (int)access,
                        Constant.ColumnSysPrivilegePrivilegeAccessKey,
                        sysId));

            return trans.Connection.Execute(
                sqlDelete,
                transaction: trans,
                commandType: CommandType.Text);
        }

        public int DeleteSysPrivilegeByAccess(string sysId, int accessValue, IDbTransaction trans)
        {
            var sqlDelete =
                CreateDeleteSql(
                    string.Format(
                        "{0}='{1}' AND {2}='{3}'",
                        Constant.ColumnSysPrivilegePrivilegeAccess,
                        accessValue,
                        Constant.ColumnSysPrivilegePrivilegeAccessKey,
                        sysId));

            return trans.Connection.Execute(
                sqlDelete,
                transaction: trans,
                commandType: CommandType.Text);
        }

        public int AddSysPrivilegeByAccess(SysPrivilege sysPrivilege,string userName, IDbTransaction trans)
        {
            var p = new DynamicParameters();
            p.Add("@SysId", Guid.NewGuid());
            p.Add("@PrivilegeMaster", sysPrivilege.PrivilegeMaster);
            p.Add("@PrivilegeMasterKey", sysPrivilege.PrivilegeMasterKey);
            p.Add("@PrivilegeAccess", sysPrivilege.PrivilegeAccess);
            p.Add("@PrivilegeAccessKey", sysPrivilege.PrivilegeAccessKey);
            p.Add("@PrivilegeOperation", sysPrivilege.PrivilegeOperation);
            p.Add("@RecordStatus", string.Format("创建时间：{0},创建人：{1}", DateTime.Now, userName));

            int result = trans.Connection.Execute("Sys_Privilege_ADD", p, trans, commandType: CommandType.StoredProcedure);
            return result;
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
