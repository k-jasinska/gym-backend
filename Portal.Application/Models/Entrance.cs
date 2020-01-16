using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class Entrance
    {
        [Key]
        public Guid EntranceID { get; set; }
        public DateTime Date { get; set; }
        public Guid CarnetID { get; set; }
    }
}
