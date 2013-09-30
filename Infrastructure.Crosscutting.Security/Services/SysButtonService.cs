namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysButtonService : ISysButtonService
    {
        #region Public Properties

        public SysButtonRepository ButtonRepository
        {
            get
            {
                return RepositoryFactory.ButtonRepository;
            }
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<SysPrivilege> GetPrivilege(string buttonId)
        {
            return this.ButtonRepository.GetList<SysPrivilege>(
                Constant.SqlTableButtonPrivilegeJoin,
                Constant.SqlFieldsPrivilegeJoin,
                string.Format("p.PrivilegeAccess={0} and b.SysId = '{1}'", (int)PrivilegeAccess.Button, buttonId));
        }

        #endregion
    }
}