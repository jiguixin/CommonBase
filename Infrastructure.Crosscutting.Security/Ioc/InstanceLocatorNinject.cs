/*
 *名称：InstanceLocatorNinject
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 11:58:52
 *修改时间：
 *备注：
 */

using System;
using Ninject;

namespace Infrastructure.Crosscutting.Security.Ioc
{ 
public class InstanceLocatorNinject : IInstanceLocator
    {
        private readonly IKernel kernel;

        public InstanceLocatorNinject(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public T GetInstance<T>() where T : class
        {
            return kernel.Get<T>();
        }

        public T GetInstance<T>(string name) where T : class
        {
            return kernel.Get<T>(name);
        }

        public object GetInstance(Type instanceType)
        {
            return kernel.Get(instanceType);
        }

        public bool IsTypeRegistered<T>()
        {
            throw new NotImplementedException();
        }

        public bool IsTypeRegistered(Type type)
        {
            throw new NotImplementedException();
        }

        public void RegisterType<T>()
        {
            kernel.Bind<T>();
        }

        public void RegisterType(Type type)
        {
            kernel.Bind(type);
        }

        public void RegisterInstance<T>(T t)
        {
            if (kernel.Get<T>() == null)
            {
                kernel.Bind<T>().ToConstant(t);
            }
            else
            {
                kernel.Rebind<T>().ToConstant(t);
            }
        }
    }
}