using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models.Dto
{
    public class MonthStatisticsDto
    {
        public int Month { get; set; }
        public int EntranceCount { get; set; }
        public int IndividualCount { get; set; }
        public int GroupCount { get; set; }
    }
}
