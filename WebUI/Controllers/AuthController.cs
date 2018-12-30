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

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller {
        private readonly AuthOptions _authOptions;
        private readonly UserManager _userManager;

        public AuthController(AuthOptions authOptions, UserManager userManager) {
            _authOptions = authOptions;
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
                        FirstName = payload.GivenName, 
                        LastName = payload.FamilyName, 
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
                new Claim("fullName", dbUser.FullName),
                new Claim("isAdmin", dbUser.IsAdmin.ToString().ToLower()),
                new Claim("pictureUrl", pictureUrl)
            };
            
            return claims;
        }

        private AppSecurityToken GenerateJwtSecurityToken(IEnumerable<Claim> claims) {
            var signingCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_authOptions.ExpireDays);
            var notBefore = DateTime.UtcNow;
            
            return new AppSecurityToken {
                Token = new JwtSecurityToken(_authOptions.Issuer, _authOptions.Audience, claims, notBefore, expires, signingCredentials)
            };
        }
    }

    internal class AppSecurityToken {
        public JwtSecurityToken Token { private get; set; }
        public string TokenString => new JwtSecurityTokenHandler().WriteToken(Token);
        public DateTime Expiration => Token.ValidTo;
    }
}