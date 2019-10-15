using System;
using System.Collections.Generic;
using System.Text;

namespace Scurri.Client.Models
{
    public class Manifest
    {
        public List<string> consignment_ids { get; set; }
        public string carrier_id { get; set; }
        public string warehouse_id { get; set; }
    }
    public class ManifestResponse
    {
        public List<string> consignment_ids { get; set; }
        public string carrier_id { get; set; }
        public string warehouse_id { get; set; }
        public string identifier { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
    }


}
