using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //이 program이라는 class는 ConsolApp1 이라는 namespace안에 있는 것이다.

    //클래스의 정적변수 bool형의 기본 값과 지역 bool 변수 값을 출력해보자.
    class Program 
    {
        static bool BoolVar;
        static void Main(string[] args)
        {
            //Console.WriteLine("helloworld!");
            //Console.WriteLine("{0},{1}", 3.14f, 12);
            //Console.WriteLine("{0:c} {1:p} {0:x}", 123, 123.45, 123);
            ////\123 12,345.00% 7b
        
           
            bool LocalBoolVar =true ;
            Console.WriteLine("{0} {1}", BoolVar, LocalBoolVar);
        }
    }
}
