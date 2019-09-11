using System;
using StackopsCore;
using StackopsCore.Factories;
using Autofac;
using MediatR;
using StackopsCore.Models;
using StackopsCore.Utils;

namespace StackopsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        { 
           try
            {
                if(args.Length != 2)
                    throw new ArgumentException("Expected two arguments 1) stack name 2) action."); 

                using(var scope = DependencyInjection.Init())
                {
                    var actionRequest   = new StackActionRequest(args[0], args[1]);
                    var configJsonPath  = FileUtils.GetAbsolutePathFromCurrentDirectory("stacks.json");
                    var allStacks       = StackFactory.CreateStacksFromJson(configJsonPath);
                    var mediatorRequest = StackRequestFactory.CreateMediatorRequest(actionRequest, allStacks);

                    scope.Resolve<IMediator>().Send(mediatorRequest).Wait();
                    Console.WriteLine("Command executed successfully.");
                }
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

            Console.WriteLine("---------------------------");
            Console.WriteLine("Please enter key to terminate.");
            Console.ReadLine();
        }
    }
}
