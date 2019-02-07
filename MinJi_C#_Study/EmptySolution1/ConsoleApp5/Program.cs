using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        //두 문자열을 합친 후에 합친 문자열과 다섯번째 문자를 출력해보자
        //문자열의 문자 개수를 length로 확인해보자
        static void Main(string[] args)
        {
            string str1 = "hello";
            string str2 = "world";
            string str3 = str1 + str2;
            string str4 = "program" + "ming";

            if(str3 == "helloworld")
            {
                Console.WriteLine("같음");
            }
            else
            {
                Console.WriteLine("다름");
            }

            Console.WriteLine("{0}, {1}",str3, str4);
            Console.WriteLine(str3[4]); //[10]하면 인덱스의 값이 벗어났다고 나옴


            Console.WriteLine(str3.Length);

        }

    }
}
