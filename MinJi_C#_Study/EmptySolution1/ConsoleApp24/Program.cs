using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp24
{
    class Program
    {
        static int[] CreateArray1(int _size)
        {
            int[] array1 = new int[_size];
            for (int i = 0; i < array1.Length; ++i)
                array1[i] = i;

            return array1;
        }
        static int[,] CreateArray2(int _size1, int _size2)
        {
            int[,] array1 = new int[_size1,_size2];
            for (int i = 0; i < _size1; ++i)
                for(int j=0;j<_size2;++j)
                     array1[i,j] = i + j;

            return array1;
        }

        //배열을 리턴하는 함수
        static void Main(string[] args)
        {
            int[] array1;
            int[,] array2;

            array1 = CreateArray1(5);
            array2 = CreateArray2(2, 3);

            for(int i=0;i<array1.Length; ++i)
            {
                Console.Write(array1[i] + "  " );
            }
            Console.WriteLine();

            for (int i = 0; i < 2; ++i)
                for (int j = 0; j < 3; ++j)
                    Console.Write(array2[i, j] + "  ");
            Console.WriteLine();
        }
    }
}
