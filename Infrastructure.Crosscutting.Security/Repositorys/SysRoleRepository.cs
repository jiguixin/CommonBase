namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysRoleRepository : Repository<SysRole>
    {
        #region Public Properties

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysRoleAdd;
            }
        }

        public SysPrivilegeRepository PrivilegeRepository
        {
            get
            {
                return RepositoryFactory.PrivilegeRepository;
            }
        }

        public override string TableName
        {
            get
            {
                return Constant.TableSysRole;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysRoleUpdate;
            }
        }

        public SysUserRoleRepository UserRoleRepository
        {
            get
            {
                return RepositoryFactory.UserRoleRepository;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override int Delete(string sysId)
        {
            return this.PrivilegeRepository.DeletePrivilegeTrans(
                sysId,
                (int)PrivilegeMaster.Role,
                this.Delete,
                this.UserRoleRepository.DeleteByRoleId,
                this.PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }

        #endregion

        #region Methods

        internal override dynamic Mapping(SysRole item)
        {
            return new { item.SysId, item.RoleName, item.RoleDesc };
        }

        #endregion
    }
}