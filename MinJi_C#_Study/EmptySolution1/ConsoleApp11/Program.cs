using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        //사용자가 누른 키를 화면에 출력하되, escape키가 입력되면 프로그램을 종료하도록 작성해보자
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyinfo;

            do
            {
                keyinfo = Console.ReadKey(true);
                Console.WriteLine(keyinfo.KeyChar);
                if (keyinfo.KeyChar == 'a')
                {
                    Console.WriteLine("a키가 눌렸다");
                }
                else if (keyinfo.KeyChar == 'A')
               {
                    Console.WriteLine("A키가 눌렸다");
                }
            }
            while (keyinfo.Key != ConsoleKey.Escape);

        }
    }
}
