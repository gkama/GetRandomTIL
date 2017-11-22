using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

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
        public async Task<string> FunctionHandler(ILambdaContext context)
        {
            Get get_til = new Get("reddit_til", context);
            string child_str = JsonConvert.SerializeObject(await get_til.Child(), Formatting.Indented);

            //Return
            return child_str;
        }
    }
}
