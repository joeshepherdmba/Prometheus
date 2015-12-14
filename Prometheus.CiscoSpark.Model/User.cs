using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class User
    {
        public string id { get; set; }
        public List<string> emails { get; set; }
        public string displayName { get; set; }
        public string avatar { get; set; }
        public string created { get; set; }
    }
}
