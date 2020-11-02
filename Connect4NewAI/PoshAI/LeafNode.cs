using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Fixed.PoshAI {
    class LeafNode : TreeNode {

        public LeafNode(string[,] board, TreeNode parent) {
            //Console.WriteLine("Leaf node made");

            this.parent = parent;
            this.board = board;

            // Obtain node value by observing board
            int xStones = 0;
            int oStones = 0;

            for (int x = 0; x < board.GetLength(0); x++) {
                for (int y = 0; y < board.GetLength(1); y++) {
                    if (board[x, y] == "O") oStones++;
                    else if (board[x, y] == "X") xStones++;
                }
            }

            if (WinChecker.won("X", board, false)) {
                value = 22 - xStones;
            } else if (WinChecker.won("O", board, false)) {
                value = -1 * (22 - oStones);
            } else value = 0;

            //Console.WriteLine(value);

        }
    }
}
