using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "C:\\temp\\test.txt"; //백슬러시 2 개
            string str2 = @"C:\temp\test.txt"; //1개

            Console.WriteLine("{0} -- {1}", str1, str2);
        }
    }
}
