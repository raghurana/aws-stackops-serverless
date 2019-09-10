using System.Linq;
using System;
using System.IO;
using StackopsCore;
using StackopsCore.Factories;
using Autofac;
using MediatR;
using StackopsCore.Commands;

namespace StackopsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using(var scope = DependencyInjection.Init())
                {
                    var jsonPath  = Path.GetFullPath("config/sample-stacks.json", Environment.CurrentDirectory);
                    var allStacks = StacksFactory.GetStacksFromJsonConfig(jsonPath);
                    var command   = new StartStackCommand(allStacks.First());

                    scope.Resolve<IMediator>().Send(command).Wait();
                    Console.WriteLine("Command Sent Successfully.");
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("An exception occured with message below:");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(ex.Message);
               
                Console.WriteLine("Stack Trace:");
                Console.WriteLine("--------------");
                Console.WriteLine(ex.StackTrace);

                if(ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine("--------------");
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            Console.WriteLine("Please enter key to finish.");
            Console.ReadLine();
        }
    }
}
