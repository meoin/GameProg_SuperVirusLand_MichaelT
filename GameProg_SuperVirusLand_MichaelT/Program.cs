using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameProg_SuperVirusLand_MichaelT
{
    internal class Virus 
    {
        public int xPos;
        public int yPos;
        public ConsoleColor color;

        public Virus(int y, int x, ConsoleColor col)
        {
            xPos = x;
            yPos = y;
            color = col;
        }
    }

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

        static List<Virus> viruses = new List<Virus>();

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
            foreach (Virus virus in viruses) 
            {
                Console.SetCursorPosition(virus.xPos, virus.yPos);
                Console.ForegroundColor = virus.color;
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

                //Debug.WriteLine($"Virus {i} sees {map[viruses[i].yPos - 1, viruses[i].xPos]} above...");
                //Debug.WriteLine($"{map[viruses[i].yPos + 1, viruses[i].xPos]} below...");
                //Debug.WriteLine($"{map[viruses[i].yPos, viruses[i].xPos + 1]} to the right...");
                //Debug.WriteLine($"{map[viruses[i].yPos, viruses[i].xPos - 1]} to the left...");

                if (dir == 0) movement = (0, 1); // right
                else if (dir == 1) movement = (1, 0); // down
                else if (dir == 2) movement = (0, -1); // left
                else if (dir == 3) movement = (-1, 0); // up

                int targetX = viruses[i].xPos + movement.Item2;
                int targetY = viruses[i].yPos + movement.Item1;
                Debug.WriteLine($"Trying to move virus {i}");

                if (targetY >= 0 && targetY < map.GetLength(0) // vertical bounds
                && targetX >= 0 && targetX < map.GetLength(1)) // horizontal bounds
                {
                    if (map[targetY, targetX] == '-') 
                    {
                        Debug.WriteLine($"Virus {i} looks in {dir} direction and sees {map[targetY, targetX]}");
                        Debug.WriteLine($"Moving virus {i} to: X{targetX}, Y{targetY}");

                        int dupeRoll = rand.Next(0, 10);
                        if (viruses.Any(v => v.xPos == targetX && v.yPos == targetY))
                        {
                            if (viruses.Any(v => v.xPos == targetX && v.yPos == targetY && !(v.color == viruses[i].color))) 
                            {
                                Virus matchingVirus = viruses.Where(v => v.xPos == targetX && v.yPos == targetY && !(v.color == viruses[i].color)).First();
                                matchingVirus.color = viruses[i].color;
                            }
                        }
                        else 
                        {
                            if (dupeRoll == 0)
                            {
                                viruses.Add(new Virus(targetY, targetX, viruses[i].color));
                            }
                            else
                            {
                                viruses[i].yPos = targetY;
                                viruses[i].xPos = targetX;
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            viruses.Add(new Virus(1, 0, ConsoleColor.Magenta));
            viruses.Add(new Virus(6, 2, ConsoleColor.Yellow));
            viruses.Add(new Virus(5, 8, ConsoleColor.Cyan));

            while (true) 
            {
                PrintMap();
                Thread.Sleep(500);
                //Console.ReadKey();
                VirusSteps();
            }
        }
    }
}
