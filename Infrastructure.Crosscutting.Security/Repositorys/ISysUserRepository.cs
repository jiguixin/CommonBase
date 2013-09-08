using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public interface ISysUserRepository 
    {
        SysUserInfoRepository UserInfoRepository { get; }

        bool Exists(string name, string pwd);
          
    }
}
