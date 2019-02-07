using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    //foreach 처음부터 끝까지 순차적으로 값을 반복하여 읽는 역할 (읽기전용)
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4 };

            foreach(int val in arr)
            {
                Console.WriteLine(val);
            }
        }
    }
}
