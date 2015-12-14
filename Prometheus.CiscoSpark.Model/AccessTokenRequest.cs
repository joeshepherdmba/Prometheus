using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class AccessTokenRequest
    {
        public string grant_type { get; set; } //This should be set to "authorization_code"
        public string client_id { get; set; } //Issued when creating your app
        public string client_secret { get; set; } //Remember this guy? You kept it safe somewhere when creating your app
        public string code { get; set; } //The Authorization Code from the previous step
        public string redirect_uri { get; set; } //Must match the one used in the previous step

    }
}
