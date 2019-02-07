using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    public struct mystruct
    {
        public int age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            mystruct test1, test2;
            test2.age = 10;
            test1.age = test2.age;
            Console.WriteLine("{0}", test1.age);

            mystruct test;
            test = new mystruct();
            Console.WriteLine(test.age);
        }
    }
}
