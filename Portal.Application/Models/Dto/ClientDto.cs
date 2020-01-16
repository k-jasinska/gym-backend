using Portal.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class ClientDto
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [System.ComponentModel.DefaultValue("")]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Sex Sex { get; set; }
        [Required]
        public string ContactData { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Niepoprawny e-mail")]
        public string Email { get; set; }
    }
}
