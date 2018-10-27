using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Infrastructure {
    public static class ClaimsPrincipalExtension {
        public static bool IsAdmin(this ClaimsPrincipal claims)
        {
            return bool.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "isAdmin")?.Value, out bool isAdmin) && isAdmin;
        }
    }
}
