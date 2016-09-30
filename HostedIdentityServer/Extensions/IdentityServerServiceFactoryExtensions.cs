using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using HostedIdentityServer;
using HostedIdentityServer.Services;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer3.Core.Configuration
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory,
                                                                EntityFrameworkServiceOptions serviceOptions,
                                                                string userBaseConnStr)
        { 
            factory.RegisterOperationalServices(serviceOptions);
            factory.RegisterConfigurationServices(serviceOptions); //which one goes first?

            factory.Register(new Registration<Context>(resolver => new Context(userBaseConnStr)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.UserService = new Registration<IUserService, IdentityUserService>();

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

            return factory;
        }
    }
}