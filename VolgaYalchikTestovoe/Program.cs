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
            string text = "";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введи полный путь до текстового файла с его названием.\n" +
                "Также можешь ввести название файла, если он хранится на твоём рабочем столе.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("При обоих вариантах ввода не указывай расширение файла.");
            Console.ResetColor();

            string temp_path;
            string path;
            while (true)
            {
                temp_path = Console.ReadLine();
                try
                {
                    if (temp_path.Contains('/') || temp_path.Contains('\\'))
                    {
                        path = temp_path + ".txt";
                    }
                    else
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        path = desktopPath + "\\" + temp_path + ".txt";
                    }
                    StreamReader sr = new StreamReader(path);
                    text = sr.ReadToEnd();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Файл успешно загружен. \nНачинаю обрабатывать данные.");
                    Console.ResetColor();
                    break;
                }
                catch (Exception ex)
                {
                    Log("Файл не найден или некорректное расширение файла. Используйте только файлы с расширением \".txt\". " +
                        "Сообщение ошибки: " + ex.Message + "\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Упс... Произошла ошибка, проверь введённые данные и отправь корректный путь.");
                    Console.ResetColor();
                    continue;
                }
            }

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
                        try
                        {
                            Count.Add(words[i], temp_counter);
                        }
                        catch (Exception ex)
                        {
                            Log("Невозможно добавить слово в список." +
                            "Сообщение ошибки: " + ex.Message + "\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ой-ой-ой... Увы, я, по непонятной для меня причине, не могу добавить слово в спиок.");
                            Console.ResetColor();
                            continue;
                        }
                    }
                    
                }
            }

            Console.WriteLine("[ С Л О В О ] — [ К О Л И Ч Е С Т В О ]");
            foreach (var pair in Count.OrderByDescending(pair => pair.Value))
            {
                Console.WriteLine("{0} — {1}", pair.Key, pair.Value);
            }
        }
        public static void Log(string message)
        {
            File.AppendAllText("log.txt","[ " + DateTime.Now.ToLongTimeString() + " | " + DateTime.Now.ToShortDateString() + " ] " + message + "\n");
        }
    }
}

