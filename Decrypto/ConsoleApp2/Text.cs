﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
namespace ConsoleApp2
{
    public class Text
    {
        Dictionary<char, char> key = new Dictionary<char, char>();
        const double v1 = 1, v2 = 1, v3 = 1;
        string rusDict = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public string bestKey;
        string text;

        public double bestMeaningfulness;

        Dictionary<string, double> controlMonoFrequency = new Dictionary<string, double>();
        Dictionary<string, double> controlBeFrequency = new Dictionary<string, double>();
        Dictionary<string, double> controlTriFrequency = new Dictionary<string, double>();


        Dictionary<string, double> monoFrequency = new Dictionary<string, double>();
        Dictionary<string, double> beFrequency = new Dictionary<string, double>();
        Dictionary<string, double> triFrequency = new Dictionary<string, double>();


        public double Step()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = i+1; j < 33; j++)
                {
                    string testingKey = bestKey;
                    List<char> tmp = new List<char>(testingKey);
                    char tmp2;
                    tmp2 = tmp[i];
                    tmp[i] = tmp[j];
                    tmp[j] = tmp2;
                    testingKey = new string(tmp.ToArray());
                    double newMean = Meaningfluense(testingKey);
                    if (newMean < bestMeaningfulness)
                    {
                        bestMeaningfulness = newMean;
                        bestKey = testingKey;
                    }          
                }
            }
            return bestMeaningfulness;
        }

        public string FindResponse()
        {
            Freq(rusDict);
            string output = "";
            foreach (var item in rusDict)
            {
                output += $"{item}; {monoFrequency[item.ToString()] / text.Length}\n";
            }

            File.WriteAllText("Freq.txt", output);
            return "";
        }

        public void FindStartKey()
        {
            Freq(rusDict);
            Dictionary<string,string> keyAssociation = new Dictionary<string, string>();
            var mono = monoFrequency.OrderBy(pair => pair.Value).ToList();
            var control = controlMonoFrequency.OrderBy(pair => pair.Value).ToList();
            for (int i = 0; i < 33; i++)
            {
                keyAssociation.Add(control[i].Key.ToString(),mono[i].Key.ToString());
            }

            bestKey = "";
            foreach (var item in rusDict)
            {
                bestKey += keyAssociation[item.ToString()];
            }
        }

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
            /*
            string output = "";
            foreach(var item in rusDict)
            {
                output += $"{item}; {controlMonoFrequency[item.ToString()]/text.Length}\n";
            }

            File.WriteAllText("MonoFreq.txt", output);*/
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
            /*
            string output = "";
            for (int i = 0; i < rusDict.Length; i++)
            {
                for (int j = 0; j < rusDict.Length; j++)
                {
                    output += $"{rusDict[i]}{rusDict[j]}; {controlBeFrequency[$"{rusDict[i]}{rusDict[j]}"] /= (text.Length - 1)}\n";
                }
            }
            File.WriteAllText("BeFreq.txt", output);*/
        }

        public void Freq(string key)
        {
            Dictionary<char, char> keyAss = new Dictionary<char, char>();
            for (int i = 0; i < 33; i++)
            {
                keyAss.Add(rusDict[i],key[i]);
            }
            foreach (char first in rusDict)
            {
                monoFrequency[$"{first}"] = 0;
                foreach (char second in rusDict)
                {
                    beFrequency[$"{first}{second}"] = 0;
                    foreach (char third in rusDict)
                    {
                        triFrequency[$"{first}{second}{third}"] = 0;
                    }
                }
            }
            for (int i = 0; i < text.Length - 2; i++)
            {
                monoFrequency[$"{keyAss[text[i]]}"] += 1;
                beFrequency[$"{keyAss[text[i]]}{keyAss[text[i + 1]]}"] += 1;
                triFrequency[$"{keyAss[text[i]]}{keyAss[text[i + 1]]}{keyAss[text[i + 2]]}"] += 1;
            }
            monoFrequency[$"{keyAss[text[text.Length - 2]]}"] += 1;
            monoFrequency[$"{keyAss[text[text.Length - 1]]}"] += 1;
            beFrequency[$"{keyAss[text[text.Length - 2]]}{keyAss[text[text.Length - 1]]}"] += 1;
            foreach (char first in rusDict)
            {
                monoFrequency[$"{first}"] /= text.Length;
                foreach (char second in rusDict)
                {
                    beFrequency[$"{first}{second}"] /= text.Length;
                    foreach (char third in rusDict)
                    {
                        triFrequency[$"{first}{second}{third}"] /= text.Length;
                    }
                }
            }
        }


        //double LocalPrecision

        double Meaningfluense(string key)
        {
            double g1 = 0;
            double g2 = 0;
            double g3 = 0;
            Freq(key);
            foreach (char first in rusDict)
            {
                g1 += Math.Abs(monoFrequency[$"{first}"] - controlMonoFrequency[$"{first}"]);
                foreach (char second in rusDict)
                {
                    g2 += Math.Abs(beFrequency[$"{first}{second}"] - controlBeFrequency[$"{first}{second}"]);
                    foreach (char third in rusDict)
                    {
                        g3 += Math.Abs(triFrequency[$"{first}{second}{third}"] - controlTriFrequency[$"{first}{second}{third}"]);
                    }
                }
            }
            return v1 * g1 + v2 * g2 + v3 * g3;
        }

        public Text (string path)
        {
            text = File.ReadAllText(path);
            text = new string (text.ToLower().Where(x => x >= 'а' && x <= 'я' || x =='ё').ToArray());
            string[] mono = File.ReadAllLines("MonoFreq.txt");
            string[] be = File.ReadAllLines("BeFreq.txt");
            string[] tri = File.ReadAllLines("TriFreq.txt");
            //todo
            foreach (string item in mono)
            {
                string[] tmp = item.Replace(";", string.Empty).Split(' ');
                controlMonoFrequency.Add(tmp[0],double.Parse(tmp[1]));
            }
            
            foreach (string item in be)
            {
                string[] tmp = item.Replace(";", string.Empty).Split(' ');

                controlBeFrequency.Add(tmp[0],double.Parse(tmp[1]));
            }
            
            foreach (string item in tri)
            {
                string[] tmp = item.Replace(";", string.Empty).Split(' ');

                controlTriFrequency.Add(tmp[0],double.Parse(tmp[1]));
            }
            
            FindStartKey();
            bestMeaningfulness = Meaningfluense(bestKey);
        }
    }
}
