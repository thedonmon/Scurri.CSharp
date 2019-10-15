using System;
using System.Collections.Generic;
using System.Text;

namespace Scurri.Client.Models
{
    public class ConsignmentDocument
    {
        /// <summary>
        /// Base64-encoded string containing raw invoice bytes.
        /// </summary>
        public string invoices { get; set; }
        /// <summary>
        /// Base64-encoded string containing raw label bytes.
        /// </summary>
        public string labels { get; set; }

        public string error { get; set; }
        public bool success { get; set; }
    }
}
