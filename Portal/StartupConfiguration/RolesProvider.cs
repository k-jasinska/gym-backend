using Newtonsoft.Json;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.StartupConfiguration
{

    /// Provider 
    internal static class RolesProvider
    {
        public static IEnumerable<string> GetRoles(ClaimsPrincipal user, string clientId, bool allowClientRoles, bool allowRealmRoles)
        {

            IEnumerable<string> clientRoles = new List<string>();
            IEnumerable<string> realmRoles = new List<string>();
            if (allowClientRoles)
            {
                clientRoles = GetClientRoles(user, clientId);
            }
            if (allowRealmRoles)
            {
                realmRoles = GetRealmRoles(user);
            }

            return clientRoles.Union(realmRoles);
        }

        private static IEnumerable<string> GetClientRoles(ClaimsPrincipal user, string clientId)
        {
            const string CLIENT_ROLES_CLAIM = "resource_access";
            if (!user.HasClaim(c => c.Type == CLIENT_ROLES_CLAIM))
            {
                return new List<string>();
            }
            string clientRolesJson = user.FindFirst(c => c.Type == CLIENT_ROLES_CLAIM).Value;
            var allClientsRoles = JsonConvert.DeserializeObject<Dictionary<string, ClientRoles>>(clientRolesJson);
            if (allClientsRoles == null)
            {
                return new List<string>();
            }
            if (!allClientsRoles.ContainsKey(clientId))
            {
                return new List<string>();
            }
            IEnumerable<string> actualRoles = allClientsRoles[clientId].Roles;
            if (actualRoles == null)
            {
                return new List<string>();
            }
            return actualRoles;
        }

        private static IEnumerable<string> GetRealmRoles(ClaimsPrincipal user)
        {
            const string REALM_ROLES_CLAIM = "realm_access";
            if (!user.HasClaim(c => c.Type == REALM_ROLES_CLAIM))
            {
                return new List<string>();
            }
            string realmRolesJson = user.FindFirst(c => c.Type == REALM_ROLES_CLAIM).Value;
            List<string> actualRoles = JsonConvert.DeserializeObject<RealmRoles>(realmRolesJson).Roles;
            if (actualRoles == null)
            {
                return new List<string>();
            }
            return actualRoles;
        }
    }


}
