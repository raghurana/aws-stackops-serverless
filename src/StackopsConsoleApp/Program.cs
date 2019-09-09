using System;
using System.IO;
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
                    var configFileFullPath  = Path.GetFullPath("config/sample-stacks.json", Environment.CurrentDirectory);
                    var allConfiguredStacks = StacksFactory.GetStacksFromJsonConfig(configFileFullPath);
                    

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
