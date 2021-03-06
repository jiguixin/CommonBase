﻿using System.Data;

using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System;
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Core;

    public class SysUserRepository : Repository<SysUser>
    { 
        public SysUserRepository(ISql sql)
            : base(sql)
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
            if (string.IsNullOrEmpty(item.SysId)) item.SysId = item.UserInfo.SysId = Util.NewId();

            return AddOrModifyTrans(item, item.UserInfo, Add, UserInfoRepository.Add);
        }

        public override int Update(SysUser item)
        {
             item.UserInfo.SysId = item.SysId;
             
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="param">如果使用了 param 那么
        /// where格式为：[列]=Constant.SqlReplaceParameterPrefix[列]</param> 
        /// <returns></returns>
        public IEnumerable<SysUser> GetUserIncludeUserInfo(string table, string fields = "", string @where = "", object param = null)
        {
            using (var connection = Connection)
            {
                var sqlText = CreateSelectSql(table,fields,where);
                sqlText = Util.ReplaceParameterPrefix(param, sqlText, sql.ParameterPrefix);

                return connection.Query<SysUser, SysUserInfo, SysUser>(
                    sqlText,
                    (u, ui) =>
                        {
                            u.UserInfo = ui;
                            return u;
                        },
                        splitOn:"SysId",param:param,
                    commandType: CommandType.Text);
            }
        }
    }
}
