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
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Please enter key to finish.");
            Console.ReadLine();
        }
    }
}
