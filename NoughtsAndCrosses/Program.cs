using System;
using System.Threading;

namespace NoughtsAndCrosses {
    class Program {
        static void Main(string[] args) {
            var player = false;

            bool won = false;

            Console.WriteLine("Starting game...");
            string[] locations = new string[9];
            for (int i = 0; i < 9; i++) locations[i] = "-";

            Console.Clear();
            var graph = new GridGraphic();
            Console.WriteLine("Loading game...");

            WinChecker checker = new WinChecker();
            AI ai = null; // new AI("X", "O");
            Thread.Sleep(200);
            AI ai2 = null; // new AI("O", "X");
            Thread.Sleep(200);

            Random r = new Random();
            Thread.Sleep(50);
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());

            int gameCount = 1;

            if (!player) {
                var aaa = r.Next(100);
                Console.Clear();
                if (r.Next() % 2 == 0) {
                    ai = new AI("X", "O");
                    ai2 = new AI("O", "X");
                }
                else {
                    ai2 = new AI("X", "O");
                    ai = new AI("O", "X");
                }
            }
            else {
                ai = new AI("X", "O");
            }



            while (!won) {
                Console.WriteLine($"Game {gameCount}");
                graph.DrawGridFromArray(locations);
                if (player) {
                    Console.WriteLine("Enter index of cell for your play:");
                    var index = Convert.ToInt32(Console.ReadLine());
                    try {
                        if (locations[index] == "-") locations[index] = "O";
                        else {
                            Console.WriteLine("Invalid location, or that location is already full. Move forfeited.");
                        }
                    }
                    catch {
                        Console.WriteLine("Invalid location, or that location is already full. Move forfeited.");
                    }
                }
                else {
                    locations[ai2.getNextMove(locations)] = ai2.symbol;
                    Console.Clear();
                    Console.WriteLine($"Game {gameCount}");
                    graph.DrawGridFromArray(locations);
                }

                won = checker.checkWin(locations, "O");
                if (won) {
                    Console.WriteLine($"Game {gameCount}");
                    graph.DrawGridFromArray(locations);
                    if (player) Console.WriteLine("Player wins!");
                    else Console.WriteLine($"{ai2.symbol} AI wins!");
                    break;
                }

                // Check for a tie
                bool tie = true;
                foreach (string a in locations) {
                    if (a == "-") tie = false;
                }

                if (tie) {
                    Console.WriteLine("Tie!");
                    for (int i = 0; i < 9; i++) locations[i] = "-";
                    gameCount++;

                    if (!player && ai2.symbol == "O") {
                        ai2 = new AI("X", "O");
                        ai = new AI("O", "X");
                    } else if (!player) {
                        ai = new AI("X", "O");
                        ai2 = new AI("O", "X");
                    }
                    continue;
                }

                if (!player) Thread.Sleep(400);

                locations[ai.getNextMove(locations)] = ai.symbol;

                if (!player) {
                    Console.Clear();
                    // Writes the game count to the
                    Console.WriteLine($"Game {gameCount}");
                    graph.DrawGridFromArray(locations);
                }

                won = checker.checkWin(locations, "X");
                if (won) {
                    Console.WriteLine($"Game {gameCount}");
                    graph.DrawGridFromArray(locations);
                    if (player) Console.WriteLine("AI Wins!");
                    else Console.WriteLine($"{ai.symbol} AI wins!");
                    break;
                }


                // Check for a tie
                tie = true;
                foreach (string a in locations) {
                    if (a == "-") tie = false;
                }

                if (tie) {
                    Console.WriteLine("Tie!");
                    for (int i = 0; i < 9; i++) locations[i] = "-";
                    gameCount++;

                    if (!player && ai2.symbol == "O") {
                        ai2 = new AI("X", "O");
                        ai = new AI("O", "X");
                    } else if (!player) {
                        ai = new AI("X", "O");
                        ai2 = new AI("O", "X");
                    }
                    continue;
                }

                if (!player) Thread.Sleep(400);


                Console.Clear();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
