using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VolgaYalchikTestovoe
{
    class Program
    {
        static void Main(string[] args)
        {
            var aboba = new List<string>();
            char[] sep = { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
            string text;
            Console.WriteLine("Введите полный путь до текстового файла с его названием и расширением...");
            string path = Console.ReadLine();
            StreamReader sr = new StreamReader(path);
            text = sr.ReadToEnd();
            string[] words;
            words = text.ToUpper().Split(sep);
            int count_mass = words.Length;
            string[,] count_words = new string[count_mass, 2];
            Dictionary<string, int> Count = new Dictionary<string, int>();
            int temp_counter = 0;
            if (words.Length != 0)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    temp_counter = 0;
                    if (words[i].Equals(""))
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words[j].Equals(""))
                            {
                                continue;
                            }

                            if (words[i] == words[j])
                            {
                                temp_counter++;
                                if (temp_counter >= 2)
                                {
                                    words[j] = "";
                                }
                                continue;
                            }
                        }
                        Count.Add(words[i], temp_counter);
                    }
                    
                }
            }

            Console.WriteLine("[ С Л О В О ] — [ К О Л И Ч Е С Т В О ]");
            foreach (var pair in Count.OrderByDescending(pair => pair.Value))
            {
                Console.WriteLine("{0} — {1}", pair.Key, pair.Value);
            }
        }
    }
}

