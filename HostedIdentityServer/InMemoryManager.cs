using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core;
using IdentityServer3.Core.Models;

using HostedIdentityServer.Common;

namespace HostedIdentityServer
{
    public class InMemoryManager
    {
        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
               StandardScopes.OpenId,
               StandardScopes.Profile,
               StandardScopes.OfflineAccess, //this one is to be able to request a refresh token
               new Scope                    //this is custom Scope
               {
                   Name = "MainWebsiteAccess",
                   DisplayName = "Main Website Access",
                   Description = "Allow access to main website",
                   Type = ScopeType.Resource
               }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "main-website",
                    ClientName = "Main Website",
                    /*
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mainwebsites3cret".Sha256())
                    },*/
                    Flow = Flows.Hybrid,
                    RedirectUris = new List<string>
                    {
                        Common.Constants.MAIN_WEBSITE_REDIRECT_URI
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServer3.Core.Constants.StandardScopes.OpenId,
                        IdentityServer3.Core.Constants.StandardScopes.Profile
                    },
                    Enabled = true,
                    RequireConsent = false
                }
            };
        }
    }
}