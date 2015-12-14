using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.CiscoSpark.Model
{
    public class AppAuthorizationRequest
    {
        public string response_type { get; set; } //Must be set to "code"
        public string client_id { get; set; } //Issued when creating your app
        public string redirect_uri { get; set; } //Must match one of the URIs provided during app registration
        public string scope { get; set; } //A space-separated list of scopes being requested by your app (https://developer.ciscospark.com/authentication.html)
        /// <summary>
        /// The state parameter is used to verify that the response from grant flow has not been tampered with along the way. 
        /// It is recommended that your app set this to a value that it verifiable once the user gives permission and the web browser is sent to your redirect_uri. 
        /// A second use for this parameter is to encode basic state information like an internal user ID or the URL of the last page they were on before entering the grant flow.
        /// </summary>
        public string state { get; set; } //A unique string that will be passed back to your app upon completion (see bellow)
    }
}
