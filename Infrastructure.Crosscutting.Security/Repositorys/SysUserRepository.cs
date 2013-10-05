using System.Data;

using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;

    public class SysUserRepository : Repository<SysUser>
    {

        public SysUserRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysUserSql"))
        {   
        }

        #region 属性
          
        public override string TableName
        {
            get { return Constant.TableSysUser; }
        }

        #endregion

        public SysUserInfoRepository UserInfoRepository {
            get { return RepositoryFactory.UserInfoRepository; }
        }

        public SysPrivilegeRepository PrivilegeRepository
        {
            get { return RepositoryFactory.PrivilegeRepository; }
        }

        public SysUserRoleRepository UserRoleRepository {
            get { return RepositoryFactory.UserRoleRepository; }
        }
          
        internal override dynamic Mapping(SysUser item)
        {
            return new
                {
                    SysId = item.SysId,
                    UserName = item.UserName,
                    UserPwd = item.UserPwd,
                    CreateTime = item.CreateTime,
                    LastLogin = item.LastLogin,
                    RecordStatus = item.RecordStatus, 
                };
        }
         
        public override int Add(SysUser item)
        {
            return AddOrModifyTrans(item, item.UserInfo, Add, UserInfoRepository.Add);
        }
         
        public override int Update(SysUser item)
        {
            return AddOrModifyTrans(item, item.UserInfo, Update, UserInfoRepository.Update);
        }

        /// <summary>
        /// 删除用户时，同时删除，权限表的用户数据，用户和角色表的数据，及用户表中的数据
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeMaster.User, Delete, UserInfoRepository.Delete, UserRoleRepository.DeleteByUserId, PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }

        public override SysUser GetModel(string sysId)
        {
            var sysUser = base.GetModel(sysId);

            if (sysUser == null)
                return null;

            sysUser.UserInfo = UserInfoRepository.GetModel(sysId);

            return sysUser;
        }

        public IEnumerable<SysUser> GetUserIncludeUserInfo(string table,string fields = "", string @where = "")
        {
            using (var connection = Connection)
            {
                return connection.Query<SysUser, SysUserInfo, SysUser>(
                    CreateSelectSql(table,fields,where),
                    (u, ui) =>
                        {
                            u.UserInfo = ui;
                            return u;
                        },
                        splitOn:"SysId",
                    commandType: CommandType.Text);
            }
        }
    }
}
