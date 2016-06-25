using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HostedIdentityServer.Services
{
    public class IdentityUserService: AspNetIdentityUserService<IdentityUser, string>
    {
        public IdentityUserService(UserManager usrMgr): base(usrMgr) { }
    }
}