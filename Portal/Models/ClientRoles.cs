using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    internal class ClientRoles
    {
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
    }

}
