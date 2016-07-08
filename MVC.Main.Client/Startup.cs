using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;


using HostedIdentityServer.Common;


[assembly: OwinStartup(typeof(MVC.Main.Client.Startup))]

namespace MVC.Main.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "main-website",                
                Authority = Constants.PRIMARY_AUTHORIZATION_SERVER_URI,
                RedirectUri = Constants.MAIN_WEBSITE_REDIRECT_URI,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token",
                Scope = "openid profile"
                /*
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = async n=>
                    {
                        Microsoft.Owin.Helpers.TokenHelper
                    }
                }
                */
            });
        }
    }
}
