using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using StackopsCore.Factories;
using StackopsCore.Models;
using StackopsCore.Utils;
using System;
using System.Threading.Tasks;

namespace StackopsServerlessFunctions
{
    public class Function
    {
        private static async Task Main(string[] args)
        {
            Func<StackActionRequest, ILambdaContext, string> func = FunctionHandler;

            using(var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new JsonSerializer()))
            {
                using(var bootstrap = new LambdaBootstrap(handlerWrapper))
                {
                    await bootstrap.RunAsync();
                }
            }
        }

        public static string FunctionHandler(StackActionRequest request, ILambdaContext context)
        { 
            var configJsonPath = FileUtils.GetAbsolutePathFromCurrentDirectory("stacks.json");
            var allStacks      = StackFactory.CreateStacksFromJson(configJsonPath);

            return Newtonsoft.Json.JsonConvert.SerializeObject(allStacks);
        }
    }
}
