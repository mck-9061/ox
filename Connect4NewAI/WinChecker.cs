using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Connect4Fixed {
    class WinChecker {
        private static string message;
        public static bool won(string character, string[,] board, bool debug) {
            message = $"Symbol {character} Checked for win on following lines:\n";
            bool won = false;
            for (int x = 0; x < board.GetLength(0); x++) {
                for (int y = 0; y < board.GetLength(1); y++) {
                    string buttonText = board[x, y];

                    if (buttonText == character) {
                        won = checkButton(x + 1, y + 1, board, character, debug);

                        if (won) break;
                    }
                }
                if (won) break;
            }

            if (debug) MessageBox.Show(message);

            return won;
        }


        public static bool checkButton(int column, int row, string[,] board, string character, bool debug) {

            List<List<Dictionary<int, int>>> linesToCheck = new List<List<Dictionary<int, int>>>();

            // Add horizontal lines to check
            List<Dictionary<int, int>> workingList = new List<Dictionary<int, int>>();
            if (column - 3 > 0) {
                workingList.Add(new Dictionary<int, int>() {{column - 3, row}} );

                workingList.Add(new Dictionary<int, int>() {{column - 2, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (column - 2 > 0 && column + 1 < 8) {
                workingList.Add(new Dictionary<int, int>() {{column - 2, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (column - 1 > 0 && column + 2 < 8) {
                workingList.Add(new Dictionary<int, int>() {{column - 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 2, row}} );
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (column + 3 < 8) {
                workingList.Add(new Dictionary<int, int>() {{column + 1, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 2, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 3, row}} );
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }


            // Add vertical lines to check
            if (row - 3 > 0) {
                workingList.Add(new Dictionary<int, int>() {{column, row - 3}});
                workingList.Add(new Dictionary<int, int>() {{column,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (row - 2 > 0 && row + 1 < 7) {
                workingList.Add(new Dictionary<int, int>() {{column,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (row - 1 > 0 && row + 2 < 7) {
                workingList.Add(new Dictionary<int, int>() {{column,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column,row + 1}});
                workingList.Add(new Dictionary<int, int>() {{column,row + 2}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if (row + 3 < 7) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column,row + 1}});
                workingList.Add(new Dictionary<int, int>() {{column,row + 2}});
                workingList.Add(new Dictionary<int, int>() {{column,row + 3}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }


            // Add diagonal lines to check
            if (row - 3 > 0 && column - 3 > 0) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column - 2,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column - 3,row - 3}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row - 2 > 0 && column - 2 > 0) && (row + 1 < 7 && column + 1 < 8)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column - 2,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column + 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row - 1 > 0 && column - 1 > 0) && (row + 2 < 7 && column + 2 < 8)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column + 2,row + 2}});
                workingList.Add(new Dictionary<int, int>() {{column + 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row + 3 < 7 && column + 3 < 8)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 3,row + 3}});
                workingList.Add(new Dictionary<int, int>() {{column + 2,row + 2}});
                workingList.Add(new Dictionary<int, int>() {{column + 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }



            if (row - 3 > 0 && column + 3 < 8) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column + 2,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column + 3,row - 3}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row - 2 > 0 && column + 2 < 8) && (row + 1 < 7 && column - 1 > 0)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column + 2,row - 2}});
                workingList.Add(new Dictionary<int, int>() {{column - 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row - 3 > 0 && column + 3 < 8) && (row + 2 < 7 && column - 2 > 0)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column + 1,row - 1}});
                workingList.Add(new Dictionary<int, int>() {{column - 2,row + 2}});
                workingList.Add(new Dictionary<int, int>() {{column - 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }

            if ((row + 3 < 7 && column - 3 > 0)) {
                workingList.Add(new Dictionary<int, int>() {{column, row}} );
                workingList.Add(new Dictionary<int, int>() {{column - 3,row + 3}});
                workingList.Add(new Dictionary<int, int>() {{column - 2,row + 2}});
                workingList.Add(new Dictionary<int, int>() {{column - 1,row + 1}});
                linesToCheck.Add(new List<Dictionary<int, int>>(workingList));
                workingList = new List<Dictionary<int, int>>();
            }


            bool won = false;
            // Now iterate lists to see if any of them match
            
            foreach (List<Dictionary<int, int>> list in linesToCheck) {
                bool lineWon = true;

                foreach (Dictionary<int, int> dict in list) {
                    string toCheck = board[dict.Keys.ToArray()[0]-1, dict.Values.ToArray()[0]-1];

                    message += $"{dict.Keys.ToArray()[0]}, {dict.Values.ToArray()[0]} + ";

                    if (toCheck != character) lineWon = false;
                }

                message += lineWon + "\n";

                

                if (lineWon) {
                    return true;
                }
            }

            return won;
        }
    }
}
