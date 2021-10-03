using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4280();
            Welcome3596();
            Console.ReadKey();
        }

        static partial void Welcome3596();

        private static void Welcome4280()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
