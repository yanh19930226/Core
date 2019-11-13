using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MvcCookieSample.Models;
using MvcCookieSample.ViewModels;
using IdentityServer4.Test;
using IdentityServer4.Services;

namespace MvcCookieSample.Controllers
{
    public class AccountController : Controller
    {
        //使用真实的IdentityServer
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IIdentityServerInteractionService identityServerInteractionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
        }
        ////使用IdentityServer的测试仓储层
        //private readonly TestUserStore _users;
        //public AccountController(TestUserStore users)
        //{
        //    _users = users;
        //}
        public IActionResult Login(string returnUrl = null)
        {
            ViewData[nameof(returnUrl)] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                //var user = _users.FindByUsername(loginViewModel.UserName);
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.Email), "user not exist");
                }
                else
                {
                    if (await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
                    {
                        AuthenticationProperties props = null;
                        //是否记住
                        if (loginViewModel.RememberMe)
                        {
                            props = new AuthenticationProperties()
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                            };
                        }
                        await _signInManager.SignInAsync(user, props);
                        if(_identityServerInteractionService.IsValidReturnUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("~/");
                        //await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, user.SubjectId, user.Username, props);
                        //return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError(nameof(loginViewModel.Email), "wrong password");
                }
            }
            return View();
        }
        public IActionResult Register(string returnUrl = null)
        {
            ViewData[nameof(returnUrl)] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new ApplicationUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    NormalizedUserName = registerViewModel.Email
                };
                var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                if (identityResult.Succeeded)
                {
                    //注册成功直接登入
                    await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });
                    //HttpContext.SignInAsync() 上面的语句等同于这一句话，是一个封装
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(identityResult);
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            //await HttpContext.SignOutAsync();
            //return RedirectToAction("Index", "Home");

        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError(string.Empty, identityError.Description);
            }
        }
    }
}