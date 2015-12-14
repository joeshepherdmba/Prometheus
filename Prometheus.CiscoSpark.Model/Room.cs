using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class Room
    {
        public string id { get; set; }
        public string title { get; set; }
        public string created { get; set; }
        public string lastActivity { get; set; }
    }
}
