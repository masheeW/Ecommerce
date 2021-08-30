﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.web.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(ILogger<AccountController> logger, SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = configuration;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Shop", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RemeberMe, false);
               
                var user = await _userManager.FindByEmailAsync(model.UserName);
           
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else if(await _userManager.IsInRoleAsync(user,"Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }

               
            }

            ModelState.AddModelError("", "Failed to login");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Shop", "App");
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Shop", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new StoreUser() { 
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName                
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    var response = await _userManager.AddToRoleAsync(user, "Customer");
                    if (response.Succeeded)
                    {
                        return RedirectToAction("Shop", "App");
                    }
                    else
                    {
                        foreach (var error in response.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return RedirectToAction("Login", "App");

        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if(user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if(result.Succeeded)
                    {
                        //create token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                           
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Audience"], claims, signingCredentials: creds, expires: DateTime.UtcNow.AddMinutes(60));

                        return Created("", new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration= token.ValidTo
                        });
                    }
                }
             
            }
            return BadRequest();
        }

    }
}
