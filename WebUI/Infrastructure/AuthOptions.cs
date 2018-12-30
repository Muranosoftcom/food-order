using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebUI.Infrastructure {
    public class AuthOptions {
        public AuthOptions(IConfiguration configuration) {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public string Issuer => Configuration["Authentication:JWT:Issuer"];
        public string Audience => Configuration["Authentication:JWT:Audience"];
        public int ExpireDays => Convert.ToInt32(Configuration["Authentication:JWT:ExpireDays"]);
        
        public SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JWT:Secret"]));
        }
    }
}