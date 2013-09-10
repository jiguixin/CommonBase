using System.Data;

using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysUserRepository : Repository<SysUser>, ISysUserRepository
    {
        public SysUserRepository() 
        {
            UserInfoRepository = new SysUserInfoRepository();
            PrivilegeRepository = new SysPrivilegeRepository();
            RoleRepository = new SysRoleRepository();
        }

        #region 存储过程名

        public override string ExistsProc
        {
            get { return Constant.ProcSysUserExists; }
        }

        public override string AddProc
        {
            get { return Constant.ProcSysUserAdd; }
        }

        public override string GetListProc
        {
            get { return Constant.ProcSysUserGetList; }
        }

        public override string GetModelProc
        {
            get { return Constant.ProcSysUserGetModel; }
        }

        public override string UpdateProc
        {
            get { return Constant.ProcSysUserUpdate; }
        }

        public override string DeleteProc
        {
            get { return Constant.ProcSysUserDelete; }
        }

        #endregion

        public SysUserInfoRepository UserInfoRepository { get; private set; }

        public SysPrivilegeRepository PrivilegeRepository { get; private set; }

        public SysRoleRepository RoleRepository { get; private set; }
         

        public bool Exists(string name, string pwd)
        {
            using (var connection = Connection)
            {
                var p = new DynamicParameters();
                p.Add("@UserName", name, DbType.String, ParameterDirection.Input, 50);
                p.Add("@UserPwd", pwd, DbType.String, ParameterDirection.Input, 150);
                p.Add("@TempID", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                connection.Execute(ExistsProc, p, commandType: CommandType.StoredProcedure);
                int rValue = p.Get<int>("@TempID");
                if (rValue == 1)
                {
                    return true;
                }
                return false;
            }
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
        /// todo:要增加删除用户角色表数据
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public override int Delete(string sysId)
        {
            return PrivilegeRepository.DeletePrivilegeTrans(sysId, (int)PrivilegeMaster.User, Delete, UserInfoRepository.Delete, PrivilegeRepository.DeleteSysPrivilegeByMaster);
        }


        public override SysUser GetModel(string sysId)
        {
            var sysUser = base.GetModel(sysId);

            if (sysUser == null)
                return null;

            sysUser.UserInfo = UserInfoRepository.GetModel(sysId);

            return sysUser;
        }

    }
}
