using System;
using System.Collections.Generic;
using System.Text;

namespace Scurri.Client
{
    public class CreateObjectResponse
    {
        public Dictionary<string, object> errors { get; set; }
        public List<string> success { get; set; }
    }
}
