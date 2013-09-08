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
        #region 存储过程名
          
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

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysPrivilegeDelete;
            }
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
            var p = new {PrivilegeMaster = master, PrivilegeMasterKey = sysId};

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeMaster, p,
                                         commandType: CommandType.StoredProcedure);
        }

        public int DeleteSysPrivilegeByMaster(string sysId, int masterValue, IDbTransaction trans)
        {
            var p = new { PrivilegeMaster = masterValue, PrivilegeMasterKey = sysId };

            return
                trans.Connection.Execute(Constant.SysPrivilegeDeleteByPrivilegeMaster, p,
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
    }
}
