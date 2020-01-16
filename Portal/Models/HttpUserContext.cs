using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.StartupConfiguration;

namespace Portal.Models
{
    public class HttpUserContext : IUserContext
    {
        private readonly ClaimsPrincipal _user;

        public Guid CurrentUserId
        {
            get
            {
                string id = GetIdFromClaims();
                return Guid.Parse(id);
            }
        }

    public HttpUserContext(IHttpContextAccessor httpContext)
        {
            if (httpContext.HttpContext != null)
            {
                _user = httpContext.HttpContext.User;
            }
        }

        public string GetIdFromClaims()
        {
            return _user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
