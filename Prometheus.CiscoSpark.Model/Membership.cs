using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class Membership
    {
        public string id { get; set; }
        public string personId { get; set; }
        public string personEmail { get; set; }
        public string roomId { get; set; }
        public bool isModerator { get; set; }
        public bool isMonitor { get; set; }
        public string created { get; set; }
    }
}
