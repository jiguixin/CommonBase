using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Data;

    using Infrastructure.Data.Ado.Dapper;

    public class SysButtonRepository:Repository<SysButton>
    {
        #region 属性

        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysButtonExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysButtonAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysButtonGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysButtonGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysButtonUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysButton; }
        }

        #endregion

        public int DeleteByMenuId(string menuId, IDbTransaction trans)
        {
            var p = new { MenuId = menuId}; 

           return
              trans.Connection.Execute(Constant.ProcSysButtonDeleteByMenuId, p,trans,
                                       commandType: CommandType.StoredProcedure);
        }
    }
}
