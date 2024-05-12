using System;
using System.Collections.Generic;
using System.Text;

namespace CheckDrive.Mobile.Helpers
{
    public class MetaData
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
