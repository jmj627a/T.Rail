using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp23
{
    class Program
    {
        //가변배열
        //다차원 가변배열
        static void Main(string[] args)
        {
            int[][] array = new int[2][];
            array[0] = new int[3] { 1, 2, 3 };
            array[1] = new int[2] { 4, 5 };

            for(int i=0;i<array.Length;++i)
            {
                for(int j=0;j<array[i].Length;++j)
                {
                    Console.WriteLine(array[i][j]);
                }
            }


            //면 행 열
            int[][][] array1 = new int[2][][];
            array1[0] = new int[2][];
            array1[1] = new int[3][];

            array1[0][0] = new int[3] { 1, 2, 3 };
            array1[0][1] = new int[2] { 4,5};

            array1[1][0] = new int[3] { 6, 7, 8 };
            array1[1][1] = new int[2] { 9,10};
            array1[1][2] = new int[2] { 11,12};

            for (int i = 0; i < array1.Length; ++i)
            {
                for (int j = 0; j < array1[i].Length; ++j)
                {
                    for(int k=0;k<array1[i][j].Length;++k)
                         Console.Write(array1[i][j][k]);
                }
            }
        }
    }
}
