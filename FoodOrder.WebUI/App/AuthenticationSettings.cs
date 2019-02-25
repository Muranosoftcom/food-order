using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrder.WebUI.App {
    public class AuthenticationSettings {
        public AuthenticationSettings(IConfigurationSection authSettingsConfigurationSection) {
            GoogleClientId = authSettingsConfigurationSection["Google:ClientId"];
            GoogleClientSecret = authSettingsConfigurationSection["Google:ClientSecret"];
            JwtSecret = authSettingsConfigurationSection["Jwt:Secret"];
            JwtIssuer = authSettingsConfigurationSection["Jwt:Issuer"];
            JwtAudience = authSettingsConfigurationSection["Jwt:Audience"];
            JwtExpireDays = authSettingsConfigurationSection["Jwt:ExpireDays"];
        }
        
        public string GoogleClientId { get; }
        public string GoogleClientSecret { get; }
        public string JwtSecret { get; }
        public string JwtIssuer { get; }
        public string JwtAudience { get; }
        public string JwtExpireDays { get; }
        
        public int ExpireDays => Convert.ToInt32(JwtExpireDays);
        
        public SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
        }
    }

    
}