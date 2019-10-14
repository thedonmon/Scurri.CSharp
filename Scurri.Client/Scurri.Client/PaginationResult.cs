using System;
using System.Collections.Generic;
using System.Text;

namespace Scurri.Client
{
    public class PaginationResult<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public List<T> results { get; set; }
    }
}
