using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    //국어영어수학 점수를 구조체 멤버변수로 입력하고
    //compute()메서드를 호출하면 평균과 총점이 계산되도록 하여 프로그래밍하자.
    class Program
    {
        public struct Score
        {
            public int korean;
            public int english;
            public int math;
            public int average;
            public int total;

            public void compute()
            {
                total = korean + english + math;
                average = total / 3;
            }
        }

        static void Main(string[] args)
        {
            Score score = new Score();

            score.korean = 80;
            score.english = 90;
            score.math = 60;
            score.compute();

            Console.WriteLine("총점 : {0} , 평균 {1} ", score.total, score.average);
        }


        
    }
}
