using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models
{
    public class ClientWithStatus
    {
        public Guid PersonID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string ContactData { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool ProfileComplete { get; set; } = false;
        public bool Exist { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }
}
