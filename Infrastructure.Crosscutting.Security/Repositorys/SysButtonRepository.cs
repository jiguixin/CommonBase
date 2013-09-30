namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysButtonRepository : DapperExtenRepository<SysButton>
    {
        #region Public Properties
         

        public SysPrivilegeRepository PrivilegeRepository
        {
            get
            {
                return RepositoryFactory.PrivilegeRepository;
            }
        }
         

        #endregion

        #region Public Methods and Operators
//
//        public override int Delete(string sysId)
//        {
//            return this.PrivilegeRepository.DeletePrivilegeTrans(
//                sysId,
//                (int)PrivilegeAccess.Button,
//                this.Delete,
//                this.PrivilegeRepository.DeleteSysPrivilegeByAccess);
//        }
//
//        public int DeleteByMenuId(string menuId, IDbTransaction trans)
//        {
//            return base.DeleteByWhere(string.Format("{0}='{1}'", Constant.ColumnSysButtonMenuId, menuId), trans);
//        }

        #endregion

        #region Methods

//        internal override dynamic Mapping(SysButton item)
//        {
//            return new { item.SysId, item.MenuId, item.BtnName, item.BtnIcon, item.BtnOrder, item.RecordStatus };
//        }

        #endregion
    }
}