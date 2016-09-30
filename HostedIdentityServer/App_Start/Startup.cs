using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Microsoft.Owin;
using System.Security.Cryptography.X509Certificates;
using System.Web;


using Owin;
using IdentityServer3.Core.Configuration;
//using IdentityServer3.EntityFramework.Entities;
using IdentityServer3.EntityFramework;
using IdentityServer3.Core.Models;
using IdentityManager.Configuration;
using Serilog;


[assembly: OwinStartup(typeof(HostedIdentityServer.Startup))]

namespace HostedIdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string connStr = ConfigurationManager.ConnectionStrings["idSvrConnStr"].ConnectionString;
            string usrBaseConnString = ConfigurationManager.ConnectionStrings["idSvrUserBaseConnStr"].ConnectionString;

            var inMemoryManager = new InMemoryManager();

            var serviceOptions = new EntityFrameworkServiceOptions { ConnectionString = connStr };

            SetupClients(inMemoryManager.GetClients(), serviceOptions);
            SetupScopes(inMemoryManager.GetScopes(), serviceOptions);

            //cleanup tokens with a background thread every 10 minutes, could be configurable
            new TokenCleanup(serviceOptions, 10).Start();

            app.Map("/identity", id =>
            {
                id.UseIdentityServer( new IdentityServerOptions { SiteName = "ASPNET MVC Hosted Identity Server3",
                                                                  SigningCertificate = LoadCertFromStore(),
                                                                  RequireSsl = true,
                                                                  Factory = new IdentityServerServiceFactory().Configure(serviceOptions,usrBaseConnString)
                });
            });

            app.Map("/admin", adminApp =>
            {
                adminApp.UseIdentityManager(new IdentityManager.Configuration.IdentityManagerOptions()
                {
                    Factory = new IdentityManagerServiceFactory().Configure(usrBaseConnString)
                });
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .CreateLogger();
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
            X509Store certStore = null; 
            try
            {
                certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);

                var certThumbPrint = ConfigurationManager.AppSettings["signing-certificate-thumbprint"];

                certStore.Open(OpenFlags.ReadOnly);

                var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certThumbPrint, false);



                if (0 == certCollection.Count)
                {
                    throw new Exception("No certificate was found containing specified thumbprint");
                }
                string certPwd = (string)ConfigurationManager.AppSettings["signing-certificate.password"];

                byte[] certBytes = certCollection[0].Export(X509ContentType.Pkcs12, certPwd);

                x509Cert = new X509Certificate2(certBytes, certPwd
                                                , X509KeyStorageFlags.MachineKeySet);

                
            }
            //catch
            //{
            //    Log.Information("Failed to load certificate from store");
            //}
            finally
            {
                
                certStore.Close();
            }

            return x509Cert;
        }

        protected void SetupClients(IEnumerable<Client> clients,
                                EntityFrameworkServiceOptions options)
        {
            using (var context = new ClientConfigurationDbContext(options.ConnectionString,
                                                                  options.Schema))
            {
                if (context.Clients.Any())
                {
                    return;
                }
                foreach(var c in clients)
                {
                    context.Clients.Add(c.ToEntity());
                }

                context.SaveChanges();
            }
        }

        protected void SetupScopes(IEnumerable<Scope> scopes,
                                    EntityFrameworkServiceOptions options)
        {
            using (var context = new ScopeConfigurationDbContext(options.ConnectionString,
                                                                options.Schema))
            {
                if(context.Scopes.Any())
                {
                    return;
                }
                foreach(var s in scopes)
                {
                    context.Scopes.Add(s.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}