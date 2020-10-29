using System;
using System.Collections.Generic;
using System.Text;

namespace NoughtsAndCrosses {
    public class AI {
        public string symbol { get; set; }
        private string oppositeSymbol;
        private Random r;
        public AI(string symbol, string oppositeSymbol) {
            this.symbol = symbol;
            this.oppositeSymbol = oppositeSymbol;
            this.r = new Random();

            // Flush random
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());
            //Console.Clear();
        }

        public int getNextMove(string[] locations) {
            // This AI path is very defensive and doesn't try to get a double win line on the player. It goes for defense where possible and only wins if a line is available.
            // Ties a lot with most players.
            return getWin(locations);
        }



        private int randomCell(string[] locations) {
            // Basic AI: Random cell
            List<int> possibleIndex = new List<int>();

            int iterator = 0;

            foreach (var i in locations) {
                if (i == "-") {
                    possibleIndex.Add(iterator);
                }

                iterator++;
            }

            return possibleIndex[r.Next(possibleIndex.Count)];
        }


        private int blockWins(string[] locations) {
            // More sophisticated AI: Block the player from winning where possible
            foreach (var winLine in WinLines()) {
                if (locations[winLine[0]] == "-" && locations[winLine[1]] == oppositeSymbol && locations[winLine[2]] == oppositeSymbol) {
                    return winLine[0];
                } else if (locations[winLine[0]] == oppositeSymbol && locations[winLine[1]] == "-" && locations[winLine[2]] == oppositeSymbol) {
                    return winLine[1];
                } else if (locations[winLine[0]] == oppositeSymbol && locations[winLine[1]] == oppositeSymbol && locations[winLine[2]] == "-") {
                    return winLine[2];
                }
            }


            return randomCell(locations);
        }

        private int getWin(string[] locations) {
            // More sophisticated AI: Get a win if a line is open
            foreach (var winLine in WinLines()) {
                if (locations[winLine[0]] == "-" && locations[winLine[1]] == symbol && locations[winLine[2]] == symbol) {
                    return winLine[0];
                } else if (locations[winLine[0]] == symbol && locations[winLine[1]] == "-" && locations[winLine[2]] == symbol) {
                    return winLine[1];
                } else if (locations[winLine[0]] == symbol && locations[winLine[1]] == symbol && locations[winLine[2]] == "-") {
                    return winLine[2];
                }
            }


            return preventDouble(locations);
        }

        private int preventDouble(string[] locations) {
            // Advanced AI: Apply a set of pre-determined rules to prevent a double win line from the player
            // Rule 1: Get the center cell if it's available
            if (r.Next(3) != 1 && locations[4] == "-") return 4;


            // Rule 2: If player places in center on turn 1, place in a corner, not an edge

            bool rule2 = true;
            int iterator = 0;
            foreach (var a in locations) {
                if (iterator != 4 && a != "-") rule2 = false;
                iterator++;
            }

            if (locations[4] != oppositeSymbol) rule2 = false;


            if (rule2) return 0;

            return blockWins(locations);
        }






        private static List<List<int>> WinLines() {
            List<List<int>> list = new List<List<int>>();

            list.Add(new List<int> { 0, 1, 2 });
            list.Add(new List<int> { 3, 4, 5 });
            list.Add(new List<int> { 6, 7, 8 });
            list.Add(new List<int> { 0, 3, 6 });
            list.Add(new List<int> { 1, 4, 7 });
            list.Add(new List<int> { 2, 5, 8 });
            list.Add(new List<int> { 0, 4, 8 });
            list.Add(new List<int> { 2, 4, 6 });

            return list;
        }
    }
}
