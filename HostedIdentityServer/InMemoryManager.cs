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
              
               //StandardScopes.OfflineAccess, //this one is to be able to request a refresh token
               new Scope                    //this is custom Scope
               {
                   Name = HostedIdentityServer.Common.Constants.MAIN_WEBSITE_ACCESS_RESOURCE_SCOPE,
                   DisplayName = "Main Website Access Resource Scope",
                   Description = "Allow access to main website",
                   Type = ScopeType.Resource,
                   Emphasize = true
               },
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Roles,
                StandardScopes.OfflineAccess
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "mainwebsite",
                    ClientName = "Main Website",
                    Flow = Flows.Hybrid,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = new List<string>
                    {
                        Common.Constants.MAIN_WEBSITE_REDIRECT_URI
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        Common.Constants.MAIN_WEBSITE_POST_LOGOUT_URI
                    },
                    
                    AllowedScopes = new List<string>
                    {
                        IdentityServer3.Core.Constants.StandardScopes.OpenId,
                        IdentityServer3.Core.Constants.StandardScopes.Profile,
                        IdentityServer3.Core.Constants.StandardScopes.OfflineAccess,
                        IdentityServer3.Core.Constants.StandardScopes.Email,
                        IdentityServer3.Core.Constants.StandardScopes.Roles,
                        HostedIdentityServer.Common.Constants.MAIN_WEBSITE_ACCESS_RESOURCE_SCOPE
                    },
                    
                    Enabled = true,
                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowRememberConsent = true,
                }
            };
        }
    }
}