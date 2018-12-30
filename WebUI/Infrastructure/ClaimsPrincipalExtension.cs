using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Infrastructure {
    public static class ClaimsPrincipalExtension {
        public static bool IsAdmin(this ClaimsPrincipal claims) {
            return bool.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "isAdmin")?.Value, out bool isAdmin) &&
                   isAdmin;
        }

        public static int? GetUserId(this ClaimsPrincipal claims) {
            return int.TryParse(claims.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out int id)
                ? id
                : (int?) null;
        }

        public static bool IsAuthenticated(this ClaimsPrincipal claims) =>
            !(!claims?.Identity.IsAuthenticated ?? false);
    }
}