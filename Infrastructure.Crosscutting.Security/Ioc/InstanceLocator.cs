/*
 *名称：InstanceLocator
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 11:58:24
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Ioc
{
    public class InstanceLocator
    {
        private static IInstanceLocator currentLocator;

        private InstanceLocator()
        { }

        public static IInstanceLocator Current
        {
            get
            {
                return currentLocator;
            }
        }

        public static void SetLocator(IInstanceLocator locator)
        {
            currentLocator = locator;
        }

        public T GetInstance<T>() where T : class
        {
            return Current.GetInstance<T>();
        }

        public T GetInstance<T>(string name) where T : class
        {
            return Current.GetInstance<T>(name);
        }

        public object GetInstance(Type instanceType)
        {
            return Current.GetInstance(instanceType);
        }

        public void RegisterType<T>()
        {
            currentLocator.RegisterType<T>();
        }

        public void RegisterType(Type type)
        {
            currentLocator.RegisterType(type);
        }

        public void RegisterInstance<T>(T t)
        {
            currentLocator.RegisterInstance<T>(t);
        }

        public bool IsTypeRegistered<T>()
        {
            return currentLocator.IsTypeRegistered<T>();
        }

        public bool IsTypeRegistered(Type type)
        {
            return currentLocator.IsTypeRegistered(type);
        }
         
    }
}