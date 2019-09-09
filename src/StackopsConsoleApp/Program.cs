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
                    var mediator = scope.Resolve<IMediator>();
                    var command  = new Ec2IdsCommand(new string[] {}, CommandType.Start);
                    mediator.Send(command).Wait();
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
