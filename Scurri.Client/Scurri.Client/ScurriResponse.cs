using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Scurri.Client
{
    /// <summary>
    /// Wrapper class used for methods that have build in error handling.  try/catch built in 
    /// </summary>
    /// <typeparam name="T">Type of return object from the API Call. Reference Models.</typeparam>
    public class ScurriResponse<T>
    {
        public T data { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
