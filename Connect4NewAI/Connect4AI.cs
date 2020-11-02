using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4Fixed {
    class Connect4AI {
        public string symbol { get; set; }
        private string otherSymbol;
        public int wins { get; set; } = 0;
        public int losses { get; set; } = 0;
        public int unconclusive { get; set; } = 0;
        public int ties { get; set; } = 0;
        public int simulatedGames { get; set; } = 0;

        public Dictionary<int, double> latestWeights { get; set; }


        public Connect4AI(string symbol, string otherSymbol) {
            this.symbol = symbol;
            this.otherSymbol = otherSymbol;
        }


        public int getNextColumn(string[,] board, int difficulty, ProgressBar progressBar, AIView viewer) {
            // Select the next column for the AI to pick.
            // Based on the MiniMax algorithm.

            Random r = new Random();
            wins = 0;
            losses = 0;
            unconclusive = 0;
            ties = 0;
            simulatedGames = 0;

            Dictionary<int, double> weights = new Dictionary<int, double>();

            List<int> availableColumns = new List<int>();

            for (int i = 0; i < board.GetLength(0); i++) {

                if (board[i, 5].Contains("_")) availableColumns.Add(i+1);
            }

            progressBar.Maximum = (availableColumns.Count);

            foreach (int c in availableColumns) {
                // Simulate all the games that would happen should the AI make this move.
                string[,] workingArray = (string[,]) board.Clone();

                int lowestRow = 6;

                if (workingArray[c - 1, 4] == $"{c}_5") lowestRow = 5;
                if (workingArray[c - 1, 3] == $"{c}_4") lowestRow = 4;
                if (workingArray[c - 1, 2] == $"{c}_3") lowestRow = 3;
                if (workingArray[c - 1, 1] == $"{c}_2") lowestRow = 2;
                if (workingArray[c - 1, 0] == $"{c}_1") lowestRow = 1;

                workingArray[c - 1, lowestRow - 1] = symbol;


                double weight = getWeightOfMove(workingArray, difficulty, viewer);
                weights.Add(c, weight);
                progressBar.PerformStep();
            }

            string message = "Column weights:\n";
            foreach (int column in availableColumns) {
                message += $"{column}: {weights[column]}\n";
            }

            message += $"Losses: {losses}\n";
            message += $"Wins: {wins}\n";

            // MessageBox.Show(message);


            // Find which column has the highest weight
            int bestColumn = availableColumns[r.Next(availableColumns.Count)];
            foreach (int column in availableColumns) {
                if (weights[column] > weights[bestColumn]) bestColumn = column;
            }

            latestWeights = new Dictionary<int, double>(weights);


            return bestColumn;
        }


        private double getWeightOfMove(string[,] currentBoard, int difficulty, AIView viewer) {
            List<string[,]> simulatedBoards = new List<string[,]>();

            double weight = 0;

            bool done = false;
            bool aiTurn = false;

            List<string[,]> nextSimulatedBoards = new List<string[,]>();
            nextSimulatedBoards.Add((string[,]) currentBoard.Clone());


            int iter = 0;
            double exponent = 4;

            while (!done) {
                iter++;
                exponent -= 1;
                simulatedBoards.Clear();

                foreach (string[,] a in nextSimulatedBoards) {
                    simulatedBoards.Add((string[,])a.Clone());
                }

                nextSimulatedBoards.Clear();

                //Parallel.ForEach(simulatedBoards, new ParallelOptions { MaxDegreeOfParallelism = 100 }, (a) => {
                foreach (string[,] a in simulatedBoards) {
                    // Simulate the next move. Add simulated board to list to simulate next if there's no win or tie.
                    List<int> availableColumns = new List<int>();

                    for (int i = 0; i < a.GetLength(0); i++) {
                        if (a[i, 5].Contains("_")) availableColumns.Add(i + 1);
                    }

                    if (availableColumns.Count == 0) {
                        // Tied
                        ties++;
                        continue;
                    }


                    foreach (int c in availableColumns) {
                        simulatedGames++;
                        string[,] workingArray = (string[,]) a.Clone();
                        if (aiTurn) {
                            // Simulate the selection of column C
                            int lowestRow = 6;

                            if (workingArray[c - 1, 4] == $"{c}_5") lowestRow = 5;
                            if (workingArray[c - 1, 3] == $"{c}_4") lowestRow = 4;
                            if (workingArray[c - 1, 2] == $"{c}_3") lowestRow = 3;
                            if (workingArray[c - 1, 1] == $"{c}_2") lowestRow = 2;
                            if (workingArray[c - 1, 0] == $"{c}_1") lowestRow = 1;

                            workingArray[c - 1, lowestRow - 1] = otherSymbol;
                        }
                        else {
                            // Simulate the player's selection of column C
                            int lowestRow = 6;

                            if (workingArray[c - 1, 4] == $"{c}_5") lowestRow = 5;
                            if (workingArray[c - 1, 3] == $"{c}_4") lowestRow = 4;
                            if (workingArray[c - 1, 2] == $"{c}_3") lowestRow = 3;
                            if (workingArray[c - 1, 1] == $"{c}_2") lowestRow = 2;
                            if (workingArray[c - 1, 0] == $"{c}_1") lowestRow = 1;

                            workingArray[c - 1, lowestRow - 1] = symbol;
                        }

                        // Check for a tie
                        bool tie = true;
                        for (int x = 0; x < workingArray.GetLength(0); x++) {
                            for (int y = 0; y < workingArray.GetLength(1); y++) {
                                if (workingArray[x, y] == $"{x + 1}_{y + 1}") tie = false;
                            }
                        }

                        if (tie) {
                            ties++;
                            //Console.WriteLine("Tie simulated");
                            continue;
                        }


                        // Check for win
                        if (WinChecker.won(symbol, workingArray, false)) {
                            // Add weight
                            weight += Math.Pow(10, exponent);
                            wins++;

                            //Console.WriteLine("Win simulated");
                            //Print2DArray(workingArray);
                        }
                        else if (WinChecker.won(otherSymbol, workingArray, false)) {
                            // Remove weight
                            weight -= Math.Pow(1000, exponent);
                            losses++;

                        }
                        else {
                            //bool add = true;
                            //foreach (string[,] board in nextSimulatedBoards) {
                            // Only add if the board isn't already about to be simulated
                            //var equal =
                            //board.Rank == workingArray.Rank &&
                            //Enumerable.Range(0, board.Rank).All(dimension => board.GetLength(dimension) == workingArray.GetLength(dimension)) &&
                            //board.Cast<string>().SequenceEqual(workingArray.Cast<string>());


                            //if (equal) {
                            //add = false;
                            //break;
                            //}
                            //}
                            //if (add) 
                            unconclusive++;
                            nextSimulatedBoards.Add((string[,]) workingArray.Clone());
                        }
                    }
                }//);

                if (nextSimulatedBoards.Count == 0 || iter == difficulty) done = true;
                aiTurn = !aiTurn;

                // Don't bother simulating boards that will inevitably lead to a loss
                if (weight < 0) done = true;
            }
            return weight;
        }


        public static void Print2DArray<T>(T[,] matrix) {
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }


        public double getEmotion() {
            double emotion;
            double deciWins = Convert.ToDouble(wins);
            double deciLosses = Convert.ToDouble(losses);
            if (wins > losses) {
                emotion = ((deciWins - deciLosses) / (deciWins + deciLosses))*10;
            } else if (losses > wins) {
                emotion = ((deciLosses - deciWins) / (deciWins + deciLosses)) * -10;

            } else emotion = 0;

            return emotion;
        }
    }
}
