// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
              
            };
        }
    

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("enquiries.read"),
                new ApiScope("enquiries.write"),
            };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("enquiries")
            {
               Scopes = new List<string>{"enquiries.read","enquiries.read"},
            }
        };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "crm_support_ui",
                    ClientName = "crm_support_ui",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 120,
                  
               
         
                    RedirectUris =           { "https://support-ui.webmelvin.me/account/callback" },
                    PostLogoutRedirectUris = { "https://support-ui.webmelvin.me" },
                    AllowedCorsOrigins =     { "https://support-ui.webmelvin.me" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        // "custom.profile",
                        "second_name",
                        "first_name",
                        "sacco_id",
                        "api1",
                        "api2",
                        "enquiries.read",
                        "enquiries.write",
                        "enquiries"
                        
                    },
                },
      
         
                new Client
                {
                    ClientId = "agent_ui",
                    ClientName = "Agent UI",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 120,
                  

               
         
                    RedirectUris =           { "https://agents-portal.webmelvin.me/account/callback" },
                    PostLogoutRedirectUris = { "https://agents-portal.webmelvin.me" },
                    AllowedCorsOrigins =     { "https://agents-portal.webmelvin.me" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        // "custom.profile",
                        "second_name",
                        "first_name",
                        "sacco_id",
                        "api1",
                        "api2",
                        "enquiries.read",
                        "enquiries.write",
                        "enquiries"
                        
                    },
                },
                
       
            };
    }
}