using Microsoft.AspNetCore.Http;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.StartupConfiguration
{
    internal class RolesAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RolesAuthorizationMiddlewareOptions _options;

        public RolesAuthorizationMiddleware(RequestDelegate next, 
            Action<RolesAuthorizationMiddlewareOptions> configure)
        {
            _next = next;
            _options = new RolesAuthorizationMiddlewareOptions();
            configure(_options);
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return this._next(context);
            }

            context.User.UpdateRoleClaimValue(_options.ClientId,
                _options.AllowClientRoles,
                _options.AllowRealmRoles);
            return this._next(context);
        }
    }

}
