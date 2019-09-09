using System;
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
