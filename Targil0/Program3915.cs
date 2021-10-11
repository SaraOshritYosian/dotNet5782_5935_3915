using System;

namespace Targil0
{
    partial class Program5935
    {
        static void Main(string[] args)
        {
            Welcome3915();
            Welcome5935();
            Console.ReadKey();
        }
        static partial void Welcome5935();

        private static void Welcome3915()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
