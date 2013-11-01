using System.Collections.Generic;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;
    using System.Linq;

    public class SysMenuRepository:Repository<SysMenu>
    {
        public SysPrivilegeRepository PrivilegeRepository { get
        {
            return RepositoryFactory.PrivilegeRepository;
        }}

        public SysButtonRepository ButtonRepository { get { return RepositoryFactory.ButtonRepository; } }

        public SysMenuRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysMenuSql"))
        {  
        }

        #region 属性
          
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
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysButtonMenuId, menuId.Trim());
            return ButtonRepository.GetListByTable<SysButton>(Constant.TableSysButton,
                                                              "SysId,MenuId,BtnName,BtnIcon,BtnOrder,BtnFunction,IsVisible,RecordStatus",
                                                              string.Format("{1}={0}{1}",
                                                                            Constant.SqlReplaceParameterPrefix,
                                                                            Constant.ColumnSysButtonMenuId), p);
           // return ButtonRepository.GetList("", string.Format("{0}='{1}'", Constant.ColumnSysButtonMenuId, menuId));
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

       
        /// <summary>
        /// 得到有层级关系的菜单集合
        /// </summary>
        /// <param name="hasButtons">获取菜单是否包含菜单下按钮</param>
        /// <returns></returns>
        public virtual IEnumerator<SysMenu> GetAllMenusByLoop(bool hasButtons)
        { 
            var lstSource = GetList();

            if (lstSource == null)
                return null;

            //得到所有父菜单
           var lstParent = lstSource.Where(p => string.IsNullOrEmpty(p.MenuParentId));

            foreach (var m in lstParent)
            {
                var childs = GetChildre(lstSource, m,hasButtons);
                if (childs != null && childs.Any()) m.Children = childs.OrderBy(c => c.MenuOrder);
            }

            return (IEnumerator<SysMenu>)lstParent;
        }

        public IEnumerable<SysMenu> GetChildre(IEnumerable<SysMenu> lstSource, SysMenu sysMenu,bool hasButtons)
        {
            var lstResult = lstSource.Where(m => m.MenuParentId == sysMenu.SysId);

            if (!lstResult.Any()) return null;

            foreach (var menu in lstResult)
            {
                if (hasButtons)
                {
                    menu.Buttons = GetButtons(menu.SysId);
                }
                menu.Children = GetChildre(lstSource, menu,hasButtons);
                
            }

            return lstResult;
        }



    }
}
