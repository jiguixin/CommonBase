using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    public class SysMenuRepository:Repository<SysMenu>
    {
        public SysPrivilegeRepository PrivilegeRepository { get; private set; }

        public SysButtonRepository ButtonRepository { get; private set; }

        public SysMenuRepository()
        {
            PrivilegeRepository = RepositoryFactory.PrivilegeRepository;
            ButtonRepository = RepositoryFactory.ButtonRepository;  
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

        internal override dynamic Mapping(SysMenu item)
        {
            return new
            {
                SysId = item.SysId,
                MenuParentId = item.MenuParentId,
                MenuOrder = item.MenuOrder,
                MenuName = item.MenuName,
                MenuLink = item.MenuLink,
                MenuIcon = item.MenuIcon,
                IsVisible = item.IsVisible,
                IsLeaf = item.IsLeaf,
                RecordStatus = item.RecordStatus
            };
        }

        public IEnumerable<SysButton> GetButtons(string menuId)
        {
            return ButtonRepository.GetList("", string.Format("{0}='{1}'", Constant.ColumnSysButtonMenuId, menuId));
        }


        public override int Delete(string sysId)
        {
            //删除菜单时，删除按钮，同时要删除该菜单下的按钮权限 
            using (var connection = Connection)
            {
                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int result = 0;
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。 

                    //删除该菜单对应的权限
                    if (
                        (result += PrivilegeRepository.DeleteSysPrivilegeByAccess(sysId, (int)PrivilegeAccess.Menu, tran))
                        >= 0)
                    {

                        //删除该菜单下的按钮对应的权限
                        if (
                            (result +=
                             PrivilegeRepository.DeleteByWhere(
                                 string.Format(
                                     "PrivilegeAccessKey in (select SysId from Sys_Button where MenuId = '{0}') and PrivilegeAccess = '{1}'",
                                     sysId,
                                     (int)PrivilegeAccess.Button),
                                 tran)) >= 0)
                        {
                            if ((result += ButtonRepository.DeleteByMenuId(sysId, tran)) >= 0)
                            {
                                if ((result += Delete(sysId, tran)) >= 0)
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
    }
}
