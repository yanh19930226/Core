using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using MvcCookieSample.Services;
using MvcCookieSample.ViewModels;

namespace MvcCookieSample.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ConsentServices _consentService;
       
        public ConsentController(ConsentServices consentService)
        {

            _consentService = consentService;
        }
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model = await _consentService.BuildConsentViewModel(returnUrl);
            if (model==null)
            {

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel model)
        {
            var result = await _consentService.ProcessConsent(model);
            if (result.isRedirect)
            {
                return Redirect(result.RedirectUrl);
            }
            if (!string.IsNullOrEmpty(result.ValidationError))
            {
                ModelState.AddModelError("", result.ValidationError);
            }
            return View(result.consentViewModel);
        }
    }
}