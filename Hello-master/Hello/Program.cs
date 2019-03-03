using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World! {0} is {1}",args[0],args[1]);
            Console.BackgroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Hello World!");
            System.Console.WriteLine("Hello World!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Hello World!");
            Console.ResetColor();
            System.Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
