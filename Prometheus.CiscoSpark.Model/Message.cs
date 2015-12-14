using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class Message
    {
        public string id { get; set; }
        public string personId { get; set; }
        public string personEmail { get; set; }
        public string roomId { get; set; }
        public string text { get; set; }
        public List<string> files { get; set; }
        public string toPersonId { get; set; }
        public string toPersonEmail { get; set; }
        public string created { get; set; }
    }
}
