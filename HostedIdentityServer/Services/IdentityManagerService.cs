using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityManager.AspNetIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HostedIdentityServer.Services
{
    public class IdentityManagerService: AspNetIdentityManagerService<User, string, Role, string>
    {
        public IdentityManagerService(UserManager usrMgr, RoleManager roleMgr)
            : base(usrMgr,roleMgr)
        {

        }
    }
}