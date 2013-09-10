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
        }

        public SysPrivilegeRepository PrivilegeRepository { get; private set; }

        #region 属性

        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysRoleExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysRoleAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysRoleGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysRoleGetModel;
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

        public override int Delete(string sysId)
        {
            //todo:实现对删除角色删除相应表数据
           // return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeMaster.User, Delete, UserInfoRepository.Delete, UserRoleRepository.DeleteByUserId, PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }
    }
}
