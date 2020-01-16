using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Portal.Application.Models
{
    public class Carnet
    {
        [Key]
        public Guid CarnetID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public CarnetType Type { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ClientID { get; set; }
    }
}
