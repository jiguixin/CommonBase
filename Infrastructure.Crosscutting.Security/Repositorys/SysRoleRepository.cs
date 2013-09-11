using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysRoleRepository:Repository<SysRole>
    {
        public SysRoleRepository()
        {
            PrivilegeRepository = new SysPrivilegeRepository();
            UserRoleRepository = new SysUserRoleRepository();
        }

        public SysPrivilegeRepository PrivilegeRepository { get; private set; }

        public SysUserRoleRepository UserRoleRepository { get; private set; }

        #region 属性

        
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysRoleAdd;
            }
        }
         
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysRoleUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysRole; }
        }
         
        #endregion

        internal override dynamic Mapping(SysRole item)
        {
            return new
                       {
                           SysId = item.SysId,
                           RoleName = item.RoleName,
                           RoleDesc = item.RoleDesc
                       };
        }

        public override int Delete(string sysId)
        {  
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeMaster.Role, Delete, UserRoleRepository.DeleteByRoleId, PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }


    }
}
