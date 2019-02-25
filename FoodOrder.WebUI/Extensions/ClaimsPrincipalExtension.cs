using System;
using System.Linq;
using System.Security.Claims;

namespace FoodOrder.WebUI.Extensions {
    public static class ClaimsPrincipalExtension {
        public static bool IsAdmin(this ClaimsPrincipal claims) {
            return bool.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "isAdmin")?.Value, out bool isAdmin) &&
                   isAdmin;
        }

        public static Guid GetUserId(this ClaimsPrincipal claims) {
            return Guid.Parse(claims.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            
            /*return int.TryParse(claims.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out Guid id)
                ? id
                : (int?) null;*/
        }

        public static bool IsAuthenticated(this ClaimsPrincipal claims) =>
            !(!claims?.Identity.IsAuthenticated ?? false);
    }
}