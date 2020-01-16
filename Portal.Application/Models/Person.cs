using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Address { get; set; }
        public string ContactData { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Niepoprawny e-mail")]
        public string Email { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Login musi mieć min 3 znaki")]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
