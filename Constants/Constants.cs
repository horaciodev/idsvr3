﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostedIdentityServer.Common
{
    public static class Constants
    {
        public const string MAIN_WEBSITE_REDIRECT_URI = "https://mainwebsite.hydra.local:44305/Home/SecureIndex/";
        public const string MAIN_WEBSITE_POST_LOGOUT_URI = "https://mainwebsite.hydra.local:44305/";
        public const string PRIMARY_AUTHORIZATION_SERVER_URI = "https://vm.hydra.local/identity";
        public const string MAIN_WEBSITE_ACCESS_RESOURCE_SCOPE = "MainWebsiteAccessResourceScope";
    }
}
