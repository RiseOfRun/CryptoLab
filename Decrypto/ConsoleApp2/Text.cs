using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace ConsoleApp2
{
    public class Text
    {
        Dictionary<char, char> key = new Dictionary<char, char>();
        string rusDict = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string text;

        Dictionary<string, double> controlMonoFrequency = new Dictionary<string, double>();
        Dictionary<string, double> controlBeFrequency = new Dictionary<string, double>();
        Dictionary<string, double> controlTriFrequency = new Dictionary<string, double>();

        public void CalculateFrequency()
        {
            //Farm

            foreach (var c in rusDict)
            {
                controlMonoFrequency[c.ToString()] = 0;
            }

            foreach (var c in text)
            {
                controlMonoFrequency[c.ToString()] += 1;
            }
            string output = "";
            foreach(var item in rusDict)
            {
                output += $"{item} {controlMonoFrequency[item.ToString()]/text.Length*100}";
            }

            File.WriteAllText("MonoFreq.txt", output);
            //
        }

        public void BeFreq()
        {
            for (int i = 0; i < rusDict.Length; i++)
            {
                for (int j = 0; j < rusDict.Length; j++)
                {
                    controlBeFrequency[$"{rusDict[i]}{rusDict[j]}"] = 0;
                }
            }

            for (int i = 0; i < text.Length - 1; i++)
            {
                controlBeFrequency[$"{text[i]}{text[i + 1]}"] += 1;
            }

            string output = "";
            for (int i = 0; i < rusDict.Length; i++)
            {
                for (int j = 0; j < rusDict.Length; j++)
                {
                    output += $"{rusDict[i]}{rusDict[j]} {controlBeFrequency[$"{rusDict[i]}{rusDict[j]}"] /= (text.Length - 1)}";
                }
            }
            File.WriteAllText("BeFreq.txt", output);
        }

        public void TriFreq()
        {
            foreach (char first in rusDict)
            {
                foreach (char second in rusDict)
                {
                    foreach (char third in rusDict)
                    {
                        string test = $"{first}{second}{third}";
                        controlTriFrequency[test] = 0;
                    }
                }
            }
            string output = "";
            File.WriteAllText("TriFreq.txt", output);
            for (int i = 0; i < rusDict.Length; i++)
            {
                for (int j = 0; j < rusDict.Length; j++)
                {
                    for (int k = 0; k < rusDict.Length; k++)
                    {
                        output += $"{rusDict[i]}{rusDict[j]}{rusDict[k]} {controlTriFrequency[$"{rusDict[i]}{rusDict[j]}{rusDict[k]}"] /= (text.Length - 1)}";
                    }
                }
            }
            File.WriteAllText("BeFreq.txt", output);
        }


        //double LocalPrecision

        double Precision(Dictionary<char, char> key) { return 0; }

        public Text (string path)
        {
            text = File.ReadAllText(path);
            text = new string (text.ToLower().Where(x => x >= 'а' && x <= 'я' || x =='ё').ToArray());
        }
    }
}
