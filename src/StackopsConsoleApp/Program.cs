using System;
using Autofac;
using MediatR;
using StackopsCore;

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
                    var mediator    = scope.Resolve<IMediator>();
                    var instanceIds = new [] { "" };
                    var command     = new Ec2IdsCommand(instanceIds, CommandType.Stop);
                    var t           = mediator.Send(command).Result;
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
