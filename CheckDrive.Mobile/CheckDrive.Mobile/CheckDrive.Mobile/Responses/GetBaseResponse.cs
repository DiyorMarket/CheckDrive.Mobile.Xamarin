﻿using System.Collections.Generic;

namespace CheckDrive.Mobile.Responses
{
    public abstract class GetBaseResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
