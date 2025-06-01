using GeoSolution.Models.MQ;
using GeoSolution.Services.Messaging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace GeoSolution.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMessagePublisher _publisher;

        public AccountController(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            return Challenge(
                new AuthenticationProperties { RedirectUri = returnUrl },
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }

        public async Task<IActionResult> Logout()
        {
           
            return SignOut(
                new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }
        public IActionResult Register(string returnUrl = "/") 
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, OpenIdConnectDefaults.AuthenticationScheme);
        }
        //debug methods delete in prod
        public async Task<IActionResult> TestEmail([FromServices] IEmailService emailService)
        {
            await emailService.SendAsync("filippoveric336@gmail.com", "Тестовая тема", "Привет! Это тестовое письмо.");
            return Ok("Письмо отправлено!");
        }

        [Authorize]
        public IActionResult Claims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Json(claims);
        }
        [Authorize]
        public async Task<IActionResult> ShowTokens()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            return Json(new
            {
                IdToken = idToken,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        [Authorize]
        [Route("account/roles")]
        public IActionResult Roles()
        {
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            return Json(roles);
        }
        public async Task<IActionResult> DebugRoles()
        {
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToArray();

            var isAdmin = User.IsInRole("Admin");

            var all = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToArray();

            return Json(new
            {
                RolesClaimValues = roles,
                IsInRoleAdmin = isAdmin,
                AllClaims = all
            });
        }
        
    }
}
