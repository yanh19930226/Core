using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MvcCookieSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCookieSample.Services
{
    public class ConsentServices
    {

        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public ConsentServices(IClientStore clientStore, IResourceStore resourceStore, IIdentityServerInteractionService identityServerInteractionService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }
       
        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, Client client, Resources resources, InputConsentViewModel model)
        {
            var rememberConsent = model?.RememberConsent??true;
            var selectedScops = model?.ScopesConsented ?? Enumerable.Empty<string>();
            var vm = new ConsentViewModel()
            {
                ClientName = client.ClientName,
                ClientLogoUrl = client.LogoUri,
                ClientUrl = client.ClientUri,
                RememberConsent = rememberConsent,
                IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i,selectedScops.Contains(i.Name)||model==null)),
                ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(x => CreateScopeViewModel(x,selectedScops.Contains(x.Name) || model == null))

            };
            return vm;
        }
        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource,bool check)
        {
            return new ScopeViewModel()
            {
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Descripton = identityResource.Description,
                Required = identityResource.Required,
                Checked = check||identityResource.Required,
                Emphasize = identityResource.Emphasize
            };
        }
        private ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel()
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Descripton = scope.Description,
                Required = scope.Required,
                Checked = check || scope.Required,
                Emphasize = scope.Emphasize
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="model">二次选中的时候发生作用</param>
        /// <returns></returns>
        public async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl,InputConsentViewModel model=null)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
                return null;
            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            var vm = CreateConsentViewModel(request, client, resources, model);
            vm.ReturnUrl = returnUrl;
            return vm;
        }
        public async Task<ProcessConsentResult> ProcessConsent(InputConsentViewModel model)
        {
            ConsentResponse response = null;
            var result = new ProcessConsentResult();
            if (model.Button == "no")
            {
                response = ConsentResponse.Denied;
            }
            else if (model.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    response = new ConsentResponse()
                    {
                        ScopesConsented = model.ScopesConsented,
                        RememberConsent = model.RememberConsent
                    };
                }
                result.ValidationError = "请至少选中一个权限";
            }
            if (response != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, response);
                result.RedirectUrl = model.ReturnUrl;
            }
            else
            {
                var consentViewModel = await BuildConsentViewModel(model.ReturnUrl,model);
                result.consentViewModel = consentViewModel;
            }
            return result;
        }
    }
}
