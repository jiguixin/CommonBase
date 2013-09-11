using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysMenuRepository:Repository<SysMenu>
    {
        public SysPrivilegeRepository PrivilegeRepository { get; private set; }

        public SysButtonRepository ButtonRepository { get; private set; }

        public SysMenuRepository() 
        {
            PrivilegeRepository = new SysPrivilegeRepository();
            ButtonRepository = new SysButtonRepository();
        }

        #region 属性
         
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysMenuAdd;
            }
        }
         
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysMenuUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysMenu; }
        }

        #endregion

        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeAccess.Menu, Delete, ButtonRepository.DeleteByMenuId, PrivilegeRepository.DeleteSysPrivilegeByAccess); 
        }
    }
}
