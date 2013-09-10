/*
 *名称：SysConfigRepository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 15:30:46
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysConfigRepository:Repository<SysConfig>
    {
        #region 属性

        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysConfigExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysConfigAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysConfigGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysConfigGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysConfigUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysConfig; }
        }

        #endregion

    }
    
}