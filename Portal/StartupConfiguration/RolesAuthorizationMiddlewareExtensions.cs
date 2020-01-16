using Microsoft.AspNetCore.Builder;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.StartupConfiguration
{
    public static class RolesAuthorizationMiddlewareExtensions
    {
        /// <summary>
        ///Middleware umożliwiający autoryzację za pomocą ról ([Authorize(Roles=...)]).
        ///Powinien być dodany pomiędzy middleware od autentykacji (UseAuthentication) i mvc (UseMvc)
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRolesAuthorization(
            this IApplicationBuilder builder,
            Action<RolesAuthorizationMiddlewareOptions> configure)
        {
            return builder.UseMiddleware<RolesAuthorizationMiddleware>(configure);
        }
    }

}
