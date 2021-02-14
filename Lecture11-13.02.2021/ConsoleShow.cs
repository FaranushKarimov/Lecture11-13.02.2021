using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture11_13._02._2021
{
    public static class ConsoleShow
    {

        public static void White<T>(T text)
        {
            System.Console.WriteLine(text);
        }
        public static void Red<T>(T text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Green<T>(T text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
