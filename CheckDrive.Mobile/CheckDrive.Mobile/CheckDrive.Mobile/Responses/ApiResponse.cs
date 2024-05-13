using CheckDrive.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CheckDrive.Mobile.Responses
{
    public class ApiResponse<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public MetaData<T> Metadata { get; private set; }

        public ApiResponse()
        {
            Data = new List<T>();
            Metadata = new MetaData<T>();
        }

        public static implicit operator ApiResponse<T>(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
