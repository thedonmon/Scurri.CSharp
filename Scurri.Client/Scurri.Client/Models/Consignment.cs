using System;
using System.Collections.Generic;
using System.Text;

namespace Scurri.Client.Models
{
    public class Consignment
    {
        public string carrier { get; set; }
        public string consignment_number { get; set; }
        public DateTime? create_date { get; set; }
        public string currency { get; set; }
        public CurrentStatus current_status { get; set; }
        public string delivery_instructions { get; set; }
        public string identifier { get; set; }
        public Invoice invoice { get; set; }
        public Options options { get; set; }
        public string order_number { get; set; }
        public double? order_value { get; set; }
        public List<Package> pacakages { get; set; }
        public Recipient recipient { get; set; }
        public string rules_package_type { get; set; }
        public string service { get; set; }
        public string service_id { get; set; }
        public string shipping_method { get; set; }
        public string tracking_url { get; set; }
        public string warehouse_id { get; set; }
    }

    public class Package
    {
        public string description { get; set; }
        public string reference { get; set; }
        public double height { get; set; }
        public List<Item> items { get; set; }
        public int lenght { get; set; }
        public string tracking_number { get; set; }
        public double width { get; set; }
    }

    public class Item
    {
        public string country_of_origin { get; set; }
        public string harmonisation_code { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; }
        public double value { get; set; }
        public double weight { get; set; }
    }

    public class Options
    {
        public string package_type { get; set; }
        public string signed { get; set; }
    }

    public class Invoice
    {
        public string incoterm { get; set; }
    }

    public class CurrentStatus
    {
        public string rejection_reason { get; set; }
        public string short_form { get; set; }
        public string status { get; set; }
    }
}
