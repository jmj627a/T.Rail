using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        //데이터형이 분명한 var형을 선언한 후에 초기화 해보고 두 값을 더한 결과를 출력해보자.
        static void Main(string[] args)
        {
            var value1 = 3.14f;
            var value2 = "hello";
            var value3 = value2 + value1;
            Console.WriteLine(value1 + value2);
            Console.WriteLine(value3);
        }
    }
}
