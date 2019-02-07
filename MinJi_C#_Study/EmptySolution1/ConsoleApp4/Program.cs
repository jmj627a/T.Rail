using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        //short형의 유효 범위 값을 출력해보자.
        static void Main(string[] args)
        {
            Console.WriteLine("{0}~{1}", ushort.MinValue, ushort.MaxValue);
            Console.WriteLine("{0}~{1}", short.MinValue, short.MaxValue);
        }
    }
}
