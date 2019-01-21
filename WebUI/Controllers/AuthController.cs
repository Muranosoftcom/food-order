using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebUI.Infrastructure;
using WebUI.Models;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly UserManager _userManager;

        public AuthController(AuthenticationSettings authenticationSettings, UserManager userManager) {
            _authenticationSettings = authenticationSettings;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("google-token-signin")]
        public async Task<IActionResult> TokenSignin([FromBody] string idToken) {
            try {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                var userEmail = payload.Email;

                if (!await _userManager.IsRegisteredAsync(userEmail)) {
                    await _userManager.RegisterNewAsync(new User {
                        UserName = payload.Name, 
                        Email = userEmail
                    });
                }
                
                var userClaims = await GetClaims(payload.Email, payload.Picture);
                var token = GenerateJwtSecurityToken(userClaims);

                return Ok(new {
                    token = token.TokenString,
                    expiration = token.Expiration,
                    success = true
                });
            }
            catch (InvalidJwtException e) {
                return BadRequest(e.Message);
            }
        }

        private async Task<IEnumerable<Claim>> GetClaims(string email, string pictureUrl = "") {
            var dbUser = await _userManager.FindUserByEmail(email);

            if (dbUser == null) {
                return null;
            }
            
            var claims = new [] {
                new Claim("id", dbUser.Id.ToString()),
                new Claim("fullName", dbUser.UserName),
                new Claim("isAdmin", dbUser.IsAdmin.ToString().ToLower()),
                new Claim("pictureUrl", pictureUrl)
            };
            
            return claims;
        }

        private AppSecurityToken GenerateJwtSecurityToken(IEnumerable<Claim> claims) {
            var signingCredentials = new SigningCredentials(_authenticationSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_authenticationSettings.ExpireDays);
            var notBefore = DateTime.UtcNow;
            
            return new AppSecurityToken {
                Token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtAudience, claims, notBefore, expires, signingCredentials)
            };
        }
    }

    internal class AppSecurityToken {
        public JwtSecurityToken Token { private get; set; }
        public string TokenString => new JwtSecurityTokenHandler().WriteToken(Token);
        public DateTime Expiration => Token.ValidTo;
    }
}