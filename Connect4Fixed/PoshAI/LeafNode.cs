using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Fixed.PoshAI {
    class LeafNode : TreeNode {
        public LeafNode(TreeNode parent) {
            this.parent = parent;
        }

        public LeafNode(string[,] board, TreeNode parent) {
            this.parent = parent;
            this.board = board;

            // Obtain node value by observing board
            int xStones = 0;
            int oStones = 0;

            for (int x = 0; x < board.GetLength(0); x++) {
                for (int y = 0; y < board.GetLength(0); y++) {
                    if (board[x, y] == "O") oStones++;
                    else if (board[x, y] == "X") xStones++;
                }
            }

            if (WinChecker.won("X", board, false)) {
                value = 22 - xStones;
            } else if (WinChecker.won("O", board, false)) {
                value = -1 * (22 - oStones);
            } else value = 0;



        }
    }
}
