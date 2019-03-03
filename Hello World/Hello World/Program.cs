using System;

namespace HelloWorld
{
    class HelloWorld
    {
        public void Hello()
        {
            Console.WriteLine("Hello World!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorld v = new HelloWorld();
            v.Hello();
        }
    }
}