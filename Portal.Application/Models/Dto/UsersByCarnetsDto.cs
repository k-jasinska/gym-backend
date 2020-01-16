using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class UsersByCarnetsDto
    {
        public string CarnetName { get; set; }
        public int CountUsers { get; set; }
        public double Percent { get; set; }
    }
}
