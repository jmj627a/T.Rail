using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    class Program
    {
        static void Main(string[] args)
        {
            //int형 이차원 배열
            int[,] array1 = new int[2, 2];
            array1[0, 0] = 1;
            array1[0, 1] = 2;
            array1[1, 0] = 3;
            array1[1, 1] = 4;

            for(int i=0;i<2;++i)
            {
                for(int j=0;j<2;++j)
                {
                    Console.WriteLine(array1[i, j]);
                }
            }
            Console.WriteLine('\n');

            int[,] array2 = { { 1, 2 }, { 3, 4 } };
            for(int i=0;i<2;++i)
                for (int j = 0; j < 2; ++j)
                    Console.WriteLine(array2[i, j]);
            Console.WriteLine('\n');

        }
    }
}
