using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class CarnetType
    {
        [Key]
        public Guid CarnetTypeID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
}