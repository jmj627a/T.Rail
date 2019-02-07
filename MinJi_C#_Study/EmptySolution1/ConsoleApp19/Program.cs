using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    //IS연산자 사용
    class Program
    {
        static void Main(string[] args)
        {
            int value = 10;
            if(value is float)
            {
                Console.WriteLine("호환됨");
            }
           else
            {
                Console.WriteLine("호환안됨");
            }

            if (value is object)
            {
                Console.WriteLine("호환됨");
            }
            else
            {
                Console.WriteLine("호환안됨");
            }


            object obj = value;

            if(obj is int)
            {
                Console.WriteLine("호환됨");
            }
            else
            {
                Console.WriteLine("호환안됨");
            }
        }
    }
}
