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
            // var customProfile = new IdentityResource(
            //     name: "custom.profile",
            //     displayName: "Custom profile",
            //     userClaims: new[] { "sacco_id","first_name","second_name","isCustomer" });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                // customProfile
            };
        }
        // public static IEnumerable<IdentityResource> IdentityResources =>
        //     
        //            new IdentityResource[]
        //            {
        //         new IdentityResources.OpenId(),
        //         new IdentityResources.Profile(),
        //         new IdentityResource(
        //             name: "other_info",
        //             userClaims: new[] { "sacco_id","first_name","second_name","isCustomer" },
        //             displayName: "Your user identifier")
        //         
        //            };

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
                    ClientId = "client_admin_dashboard",
                    ClientName = "Client Admin Dashboard",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 120,
                    // AllowOfflineAccess = true,
                    // AccessTokenLifetime = 90, // 1.5 minutes
                    // AbsoluteRefreshTokenLifetime = 0,
                    // RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    // RefreshTokenExpiration = TokenExpiration.Sliding,
                    // UpdateAccessTokenClaimsOnRefresh = true,
                    // RequireConsent = false,

               
         
                    RedirectUris =           { "http://localhost:7894/account/callback" },
                    PostLogoutRedirectUris = { "http://localhost:7894" },
                    AllowedCorsOrigins =     { "http://localhost:7894" },

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
                    ClientId = "js",
                    ClientName = "Centrino Help Desk Support",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 120,
                    // AllowOfflineAccess = true,
                    // AccessTokenLifetime = 90, // 1.5 minutes
                    // AbsoluteRefreshTokenLifetime = 0,
                    // RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    // RefreshTokenExpiration = TokenExpiration.Sliding,
                    // UpdateAccessTokenClaimsOnRefresh = true,
                    // RequireConsent = false,

               
         
                    RedirectUris =           { "http://localhost:7893/account/callback" },
                    PostLogoutRedirectUris = { "http://localhost:7893" },
                    AllowedCorsOrigins =     { "http://localhost:7893" },

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
                ClientId = "jsagent",
                ClientName = "VueJs JavaScript Agent",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = true,
                AccessTokenLifetime = 120,
            
                // AllowOfflineAccess = true,
                // AccessTokenLifetime = 90, // 1.5 minutes
                // AbsoluteRefreshTokenLifetime = 0,
                // RefreshTokenUsage = TokenUsage.OneTimeOnly,
                // RefreshTokenExpiration = TokenExpiration.Sliding,
                // UpdateAccessTokenClaimsOnRefresh = true,
                // RequireConsent = false,

               
         
                RedirectUris =   { "http://localhost:7892/account/callback" },
                PostLogoutRedirectUris = { "http://localhost:7892" },
                AllowedCorsOrigins =  { "http://localhost:7892" },

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
                }
            };
    }
}