using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Scurri.Client
{
    public class ScurriResponse<T>
    {
        public T data { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
