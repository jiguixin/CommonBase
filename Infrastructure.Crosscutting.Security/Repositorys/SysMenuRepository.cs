namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Data;

    using DapperExtensions;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysMenuRepository : DapperExtenRepository<SysMenu>
    {

        public SysButtonRepository ButtonRepository
        {
            get
            {
                return RepositoryFactory.ButtonRepository;
            }
        }

        public SysPrivilegeRepository PrivilegeRepository
        {
            get
            {
                return RepositoryFactory.PrivilegeRepository;
            }
        }


        #region Public Methods and Operators

        public override bool Delete(SysMenu item, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Delete(Predicates.Field<SysMenu>(f => f.SysId, Operator.Eq, item.SysId), transaction, commandTimeout);
        }

        public override bool Delete(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            //删除菜单时，删除按钮，同时要删除该菜单下的按钮权限 
            IDbConnection cn = transaction != null ? transaction.Connection : this.Connection;

            using (cn)
            {
                IDbTransaction tran = transaction ?? cn.BeginTransaction(IsolationLevel.ReadCommitted);
                 

                using (tran)
                { 
                    //等于0是考虑有些表并没有相关的数据，如权限表有可能没有用户SysId数据。 

                    //删除该菜单对应的权限

                    //根据predicate 查询相应的菜单数据
                    var lstMenus = GetList(predicate, transaction: tran, commandTimeout: commandTimeout);

                    //遍历所有查询出来的菜单数据
                    foreach (var menu in lstMenus)
                    {
                        if (this.PrivilegeRepository.DeleteSysPrivilegeByAccess(
                            menu.SysId,
                            (int)PrivilegeAccess.Menu,
                            tran))
                        {

                            //删除该菜单下的按钮对应的权限

                             //根据MenuId查按钮 
                                var lstButtons =
                                ButtonRepository.GetList(
                                    Predicates.Field<SysButton>(f => f.MenuId, Operator.Eq, menu.SysId),
                                    transaction: tran,
                                    commandTimeout: commandTimeout);
                            
                            //todo:用IN 各按钮的ID，去删除相应的权限数据
                             

                            /*
                             this.PrivilegeRepository.DeleteByWhere(
                                 string.Format(
                                     "PrivilegeAccessKey in (select SysId from Sys_Button where MenuId = '{0}') and PrivilegeAccess = '{1}'",
                                     sysId,
                                     (int)PrivilegeAccess.Button),
                                 tran)) >= 0)
                        {
                            if ((result += this.ButtonRepository.DeleteByMenuId(sysId, tran)) >= 0)
                            {
                                if ((result += this.Delete(sysId, tran)) >= 0)
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
 */


                        }
                    }
                }
        }

        public IEnumerable<SysButton> GetButtons(string menuId)
        {
            return this.ButtonRepository.GetList("", string.Format("{0}='{1}'", Constant.ColumnSysButtonMenuId, menuId));
        }

        #endregion

        #region Methods

         

        #endregion
    }
}