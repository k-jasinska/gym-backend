using Flurl.Http;
using Newtonsoft.Json;
using Portal.Application;
using Portal.Application.Models;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private const string portalClientId= "ac05ccf6-a81a-497e-be70-4347b7048cfa";
        private const string portalUrl = "http://localhost:8080/auth/admin/realms/gym/users/";
        public async Task CreatePerson(Person p)
        {
                var accessToken = await getToken();

                //create user in keycloak
                var resultContent = await portalUrl
                    .WithOAuthBearerToken(accessToken)
                    .PostJsonAsync(new { username = p.Login });

                //get user id
                var urlWithId = resultContent.Headers.Location.AbsoluteUri;
                Uri uri = new UriBuilder(urlWithId).Uri;
                string createdUserId = uri.Segments[6];

                p.PersonID = new Guid(createdUserId);

                await SetUserPassword(createdUserId, p.Password);
                await EnableUser(createdUserId);
                await SetRole(createdUserId, p.Role);               
        }

        public async void DeleteClients(Guid id)
        {
            var accessToken = await getToken();
            var url=portalUrl+id.ToString();
            await url.WithOAuthBearerToken(accessToken).DeleteAsync();
        }

        public async Task<string> getToken()
        {
                var resultContent = await "http://localhost:8080/auth/realms/gym/protocol/openid-connect/token"
                    .PostUrlEncodedAsync(new { grant_type = "password", username = "master", password = "master", client_id = "Gym" })
                    .ReceiveString();

                Token token = JsonConvert.DeserializeObject<Token>(resultContent);
                return token.AccessToken;
        }

        public async Task SetUserPassword(string userId, string password)
        {
            string url = portalUrl + userId + "/reset-password";
            string accessToken = await getToken();
            await url
                   .WithOAuthBearerToken(accessToken)
                   .PutJsonAsync(new { type = "password", temporary = false, value = password });
        }

        public async Task SetRole(string userId, string personRole)
        {
            string url = portalUrl + userId + "/role-mappings/clients/"+ portalClientId;
            string id = "cf82b214-c40f-4640-954f-255a6def2a58";
            if (personRole == "Trainer")
            {
                id = "e63bbb03-bb69-4e0f-aeb8-29a5a583e6ca";
            }
            List<object> role = new List<object>() { new { clientRole = true, composite = false, containerId = portalClientId, id, name = personRole } };
            string accessToken = await getToken();

            await url
                   .WithOAuthBearerToken(accessToken)
                   .PostJsonAsync(role);
        }

        public async Task EnableUser(string userId)
        {
            string url = portalUrl + userId;
            string accessToken = await getToken();
            await url
                  .WithOAuthBearerToken(accessToken)
                  .PutJsonAsync(new { enabled = true });
        }


    }
}
