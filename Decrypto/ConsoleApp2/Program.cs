﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Text book = new Text("book.txt");
            book.CalculateFrequency();
            book.BeFreq();
            book.TriFreq();
            Console.ReadKey();

        }
    }
}