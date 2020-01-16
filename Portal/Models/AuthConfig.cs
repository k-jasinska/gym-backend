using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class AuthConfig
    {
        [JsonRequired]
        public IEnumerable<string> Audiences { get; set; }

        [JsonRequired]
        public string ServerAddress { get; set; }

        [JsonRequired]
        public string Realm { get; set; }

        [JsonRequired]
        public string ConnectionStrings { get; set; }

        [JsonRequired]
        public string ClientID { get; set; }
    }
}
