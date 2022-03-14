// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServerAspNetIdentity.EmailService;
using IdentityServerAspNetIdentity.Services;
using IdentityServerAspNetIdentity.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.ForgotPassword;
using TicketCRM.DataLayer.EmailTemplates.Views.Emails.RegisteredAccount;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using AgentDTO = IdentityServerAspNetIdentity.Configuration.AgentDTO;
using DepartmentDTO = IdentityServerAspNetIdentity.Configuration.DepartmentDTO;
using SaccoDTO = IdentityServerAspNetIdentity.Configuration.SaccoDTO;

namespace IdentityServerHost.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IAgentService _agentService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmailTemplateResolver<NewAccountViewModel> _emailTemplateResolverNewAccount;
        private readonly IEmailTemplateResolver<ForgotPasswordCustomModel> _emailTemplateResolverForgotPassword;
        private readonly IEmailIdentityServerService _emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IDepartmentService departmentService,
            IMapper mapper,
            IAgentService agentService,
            IOrganizationService organizationService,
            IEmailTemplateResolver<NewAccountViewModel> emailTemplateResolverNewAccount,
            IEmailTemplateResolver<ForgotPasswordCustomModel> emailTemplateResolverForgotPassword ,
            IEmailIdentityServerService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _departmentService = departmentService;
            _mapper = mapper;
            _agentService = agentService;
            _organizationService = organizationService;
            _emailTemplateResolverNewAccount = emailTemplateResolverNewAccount;
            _emailTemplateResolverForgotPassword = emailTemplateResolverForgotPassword;
            _emailService = emailService;
        }
        
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();
            
            var allSaccos = _organizationService.GetListOfSaccos();

            vm.Saccos = new SelectList(_mapper.Map<List<SaccoDTO>>(allSaccos), "Id", "SaccoName");
            
            return View(vm);
        }
        
        [HttpGet]
        public IActionResult ForgotPassword(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
                
                return View(forgotPassword);
            
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var callback = Url.Action(nameof(ResetPassword), "Account", 
                new {
                    token,
                    email = user.Email ,
                    returnUrl=forgotPassword.ReturnUrl
                }, Request.Scheme);
            
            var mailviewDto = new MailViewModelDTO();
            mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
            mailviewDto.EmailTemplatePath = "/Views/Emails/ForgotPassword/ForgotPassword.cshtml";
            mailviewDto.LinkedResourceContentId = "header";

            var forgotPasswordCustomModel = new ForgotPasswordCustomModel(callback);

            var builder =await _emailTemplateResolverForgotPassword.BuildEmailBodyAsync(mailviewDto, forgotPasswordCustomModel);

            await _emailService.SendEmailAsync(builder.ToMessageBody(), user.Email,"Forgot Password");
           
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email,string returnUrl)
        {
            var model = new ResetPassword{ Token = token, Email = email ,ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if(!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation),"Account",new
            {
                returnUrl= resetPasswordModel.ReturnUrl
            });
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        public IActionResult RegisterAgent()
        {
            var vm =new RegisterAgentViewModel();
            var allDepartments = _departmentService.GetListOfDepartments();

            vm.Departments = new SelectList(_mapper.Map<List<DepartmentDTO>>(allDepartments),"Id","DepartmentVal");

            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var allSaccos = _organizationService.GetListOfSaccos();

                model.Saccos = new SelectList(_mapper.Map<List<SaccoDTO>>(allSaccos), "Id", "SaccoName");
            }
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SaccoId = model.Sacco,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new
                    {
                        token,
                        user.Email,
                        returnUrl
                        
                     
                      
                    },Request.Scheme);
                    
                    var mailviewDto = new MailViewModelDTO();
                    mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
                    mailviewDto.EmailTemplatePath = "/Views/Emails/RegisteredAccount/NewAccountEmail.cshtml";
                    mailviewDto.LinkedResourceContentId = "header";

                    var newAccountViewModel = new NewAccountViewModel(confirmationLink);

                    var builder =await _emailTemplateResolverNewAccount.BuildEmailBodyAsync(mailviewDto, newAccountViewModel);

                    await _emailService.SendEmailAsync(builder.ToMessageBody(), user.Email,"Account Registration");
                    
                    return RedirectToAction("SuccessfulRegistration");
                }
            
              
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
             

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email,string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user==null) return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);


            ViewBag.returnUrl= returnUrl;
            
            
            return View(result.Succeeded? nameof(ConfirmEmail):"Error");
        }

   
        
        [HttpGet]
        public IActionResult SuccessfulRegistration(string token, string email)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAgent(RegisterAgentViewModel model,string returnUrl)
        {
     
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SaccoId =  Guid.Parse("3d18bfa3-0991-4c13-a866-86635d7863be"),
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                };
           
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new
                    {
                        token,
                        user.Email,
                        returnUrl
                        
                     
                      
                    },Request.Scheme);
                    
                    var mailviewDto = new MailViewModelDTO();
                    mailviewDto.LinkedResourceContentPath = "Assets/group4.png";
                    mailviewDto.EmailTemplatePath = "/Views/Emails/RegisteredAccount/NewAccountEmail.cshtml";
                    mailviewDto.LinkedResourceContentId = "header";

                    var newAccountViewModel = new NewAccountViewModel(confirmationLink);

                    var builder =await _emailTemplateResolverNewAccount.BuildEmailBodyAsync(mailviewDto, newAccountViewModel);

                    await _emailService.SendEmailAsync(builder.ToMessageBody(), user.Email,"Account Registration");
                    
                    var agentDto = new AgentDTO()
                    {
                        Username = model.Email,
                        FirstName = model.Email,
                        SecondName = model.Email,
                        DepartmentId = model.Department,
                        UserId = Guid.Parse(user.Id)
                    
                    };
                    await _agentService.AddNewAgent(agentDto);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("SuccessfulRegistration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }


        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            vm.ClientId = context.Client.ClientId;
            


            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (await _userManager.IsEmailConfirmedAsync(user)==false)
                    {
                        ModelState.AddModelError(string.Empty,AccountOptions.NotConfirmedEmail);
                    }
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));
                
                    if (context != null)
                    {
                        if (context.IsNativeClient())
                        {
                            // The client is native, so this change in how to
                            // return the response is for better UX for the end user.
                            return this.LoadingPage("Redirect", model.ReturnUrl);
                        }
                      

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(model.ReturnUrl);
                    }

                 
                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId:context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        
        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}