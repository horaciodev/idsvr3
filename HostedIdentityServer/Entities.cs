using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HostedIdentityServer
{
    public class Context: IdentityDbContext<IdentityUser,
                                            IdentityRole, 
                                            string, 
                                            IdentityUserLogin, 
                                            IdentityUserRole, 
                                            IdentityUserClaim>
    {
        public Context(string connString) : base(connString) { }
    }

    public class UserStore: UserStore<IdentityUser,
                                        IdentityRole, 
                                        string, 
                                        IdentityUserLogin, 
                                        IdentityUserRole, 
                                        IdentityUserClaim>
    {
        public UserStore(Context ctx) : base(ctx) { }
    }

    public class RoleStore: RoleStore<IdentityRole>
    {
        public RoleStore(Context ctx) : base(ctx) { }
    }

    public class UserManager : UserManager<IdentityUser, string>
    {
        public UserManager(UserStore userStore) : base(userStore) { }
    }

    public class RoleManager: RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore roleStore) : base(roleStore) { }
    }
}