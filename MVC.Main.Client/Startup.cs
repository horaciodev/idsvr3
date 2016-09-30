using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;


using HostedIdentityServer.Common;
using System.Web.Helpers;
using System.IdentityModel.Tokens;


[assembly: OwinStartup(typeof(MVC.Main.Client.Startup))]

namespace MVC.Main.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mainwebsite",                
                Authority = Constants.PRIMARY_AUTHORIZATION_SERVER_URI,
                RedirectUri = Constants.MAIN_WEBSITE_REDIRECT_URI,
                PostLogoutRedirectUri = Constants.MAIN_WEBSITE_POST_LOGOUT_URI,
                ResponseType = "code id_token token",
                Scope = "openid profile email roles offline_access",
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                },
                ClientSecret = "secret",
                SignInAsAuthenticationType = "Cookies",

            });
        }
    }
}
