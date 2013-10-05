/*
 *名称：IInstanceLocator
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 11:58:08
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Ioc
{
    public interface IInstanceLocator
    {
        T GetInstance<T>() where T : class;
        T GetInstance<T>(string name) where T : class;
        object GetInstance(Type instanceType);
        bool IsTypeRegistered<T>();
        bool IsTypeRegistered(Type type);
        void RegisterType<T>();
        void RegisterType(Type type);
        void RegisterInstance<T>(T t); 
    }
}