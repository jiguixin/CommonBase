namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Data;

    using Dapper;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysUserRepository : Repository<SysUser>
    {
        #region Public Properties

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserAdd;
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
                return Constant.TableSysUser;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserUpdate;
            }
        }

        public SysUserInfoRepository UserInfoRepository
        {
            get
            {
                return RepositoryFactory.UserInfoRepository;
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

        public override int Add(SysUser item)
        {
            return this.AddOrModifyTrans(item, item.UserInfo, this.Add, this.UserInfoRepository.Add);
        }

        /// <summary>
        ///     删除用户时，同时删除，权限表的用户数据，用户和角色表的数据，及用户表中的数据
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public override int Delete(string sysId)
        {
            return this.PrivilegeRepository.DeletePrivilegeTrans(
                sysId,
                (int)PrivilegeMaster.User,
                this.Delete,
                this.UserInfoRepository.Delete,
                this.UserRoleRepository.DeleteByUserId,
                this.PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }

        public override SysUser GetModel(string sysId)
        {
            SysUser sysUser = base.GetModel(sysId);

            if (sysUser == null)
            {
                return null;
            }

            sysUser.UserInfo = this.UserInfoRepository.GetModel(sysId);

            return sysUser;
        }

        public IEnumerable<SysUser> GetUserIncludeUserInfo(string table, string fields = "", string @where = "")
        {
            using (IDbConnection connection = this.Connection)
            {
                var p = new DynamicParameters();
                p.Add("@Table", table, DbType.String, ParameterDirection.Input, 1000);
                p.Add("@Fields", fields, DbType.String, ParameterDirection.Input, 2000);
                p.Add("@Where", where, DbType.String, ParameterDirection.Input, 1000);

                return connection.Query<SysUser, SysUserInfo, SysUser>(
                    Constant.ProcGetList,
                    (u, ui) =>
                        {
                            u.UserInfo = ui;
                            return u;
                        },
                    p,
                    splitOn: "SysId",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override int Update(SysUser item)
        {
            return this.AddOrModifyTrans(item, item.UserInfo, this.Update, this.UserInfoRepository.Update);
        }

        #endregion

        #region Methods

        internal override dynamic Mapping(SysUser item)
        {
            return new { item.SysId, item.UserName, item.UserPwd, item.CreateTime, item.LastLogin, item.RecordStatus, };
        }

        #endregion
    }
}