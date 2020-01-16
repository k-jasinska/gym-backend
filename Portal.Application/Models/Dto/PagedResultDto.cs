using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Models
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalCount { get; set; }

    }
}
