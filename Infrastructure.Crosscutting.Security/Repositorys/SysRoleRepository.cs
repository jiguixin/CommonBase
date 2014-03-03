using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysRoleRepository:Repository<SysRole>
    {
        public SysRoleRepository(ISql sql)
            : base(sql)
        {

        }
          
        public SysPrivilegeRepository PrivilegeRepository
        {
            get { return RepositoryFactory.PrivilegeRepository; }
        }

        public SysUserRoleRepository UserRoleRepository
        {
            get { return RepositoryFactory.UserRoleRepository; }
        }

        #region 属性
         
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
                           RoleDesc = item.RoleDesc,
                           RecordStatus = item.RecordStatus
                       };
        }

        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeMaster.Role, Delete, UserRoleRepository.DeleteByRoleId, PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }


    }
}
