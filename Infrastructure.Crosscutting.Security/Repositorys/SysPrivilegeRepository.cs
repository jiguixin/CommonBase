using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysPrivilegeRepository:Repository<SysPrivilege>,ISysPrivilegeRepository
    {
        #region 属性

        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysPrivilegeExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysPrivilegeAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysPrivilegeGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysPrivilegeGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysPrivilegeUpdate;
            }
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
                var p = new {PrivilegeMaster = master, PrivilegeMasterKey = sysId};

                return
                    connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeMaster, p,
                        commandType: CommandType.StoredProcedure);
            }
        }

        public int DeleteSysPrivilegeByMaster(string sysId,PrivilegeMaster master,IDbTransaction trans)
        { 
            var p = new {PrivilegeMaster = (int)master, PrivilegeMasterKey = sysId};

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeMaster, p,trans,
                                         commandType: CommandType.StoredProcedure);
        }

        public int DeleteSysPrivilegeByMaster(string sysId, int masterValue, IDbTransaction trans)
        {
            var p = new { PrivilegeMaster = masterValue, PrivilegeMasterKey = sysId };

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeMaster, p,trans,
                                         commandType: CommandType.StoredProcedure);
        }

        public int DeleteSysPrivilegeByAccess(PrivilegeAccess access, string sysId)
        {
            using (var connection = Connection)
            {
                var p = new { PrivilegeAccess = access, PrivilegeAccessKey = sysId };

                return
                    connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeAccess, p,
                        commandType: CommandType.StoredProcedure);
            }
        }

        public int DeleteSysPrivilegeByAccess(string sysId, PrivilegeAccess access, IDbTransaction trans)
        {
            var p = new { PrivilegeAccess = (int)access, PrivilegeAccessKey = sysId };

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeAccess, p, trans,
                                         commandType: CommandType.StoredProcedure);
        }

        public int DeleteSysPrivilegeByAccess(string sysId, int accessValue, IDbTransaction trans)
        {
            var p = new { PrivilegeAccess = accessValue, PrivilegeAccessKey = sysId };

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeAccess, p, trans,
                                         commandType: CommandType.StoredProcedure);
        }

        #region Helper

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
                    int result;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。

                    if ((result = grandChild(sysId, enumValue, tran)) >= 0)
                    {
                        if ((result = child(sysId, tran)) >= 0)
                        {
                            if ((result = parent(sysId, tran)) >= 0)
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
                    int result;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。

                    if ((result = reGrandChild(sysId, enumValue, tran)) >= 0)
                    {
                        if ((result = grandChild(sysId, tran)) >= 0)
                        {
                            if ((result = child(sysId, tran)) >= 0)
                            {
                                if ((result = parent(sysId, tran)) >= 0)
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
