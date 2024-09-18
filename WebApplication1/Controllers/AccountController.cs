using Business.Abstracts;
using Business.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using RestaurantWepApp.Models;
using System.Security.Claims;
using System.Linq;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace RestaurantWepApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginResult = _authService.Login(new UserForLoginDto
            {
                Email = model.Email,
                Password = model.Password
            });

            if (loginResult.Success)
            {
                var user = loginResult.Data;

                var tokenResult = _authService.CreateAccessToken(user);
                if (tokenResult.Success)
                {
                    var accessToken = tokenResult.Data.Token;

                    Response.Cookies.Append("token", accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // HTTPS kullanıyorsanız
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(30) // Örneğin, 30 dakika geçerlilik süresi
                    });



                    // Token'ı bir HTTP-only cookie olarak sakla
                 
                    var roles = _userService.GetClaims(user);

                    if (roles.Any(role => role.Name == "Admin"))
                    {
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", tokenResult.Message);
            }
            else
            {
                ModelState.AddModelError("", loginResult.Message);
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            // JWT ile logout işlemi istemci tarafından yapılır, sunucu tarafında bir işlem gerekmez
            return RedirectToAction("Login");
        }
    }
}
