using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{ 
    public class SysUserRoleRepository:Repository<SysUserRole>
    { 
        #region 属性
         
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserRoleAdd;
            }
        }
         
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserRoleUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysUserRole; }
        }

        #endregion

        public  int DeleteByUserId(string sysId, System.Data.IDbTransaction trans)
        { 
            return base.DeleteByWhere(string.Format("UserId='{0}'",sysId), trans);
        }

        public int DeleteByRoleId(string sysId, System.Data.IDbTransaction trans)
        {
            return base.DeleteByWhere(string.Format("RoleId='{0}'", sysId), trans);
        }
    }
}
