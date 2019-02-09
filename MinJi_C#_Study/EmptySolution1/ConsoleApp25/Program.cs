using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp25
{
    class Program
    {
        static void Main(string[] args)
        {
            //파일 생성, 열기, 쓰기 
            int value = 12;
            float value2 = 3.14f;
            string str1 = "hello world";
            FileStream fs = new FileStream("test.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(value);
            sw.Write(value2);
            sw.Write(str1);
            sw.Close();


            using (StreamWriter sw1 = new StreamWriter(new FileStream("test1.txt", FileMode.Create)))
            {
                sw1.WriteLine(value);
                sw1.WriteLine(value2);
                sw1.WriteLine(str1);
            }

            //읽고 출력하기
            fs = new FileStream("test1.txt", FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);// or ("text1.txt");
            int value3 = int.Parse(sr.ReadLine());
            float value4 = float.Parse(sr.ReadLine());
            string str2 = sr.ReadLine();
            sr.Close();
            Console.WriteLine(value3 + value4 +  str2);
        }
    }
}
