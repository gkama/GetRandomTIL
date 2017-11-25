using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace GetRandomTIL
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<response> FunctionHandler(APIGatewayCustomAuthorizerRequest request, ILambdaContext context)
        {
            try
            {
                Get get_til = new Get("reddit_til", context);
                string child_str = JsonConvert.SerializeObject(await get_til.Child(), Formatting.Indented);

                //Response
                var response = new response()
                {
                    statusCode = "200",
                    headers = new Dictionary<string, string>() { { "Access-Control-Allow-Origin", "*" } },
                    body = child_str
                };

                //Return
                return response;
            }
            catch (Exception e)
            {
                //Response
                var response = new response()
                {
                    statusCode = "400",
                    headers = new Dictionary<string, string>() { { "Access-Control-Allow-Origin", "*" } },
                    body = e.Message
                };

                //Return
                return response;
            }
        }

        //Response
        public class response
        {
            public string statusCode { get; set; }
            public Dictionary<string, string> headers { get; set; }
            public string body { get; set; }
        }
    }
}
