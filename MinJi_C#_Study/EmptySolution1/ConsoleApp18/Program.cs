using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    //상수를 문자열로 대치하여 선언
    //요일에 대한 열거형 변수를 정수형 변수에 대입하여 출력해보자


    enum Days
    {
        sun = 1,
        mon =2,
        tue,
        wed,
        thu,
        fri,
        sat
    }



    class Program
    {
        static void Main(string[] args)
        {
            int val = (int)Days.mon;
            Days day = Days.tue;

            Console.WriteLine(val+" "  + day);
        }
    }
}
