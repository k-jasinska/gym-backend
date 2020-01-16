using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class RolesAuthorizationMiddlewareOptions
    {
        /// <summary>
        /// Określa, czy uwzględniać role realmu przy autoryzacji, domyślnie false
        /// </summary>
        public bool AllowRealmRoles { get; set; } = false;

        /// <summary>
        /// Role z tego klienta będą uwzględniane przy autoryzacji, domyślnie string.Empty
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Określa, czy uwzględniać role clienta przy autoryzacji, domyślnie true
        /// </summary>
        public bool AllowClientRoles { get; set; } = true;
    }
}
