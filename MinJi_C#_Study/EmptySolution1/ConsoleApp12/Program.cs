using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            int kor, eng, math, total;
            float average;

            Console.Write("국어 점수를 입력해 주세요. : ");
            kor = Convert.ToInt32(Console.ReadLine());

            Console.Write("영어 점수를 입력해 주세요. : ");
            eng = Convert.ToInt32(Console.ReadLine());

            Console.Write("수학 점수를 입력해 주세요. : ");
            math = Convert.ToInt32(Console.ReadLine());

            total = kor + eng + math;

            average = total / 3.0f;

            Console.WriteLine("{0} {1} {2} {3} ", kor, eng, math, average);
        }
    }
}
