using System.Net;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using StackopsCore.Models;

[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace StackopsServerlessFunctions
{
    public class Functions
    {
        public void StackRequestHandler(StackActionRequest request, ILambdaContext context)
        {
            context.Logger.LogLine($"StackRequestHandler: {request}");
        }
    }
}
