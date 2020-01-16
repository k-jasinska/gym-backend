using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    internal class RealmRoles
    {
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
    }

}
