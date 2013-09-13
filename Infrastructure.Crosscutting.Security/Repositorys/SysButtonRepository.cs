using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    using Infrastructure.Data.Ado.Dapper;

    public class SysButtonRepository:Repository<SysButton>
    {
        public SysButtonRepository()
        {
            PrivilegeRepository = new SysPrivilegeRepository(); 
        }
        #region 属性
 
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysButtonAdd;
            }
        }
         
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysButtonUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysButton; }
        }

        public SysPrivilegeRepository PrivilegeRepository { get; private set; }
         
          
        #endregion

        internal override dynamic Mapping(SysButton item)
        {
            return new
                       {
                           SysId = item.SysId,
                           MenuId = item.MenuId,
                           BtnName = item.BtnName,
                           BtnIcon = item.BtnIcon,
                           BtnOrder = item.BtnOrder,
                           RecordStatus = item.RecordStatus
                       };
        }
         
        public int DeleteByMenuId(string menuId, IDbTransaction trans)
        {
            return base.DeleteByWhere(string.Format("{0}='{1}'",Constant.ColumnSysButtonMenuId, menuId), trans);
        }

        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeAccess.Button, Delete, PrivilegeRepository.DeleteSysPrivilegeByAccess);
        }
    }
}
