using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{

    public struct myStruct
    {
        public int age;
    }

    public class myClass
    {
        public int age;
    }

    class Program
    {
        static void Main(string[] args)
        {
            myStruct test1 = new myStruct();
            test1.age = 12;

            myStruct test2 = test1;
            test2.age = 24;
            Console.WriteLine(test1.age + " " + test2.age);

            myClass test3 = new myClass();
            test3.age = 12;

            myClass test4 = new myClass();
            test4 = test3;
            test4.age = 24;
            Console.WriteLine(test3.age + " " + test4.age);
        }
    }
}
