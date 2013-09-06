using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysUserRepository : Repository<SysUser>,ISysUserRepository
    {
        #region 存储过程名

        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysUserExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysUserGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysUserGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysUserDelete;
            }
        }

        #endregion

        public bool Exists(string name, string pwd)
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
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
                else
                {
                    return false;
                }
            }
        }
    }
}
