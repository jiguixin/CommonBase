using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    public class SysButtonRepository:Repository<SysButton>
    {
        public SysButtonRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysButtonSql"))
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
