using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Portal.StartupConfiguration
{
    internal static class ClaimsPrincipalExtensions
    {
        public static void UpdateRoleClaimValue(this ClaimsPrincipal user,
            string clientId,
            bool allowClientRoles,
            bool allowRealmRoles)
        {
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;
            List<Claim> claims = identity.FindAll(x => x.Type == identity.RoleClaimType).ToList();
            for (int i = 0; i < claims.Count; i++)
            {
                identity.RemoveClaim(claims[i]);
            }
            IEnumerable<string> roles = RolesProvider.GetRoles(user, clientId, allowClientRoles, allowRealmRoles);
            foreach (string role in roles)
            {
                Claim roleClaim = new Claim(identity.RoleClaimType, role);
                identity.AddClaim(roleClaim);
            }
        }
    }

}
