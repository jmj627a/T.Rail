using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    //구조체 안의 const 또는 static 변수에 대해서만 선언과 동시에 초기화하는 예


    public struct myStruct
    {
        public const float pi = 3.14f;
        public static int age = 12;
        public int val;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} {1}", myStruct.pi, myStruct.age);

            //이렇게 하려면 선언하고 써야한다.
            myStruct a;
            a.val = 10;
            //myStruct.val = 10;
            Console.WriteLine(a.val);
        }
    }
}
