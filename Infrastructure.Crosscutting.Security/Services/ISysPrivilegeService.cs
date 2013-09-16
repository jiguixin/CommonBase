/*
 *名称：ISysPrivilegeService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-16 05:22:19
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    public interface ISysPrivilegeService
    {
        void InitDataByRole();
        void InitDataByUser();

    }
}