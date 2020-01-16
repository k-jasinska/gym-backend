using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class WeeksStatisticsDto
    {
        public DateTime Day { get; set; }
        public int EntranceCount { get; set; }
        public int IndividualCount { get; set; }
        public int GroupCount { get; set; }
    }
}
