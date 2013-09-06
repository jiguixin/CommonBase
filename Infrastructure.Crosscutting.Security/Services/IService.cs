/*
 *名称：IService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-03 05:30:49
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    public interface IService<T>
    {  
        int Add(T t);
         
        int Delete(string id);

        bool Exists(string id);

        IList<T> GetList();

        T GetModel(string id);

        int Update(T t);
         
    }
     
}