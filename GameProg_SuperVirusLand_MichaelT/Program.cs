using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProg_SuperVirusLand_MichaelT
{
    internal class Program
    {
        static char[,] map = {
            {'^','^','^','-','~','~','-','-','^','^'},
            {'-','^','-','-','~','~','-','^','^','^'},
            {'-','-','-','~','~','-','-','-','-','-'},
            {'~','~','-','-','-','-','~','~','-','-'},
            {'~','~','-','-','-','~','~','~','-','^'},
            {'~','~','-','^','-','-','-','~','-','^'},
            {'-','~','-','^','^','-','-','-','^','^'},
            {'-','-','-','^','^','^','^','^','^','^'}
        };

        static void printMap() 
        {
            for (int y = 0; y < map.GetLength(0); y++) 
            {
                for (int x = 0; x < map.GetLength(1); x++) 
                {
                    char c = map[y, x];

                    if (c == '-') Console.ForegroundColor = ConsoleColor.Green;
                    else if (c == '~') Console.ForegroundColor = ConsoleColor.Blue;
                    else if (c == '^') Console.ForegroundColor = ConsoleColor.Gray;
                    else Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(c);
                }
                Console.Write("\n");
            }
        }

        static void Main(string[] args)
        {
            printMap();
        }
    }
}
