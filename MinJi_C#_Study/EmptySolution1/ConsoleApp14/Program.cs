using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    //구조체에 생성자와 소멸자를 명시하고 new로 생성했을 때와 일번 선언했을때의 초기화
    public struct myStruct
    {
        public int age;
        public myStruct(int _age)
        {
            age = _age;
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            myStruct test1;
            test1.age = 12;
            Console.WriteLine(test1.age);

            myStruct test2 = new myStruct();
            Console.WriteLine(test2.age);

            myStruct test3 = new myStruct(12);
            Console.WriteLine("{0}", test3.age);


        }
    }
}
