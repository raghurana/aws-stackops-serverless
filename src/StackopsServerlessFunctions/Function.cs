using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using Autofac;
using MediatR;
using StackopsCore;
using StackopsCore.Factories;
using StackopsCore.Utils;
using System;
using System.Threading.Tasks;

namespace StackopsServerlessFunctions
{
    public class Function
    {
        private static async Task Main(string[] args)
        {
            Func<string, ILambdaContext, string> func = FunctionHandler;

            using(var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new JsonSerializer()))
            {
                using(var bootstrap = new LambdaBootstrap(handlerWrapper))
                {
                    try
                    {
                        await bootstrap.RunAsync();
                    }

                    catch(Exception ex)
                    {
                        Console.WriteLine("An exception occured with message below:");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine(ex.Message);
                    
                        Console.WriteLine("Stack Trace:");
                        Console.WriteLine("------------");
                        Console.WriteLine(ex.StackTrace);

                        if(ex.InnerException != null)
                        {
                            Console.WriteLine("Inner Exception:");
                            Console.WriteLine("---------------");
                            Console.WriteLine(ex.InnerException.Message);
                        }
                    }
                }
            }
        }

        public static string FunctionHandler(string actionRequest, ILambdaContext context)
        { 
            Console.WriteLine($"Received Action: {actionRequest}");

            using(var scope = DependencyInjection.Init())
            {
                var configJsonPath  = FileUtils.GetAbsolutePathFromCurrentDirectory("stacks.json");
                var allStacks       = StackFactory.CreateStacksFromJson(configJsonPath);
                var mediatorRequest = StackRequestFactory.CreateMediatorRequest(allStacks, actionRequest);

                Console.WriteLine("Stacks affected:");
                Array.ForEach(allStacks, stack => Console.WriteLine(stack.Name));
                scope.Resolve<IMediator>().Send(mediatorRequest).Wait();    
            }

            Console.WriteLine("Command executed successfully.");
            return "Command executed successfully.";
        }
    }
}
