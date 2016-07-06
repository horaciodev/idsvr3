using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using HostedIdentityServer;
using HostedIdentityServer.Services;

namespace IdentityServer3.Core.Configuration
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory, string connStr, string userBaseConnStr)
        {
            var serviceOptions = new EntityFrameworkServiceOptions { ConnectionString = connStr };
            factory.RegisterOperationalServices(serviceOptions);
            factory.RegisterConfigurationServices(serviceOptions);

            factory.Register(new Registration<Context>(resolver => new Context(userBaseConnStr)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.UserService = new Registration<IUserService, IdentityUserService>();

            return factory;
        }
    }
}