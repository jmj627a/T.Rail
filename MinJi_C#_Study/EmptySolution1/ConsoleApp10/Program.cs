using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    class Program
    {
        //int형 값을 박싱한 후에 다시 언박싱 하여 출력해보자.
        //박싱과 언박싱 과정에서 메모리 공유가 발생하는지, 또는 복사가 발생하는지 확인해보자.
        static void Main(string[] args)
        {
            //int i = 123;      //i라는 메모리 공간에 123이 저장됨
            //object obj = i;   //heap 메모리가 obj로 123이 복사가 됨 여기서 i를 456으로 바꾼다면
            //메모리를 공유하면 obj도 456으로 바뀜. 근데 참조, 공유하는 형태가 아니기때문에 123이 출력.
            //Console.WriteLine("{0}{1}", i, (int)obj);

            int i = 123;      
            object obj = i;
            i = 456;
            Console.WriteLine("{0}{1}", i, (int)obj);
        }
    }
}
