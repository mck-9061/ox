using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NoughtsAndCrosses;

namespace NoughtsAndCrossesGui {
    // An AI class that assigns each number a "weight", then picks the square with the highest weight.
    // It does this by simulating literally every single possible combination ahead and assigning weight based on events.
    // For example, picking a square that would guarantee a win would have a weight of 1000.
    // Picking a square that would guarantee a loss would have a weight of -1000.
    class BetterAI {
        public string character { get; set; }
        public string otherCharacter { get; set; }
        public BetterAI(string character, string otherCharacter) {
            this.character = character;
            this.otherCharacter = otherCharacter;
        }
        public int getNextSquare(string[] array) {
            
            List<int> possibleLocations = new List<int>();
            Dictionary<int, int> weights = new Dictionary<int, int>();
            WinChecker checker = new WinChecker();

            for (int i = 0; i < array.Length; i++) {
                if (array[i] == "-") possibleLocations.Add(i);
            }

            // Hardcoded: Make sure to avoid getting corner trapped
            if (possibleLocations.Count == 8) {
                if (array[0] == otherCharacter) return 3;
                if (array[2] == otherCharacter) return 1;
                if (array[6] == otherCharacter) return 7;
                if (array[8] == otherCharacter) return 5;
            }


            foreach (int i in possibleLocations) {
                int weight = 0;



                string[] workingArray = (string[]) array.Clone();
                workingArray[i] = character;

                // First, check if the AI will win using this space:
                if (checker.checkWin(workingArray, character)) {
                    weight = 1000;
                    weights.Add(i, weight);
                    continue;
                }

                // Then make sure the player won't win on their next turn:
                foreach (var winLine in WinLines())
                {
                    if (workingArray[winLine[0]] == "-" && workingArray[winLine[1]] == otherCharacter && workingArray[winLine[2]] == otherCharacter)
                    {
                        return winLine[0];
                    }
                    else if (workingArray[winLine[0]] == otherCharacter && workingArray[winLine[1]] == "-" && workingArray[winLine[2]] == otherCharacter)
                    {
                        return winLine[1];
                    }
                    else if (workingArray[winLine[0]] == otherCharacter && workingArray[winLine[1]] == otherCharacter && workingArray[winLine[2]] == "-")
                    {
                        return winLine[2];
                    }
                }

                // Then start to simulate every single possible board from this move onwards.
                weight = getGameWeight(workingArray);
                weights.Add(i, weight);
            }

            // DEBUG: Position weights
            string message = $"Position weights:\n";
            foreach (int space in weights.Keys) {
                message = message + $"Position {space}: {weights[space]}\n";
            }

            //MessageBox.Show(message);

            // Then find the space with the highest weight.
            int highestWeightSpace = possibleLocations[0];

            foreach (int space in weights.Keys) {
                if (weights[space] > weights[highestWeightSpace]) highestWeightSpace = space;
            }

            return highestWeightSpace;
        }


        public int getGameWeight(string[] array) {
            List<string[]> simulatedBoards = new List<string[]>();
            simulatedBoards.Add((string[]) array.Clone());

            // Return the weight of a game's array by simulating all possible games from said array.
            int weight = 0;

            // This is a list of all the boards that this game could finish on, whether it be win, loss, or tie.
            List<string[]> allEndBoards = new List<string[]>();

            // Generate this list:
            bool done = false;
            bool aiTurn = false;
            List<string[]> nextSimulatedBoards = new List<string[]>();

            foreach (string[] a in simulatedBoards) {
                nextSimulatedBoards.Add((string[])a.Clone());
            }

            int iter = 0;

            while (!done) {
                iter++;

                simulatedBoards.Clear();

                foreach (string[] a in nextSimulatedBoards) {
                    simulatedBoards.Add((string[]) a.Clone());
                }

                nextSimulatedBoards.Clear();
                foreach (string[] a in simulatedBoards) {
                    // Simulate the next move:
                    List<int> possibleSpaces = new List<int>();

                    for (int i = 0; i < a.Length; i++) {
                        if (a[i] == "-") possibleSpaces.Add(i);
                    }

                    // Failsafe
                    if (possibleSpaces.Count == 0) {
                        allEndBoards.Add((string[]) a.Clone());
                        continue;
                    }

                    foreach (int space in possibleSpaces) {
                        string[] workingArray = (string[]) a.Clone();
                        if (aiTurn) {
                            workingArray[space] = character;
                        }
                        else {
                            workingArray[space] = otherCharacter;
                        }

                        // Check for tie
                        bool tie = true;
                        foreach (string s in workingArray) if (s != "-") tie = false;

                        if (tie) allEndBoards.Add(workingArray);
                        else {
                            // Check for win
                            WinChecker checker = new WinChecker();
                            if (checker.checkWin(workingArray, character)) allEndBoards.Add(workingArray);
                            else if (checker.checkWin(workingArray, otherCharacter)) allEndBoards.Add(workingArray);
                            else nextSimulatedBoards.Add(workingArray);
                        }
                        // Finally, adjust weight if the board contains a trap
                        WinChecker checker3 = new WinChecker();

                        if (checker3.nearWinLine(workingArray, character) > 1) weight += 1000;
                        if (checker3.nearWinLine(workingArray, otherCharacter) > 1) weight -= 1000;
                    }
                }

                if (nextSimulatedBoards.Count == 0) done = true;
                aiTurn = !aiTurn;
            }

            // All boards have now been simulated.
            // Time to calculate the weight!
            // Ties cause no weight change.
            // Wins increase weight by 10.
            // Losses decrease weight by 10.
            WinChecker checker2 = new WinChecker();


            foreach (string[] a in allEndBoards) {
                if (checker2.checkWin(a, character)) weight += 10;
                else if (checker2.checkWin(a, otherCharacter)) weight -= 10;
            }



            return weight;
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
