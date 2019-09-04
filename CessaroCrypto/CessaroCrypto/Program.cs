using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CessaroCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>();
            string rusDict = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string key = File.ReadAllText(args[0]);
            for (int i = 0; i < rusDict.Length; i++)
            {
                dict[rusDict[i]] = key[i];
            }

            string textToTranslate = File.ReadAllText(args[1]);
            textToTranslate = textToTranslate.ToLower();
            textToTranslate = new string(textToTranslate.Where(x => x >='а' && x <= 'я' || x == 'ё' ).ToArray<char>());
            string newText = "";
            for (int i = 0; i < textToTranslate.Length; i++)
            {
                newText += dict[textToTranslate[i]];
            }

            File.WriteAllText(args[2], newText);
        }
    }
}