using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core;
using IdentityServer3.Core.Models;

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
                   Name = "sysop",
                   DisplayName = "SysOp Read-Only Admin"
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
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mainwebsites3cret".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId
                    },
                    Enabled = true
                }
            };
        }
    }
}