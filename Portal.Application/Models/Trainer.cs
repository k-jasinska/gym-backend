using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Application.Models
{
    public class Trainer:Person
    {
        public List<Training> Training { get; set; }
    }
}
