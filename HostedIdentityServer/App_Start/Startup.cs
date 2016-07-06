using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;


[assembly: OwinStartup(typeof(HostedIdentityServer.Startup))]

namespace HostedIdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string connStr = ConfigurationManager.ConnectionStrings["idSvrConnStr"].ConnectionString;
            string usrBaseConnString = ConfigurationManager.ConnectionStrings["idSvrUserBaseConnStr"].ConnectionString;

            var inMemoryManager = new InMemoryManager(); //to be used later

            app.Map("/identity", id =>
            {
                id.UseIdentityServer( new IdentityServerOptions { SiteName = "ASPNET MVC Hosted Identity Server3",
                                                                  SigningCertificate = LoadCertFromStore(),
                                                                  RequireSsl = true,
                                                                  Factory = new IdentityServerServiceFactory().Configure(connStr,usrBaseConnString)
                });
            });
        }

        //private X509Certificate2 LoadCertificate()
        //{
            //test certificate sourced from https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/Certificates

            /*
            return new X509Certificate2(
                string.Format(@"{0}\bin\{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["signing-certificate.name"]),
                (string)ConfigurationManager.AppSettings["signing-certificate.password"]
                );
             */   

          
        //}

        private X509Certificate2 LoadCertFromStore()
        {
            X509Certificate2 x509Cert = null;
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            var certThumbPrint = ConfigurationManager.AppSettings["signing-certificate-thumbprint"];

            certStore.Open(OpenFlags.ReadOnly);

            var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certThumbPrint, true);

            certStore.Close();

            if(0 == certCollection.Count)
            {
                throw new Exception("No certificate was found containing specified thumbprint");
            }

            x509Cert = new X509Certificate2(certCollection[0].GetRawCertData(),
                                            (string)ConfigurationManager.AppSettings["signing-certificate.password"]);

            return x509Cert;
        }
    }
}