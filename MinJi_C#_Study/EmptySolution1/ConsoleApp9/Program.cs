using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            int value1 = 127;
            string str1 = value1.ToString();
            Console.WriteLine(str1);

            int value2 = Convert.ToInt32(str1);
            Console.WriteLine(value2);

            string str2 = "3.14";
            float value3 = float.Parse(str2);
            Console.WriteLine(value3);

        }
    }
}
