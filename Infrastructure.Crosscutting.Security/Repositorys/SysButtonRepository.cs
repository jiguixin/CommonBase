﻿using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    using Infrastructure.Crosscutting.Security.Core;

    public class SysButtonRepository:Repository<SysButton>
    { 
        public SysButtonRepository(ISql sql)
            : base(sql)
        {
        }

        #region 属性
  

        public override string TableName
        {
            get { return Constant.TableSysButton; }
        }

        public SysPrivilegeRepository PrivilegeRepository
        {
            get { return RepositoryFactory.PrivilegeRepository; }
        }


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
                           BtnFunction=item.BtnFunction,
                           RecordStatus = item.RecordStatus,
                           IsVisible = item.IsVisible
                       };
        }
         
        public int DeleteByMenuId(string menuId, IDbTransaction trans)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysButtonMenuId, menuId.Trim());

            return base.DeleteByWhere(string.Format("{1}={0}{1}", Constant.SqlReplaceParameterPrefix, Constant.ColumnSysButtonMenuId),trans, p);
            //return base.DeleteByWhere(string.Format("{0}='{1}'",Constant.ColumnSysButtonMenuId, menuId), trans);
        }

        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeAccess.Button, Delete, PrivilegeRepository.DeleteSysPrivilegeByAccess);
        }
    }
}
