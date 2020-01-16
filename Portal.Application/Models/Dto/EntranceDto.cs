using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class EntranceDto
    {
        public Guid CarnetId { get; set; }
        public List<DateTime> Dates { get; set; }
        public CarnetType Type { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
