using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Contexts;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller {
        public AuthController(IConfiguration configuration, FoodOrderContext dbContext) {
            Configuration = configuration;
            DbContext = dbContext;
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JWT:Secret"]));
            // UserManager<>
        }

        private SymmetricSecurityKey IssuerSigningKey { get; }
        private IConfiguration Configuration { get; }
        private FoodOrderContext DbContext { get; }

        [HttpPost]
        [Route("google-token-signin")]
        public async Task<IActionResult> TokenSignin([FromBody] string idToken) {
            try {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                var userName = payload.Name;
                var userEmail = payload.Email;
                
                var dbUser = await DbContext.Users.FirstOrDefaultAsync(u => string.Compare(u.Email, userEmail, StringComparison.CurrentCultureIgnoreCase) == 0);
                
                if (dbUser == null) {
                    dbUser = new User { FirstName = payload.GivenName, LastName = payload.FamilyName, Email = userEmail };
                    await DbContext.Users.AddAsync(dbUser);
                    DbContext.SaveChanges();
                }

                var token = GenerateJwtSecurityToken(new[] {
                    new Claim("id", dbUser.Id.ToString()),
                    new Claim("fullName", userName),
                    new Claim("isAdmin", dbUser.IsAdmin.ToString().ToLower()),
                    new Claim("pictureUrl", payload.Picture)
                });
                
                return Ok(new {
                    token = token.TokenString,
                    expiration = token.Expiration,
                    success = true
                });
            } catch (InvalidJwtException e) {
                return BadRequest(e.Message);
            }
        }

        private AppSecurityToken GenerateJwtSecurityToken(Claim[] claims) {
            var signingCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

            return new AppSecurityToken {
                Token = new JwtSecurityToken(
                    issuer: Configuration["Authentication:JWT:Issuer"],
                    audience: Configuration["Authentication:JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(10),
                    claims: claims,
                    signingCredentials: signingCredentials
                )
            };
        }
    }

    internal class AppSecurityToken {
        public JwtSecurityToken Token { private get; set; }
        public string TokenString => new JwtSecurityTokenHandler().WriteToken(Token);
        public DateTime Expiration => Token.ValidTo;
    }
}