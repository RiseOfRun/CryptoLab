using System;
using System.Collections.Generic;
using System.IO;
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
            double k = double.PositiveInfinity;
            double best = book.bestMeaningfulness;
            int i = 0;
            while (k!=best&&i<10)
            {
                best = book.bestMeaningfulness;
                k = book.Step();
                i++;
            }

            string key = "";
            foreach (var item in book.bestKey)
            {
                key += item;
            }
            File.WriteAllText("outKey.txt",key);
        }
    }
}
