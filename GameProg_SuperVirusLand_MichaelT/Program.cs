using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

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

        static List<(int, int)> viruses = new List<(int, int)>();

        static void PrintMap() 
        {
            Console.Clear();

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

            ShowViruses();
        }

        static void ShowViruses() 
        {
            foreach ((int, int) virus in viruses) 
            {
                Console.SetCursorPosition(virus.Item2, virus.Item1);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write('%');
            }
            Console.SetCursorPosition(0, map.GetLength(0) + 1);
        }

        static void VirusSteps() 
        {
            Random rand = new Random();

            for (int i = 0; i < viruses.Count; i++)
            {
                int dir = rand.Next(0, 4);
                (int, int) movement = (0, 0);

                //Debug.WriteLine($"Virus {i} sees {map[viruses[i].Item1 - 1, viruses[i].Item2]} above...");
                //Debug.WriteLine($"{map[viruses[i].Item1 + 1, viruses[i].Item2]} below...");
                //Debug.WriteLine($"{map[viruses[i].Item1, viruses[i].Item2 + 1]} to the right...");
                //Debug.WriteLine($"{map[viruses[i].Item1, viruses[i].Item2 - 1]} to the left...");

                if (dir == 0) movement = (0, 1); // right
                else if (dir == 1) movement = (1, 0); // down
                else if (dir == 2) movement = (0, -1); // left
                else if (dir == 3) movement = (-1, 0); // up

                (int, int) target = (viruses[i].Item1 + movement.Item1, viruses[i].Item2 + movement.Item2);
                Debug.WriteLine($"Trying to move virus {i}");

                if (target.Item1 >= 0 && target.Item1 < map.GetLength(0) // vertical bounds
                && target.Item2 >= 0 && target.Item2 < map.GetLength(1)) // horizontal bounds
                {
                    if (map[target.Item1, target.Item2] == '-' && !viruses.Contains(target)) 
                    {
                        Debug.WriteLine($"Virus {i} looks in {dir} direction and sees {map[target.Item1, target.Item2]}");
                        Debug.WriteLine($"Moving virus {i} to: X{target.Item2}, Y{target.Item1}");

                        int dupeRoll = rand.Next(0, 10);

                        if (dupeRoll == 0)
                        {
                            viruses.Add((target.Item1, target.Item2));
                        }
                        else 
                        {
                            viruses[i] = target;
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            viruses.Add((1, 0));
            viruses.Add((6, 2));
            viruses.Add((5, 8));

            while (true) 
            {
                PrintMap();
                Thread.Sleep(500);
                VirusSteps();
            }
        }
    }
}
