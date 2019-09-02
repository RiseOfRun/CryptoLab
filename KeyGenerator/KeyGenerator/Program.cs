using System;
using System.Collections.Generic;
using System.IO;
namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random seed = new Random();
            List <char> dict = new List<char> ( "абвгдеёжзийкламнопрстуфхцчшщъыьэюя");
            for (int i = 33; i >= 0; i--)
            {
                int j = seed.Next(i);
                char tmp = dict[j];
                dict[j] = dict[i];
                dict[i] = tmp;
            }
            File.WriteAllText(args[0] ,$"{new string(dict.ToArray())}");
        }
    }
}
