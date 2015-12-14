using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class Webhook
    {
        public string id { get; set; }
        public string name { get; set; }
        public string targetUrl { get; set; }
        public string resource { get; set; }
        public string @event { get; set; }
        public string filter { get; set; }
        public string created { get; set; }
    }
}
