using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class PasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
