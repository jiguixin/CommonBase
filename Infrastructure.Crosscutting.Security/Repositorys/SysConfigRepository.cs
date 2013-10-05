/*
 *名称：SysConfigRepository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 15:30:46
 *修改时间：
 *备注：
 */

using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;

    public class SysConfigRepository:Repository<SysConfig>
    {
        #region 属性

        public SysConfigRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysConfigSql"))
        {
        }

        public override string TableName
        {
            get { return Constant.TableSysConfig; }
        }

        #endregion

    }
    
}