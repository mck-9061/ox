using System;
using System.Collections.Generic;
using System.Text;

namespace NoughtsAndCrosses {
    public class WinChecker {
        public WinChecker() {

        }

        private static List<List<int>> WinLines() {
            List<List<int>> list = new List<List<int>>();

            list.Add(new List<int> {0, 1, 2});
            list.Add(new List<int> { 3, 4, 5 });
            list.Add(new List<int> { 6, 7, 8 });
            list.Add(new List<int> { 0, 3, 6 });
            list.Add(new List<int> { 1, 4, 7 });
            list.Add(new List<int> { 2, 5, 8 });
            list.Add(new List<int> { 0, 4, 8 });
            list.Add(new List<int> { 2, 4, 6 });

            return list;
        }

        public bool checkWin(string[] values, string check) {
            foreach (var line in WinLines()) {
                if (values[line[0]] == check && values[line[1]] == check && values[line[2]] == check) {
                    return true;
                }
            }


            return false;
        }

        public int nearWinLine(string[] array, string check) {
            // Returns the amount of win lines where 2 spaces contains the character and 1 space contains a blank.
            int lines = 0;
            foreach (var line in WinLines()) {
                if ((array[line[0]] == check && array[line[1]] == check && array[line[2]] == "-")
                    || (array[line[0]] == check && array[line[1]] == "-" && array[line[2]] == check)
                    || (array[line[0]] == "-" && array[line[1]] == check && array[line[2]] == check)) {

                    lines++;
                }
            }
            return lines;
        }
    }
}
