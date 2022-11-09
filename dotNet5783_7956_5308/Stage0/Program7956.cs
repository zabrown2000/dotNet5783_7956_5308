using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil0
{
    partial class Program
    {
        
        static void Main(string[] args)
        {
            Welcome7956();
            Console.ReadKey();
        }

        static partial void Welcome5308()
        {
            Console.WriteLine("I am also here");
        }

        private static void Welcome7956()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
