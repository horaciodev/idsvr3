using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HostedIdentityServer.Services;
using HostedIdentityServer;

namespace IdentityManager.Configuration
{
    public static class IdentityManagerServiceExtensions
    {
        public static IdentityManagerServiceFactory Configure(this IdentityManagerServiceFactory factory,
                                                                string connString)
        {
            factory.Register(new Registration<Context>(resolver => new Context(connString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<RoleStore>());
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<RoleManager>());

            factory.IdentityManagerService = new IdentityManager.Configuration.Registration<IIdentityManagerService, IdentityManagerService>();

            return factory;
        }
    }
}