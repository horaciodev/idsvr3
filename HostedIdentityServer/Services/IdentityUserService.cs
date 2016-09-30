using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace HostedIdentityServer.Services
{
    public class IdentityUserService: AspNetIdentityUserService<User, string>
    {
        public IdentityUserService(UserManager usrMgr): base(usrMgr) { }

        public override Task GetProfileDataAsync(ProfileDataRequestContext ctx)
        {
            return base.GetProfileDataAsync(ctx);
        }
        /*
        public async Task<IEnumerable<Claim>> GetNameClaims(string userId)
        {
            var nameClaims = new List<Claim>();
            
            var usr = await this.userManager.FindByIdAsync(userId);
            if (usr != null)
            {
                if(!String.IsNullOrEmpty(usr.FirstName))
                {
                    nameClaims.Add(new Claim(Constants.ClaimTypes.GivenName, usr.FirstName));
                }
                if (!String.IsNullOrEmpty(usr.LastName))
                {
                    nameClaims.Add(new Claim(Constants.ClaimTypes.FamilyName, usr.LastName));
                }
            }

            return nameClaims;
        }
        */

    }
}