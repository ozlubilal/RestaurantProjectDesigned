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



                   
                 
                    var roles = _userService.GetClaims(user);

                    if (roles.FirstOrDefault()?.Name=="Admin")
                    {
                        return RedirectToAction("Index", "Category");
                    }
                    if(roles.FirstOrDefault()?.Name=="Waiter")
                    {
                        return RedirectToAction("Index", "WaiterTable");
                    }
                    if(roles.FirstOrDefault()?.Name=="Chef")
                    {
                        return RedirectToAction("Index", "Chef");
                    }
                    if (roles.FirstOrDefault()?.Name == "Cashier")
                    {
                        return RedirectToAction("Index","Cashier");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register Post Request
        [HttpPost]
        public IActionResult Register(UserForRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerResult = _authService.Register(new UserForRegisterDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            },model.Password);

            if (registerResult.Success)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", registerResult.Message);
            return View(model);
        }
    }
}

   