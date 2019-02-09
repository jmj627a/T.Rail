using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCooker
{
    class Program
    {
        //화면 출력 클래스 Console
        //화면 출력 메서드
        /*
         Console.Beep()
         Console.Clear()
         Console.SetCursorPosition()
         Console.SetWindowSize()
         Thread.Sleep(대기시간)
         */

         //콘솔 색상관련 속성
         /*
          * Console.BackGroundColor() 배경
          * Console.ForegroundColor() 글자 */

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);
            Random rand = new Random();
            ConsoleColor[] Color = {ConsoleColor.Blue,
                                    ConsoleColor.Red,
                                    ConsoleColor.Cyan,
                                    ConsoleColor.Yellow,
                                    ConsoleColor.Magenta,
                                    ConsoleColor.Gray};

            while(true)
            {
                Console.Clear();
                for(int i=0;i<30;++i)
                {
                    Console.ForegroundColor = Color[rand.Next(6)];
                    Console.SetCursorPosition(rand.Next(100), rand.Next(40));
                    Console.Write("Hello World");
                }

                Thread.Sleep(200);
            }
        }
    }
}
