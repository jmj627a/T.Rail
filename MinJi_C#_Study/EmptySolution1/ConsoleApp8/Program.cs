using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            //int? a = null;
            //Console.WriteLine(sizeof(int?));

            int? num1 = null;

            if (num1.HasValue)
            {
                Console.WriteLine("올바른 값");
            }
            else
                Console.WriteLine("null값");

            Console.WriteLine("null:{0}",num1);

        }
    }
}
