/*
 *名称：Logger
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-11-18 09:32:14
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Common
{
    using log4net;
    using log4net.Config;

    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));
        private static bool _configured;
        private static volatile object _lock = new object();
        public static ILog Log
        {
            get
            {
                if (!_configured)
                    lock (_lock)
                    {
                        if (!_configured)
                        {
                            InitLogger();
                            _configured = true;
                        }
                    }
                return log;
            }
        }

        /// <summary>
        /// 在程序首次启动时执行该方法，用WEB项目，在 Application_Start()中进行调用
        /// </summary>
        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}