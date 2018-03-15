using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace ChomadoVoice
{
    public static class GoogleHome
    {
        [FunctionName("GoogleHome")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            /*string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;*/

            // Get request body
            var data = await req.Content.ReadAsAsync<Models.DialogFlowResponseModel>();
            //log.Info(data);
            //var json = await req.Content.ReadAsStringAsync();
            var say = data.Result.ResolvedQuery;

            var result = req.CreateResponse(HttpStatusCode.OK, new
            {
                speech = $"あなたは{say}と言いましたね",
                displayText = $"あなたは{say}と言いましたね"
            });
            result.Headers.Add("ContentType", "application/json");
            return result;

            // Set name to query string or body data
            //name = name ?? data?.name;

            //return name == null
            //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            //    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}
