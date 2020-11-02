using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Fixed.PoshAI {
    class TreeNode {
        public int value { get; set; }
        public TreeNode parent { get; set; }
        public List<TreeNode> children { get; set; }
        public string[,] board { get; set; }
        public bool isMaximiser;
        public TreeNode bestChildNode;
        public int columnPicked { get; set; }


        public TreeNode(string[,] board, TreeNode parent, bool isMaximiser, int columnPicked) {
            this.parent = parent;
            this.board = board;
            this.isMaximiser = isMaximiser;
            this.columnPicked = columnPicked;

            this.value = 0;
        }

        public TreeNode() {

        }


        public void generateChildren(bool onlyLeaves, int recursive) {
            children = new List<TreeNode>();
            // Generates the children TreeNodes/LeafNodes of the current game board.
            List<int> availableColumns = new List<int>();

            for (int i = 0; i < board.GetLength(0); i++) {
                if (board[i, 5].Contains("_")) availableColumns.Add(i + 1);
            }


            foreach (int c in availableColumns) {
                string[,] workingArray = (string[,]) board.Clone();

                int lowestRow = 6;

                if (workingArray[c - 1, 4] == $"{c}_5") lowestRow = 5;
                if (workingArray[c - 1, 3] == $"{c}_4") lowestRow = 4;
                if (workingArray[c - 1, 2] == $"{c}_3") lowestRow = 3;
                if (workingArray[c - 1, 1] == $"{c}_2") lowestRow = 2;
                if (workingArray[c - 1, 0] == $"{c}_1") lowestRow = 1;

                if (isMaximiser) workingArray[c - 1, lowestRow - 1] = "X";
                else workingArray[c - 1, lowestRow - 1] = "O";

                // Check for tie
                bool tie = true;
                for (int x = 0; x < workingArray.GetLength(0); x++) {
                    for (int y = 0; y < workingArray.GetLength(0); y++) {
                        if ((workingArray[x, y] != "O") || (workingArray[x, y] != "X")) {
                            tie = false;
                            break;
                        }
                    }

                    if (!tie) break;
                }

                if (tie || WinChecker.won("X", workingArray, false) || WinChecker.won("O", workingArray, false)) {
                    LeafNode node = new LeafNode((string[,]) workingArray.Clone(), this);
                    children.Add(node);
                }
                else if (!onlyLeaves) {
                    children.Add(new TreeNode((string[,]) workingArray.Clone(), this, !isMaximiser, c));
                }
            }

            if (recursive == 1) foreach (TreeNode child in children) child.generateChildren(true, 0);
            if (recursive > 1) foreach (TreeNode child in children) child.generateChildren(false, recursive - 1);
        }

        public void obtainValueFromChildren(bool debug) {
            if (isMaximiser) {
                // This node is a maximiser and as such will take the highest value from its children.
                int v = -100;
                foreach (TreeNode node in children) {
                    if (node.value > v) {
                        v = node.value;
                        bestChildNode = node;
                    }

                    if (debug) Console.WriteLine($"{node.value}");
                }

                if (children.Count == 0) v = 0;

                this.value = v;
            }
            else {
                // This node is a minimiser and as such will take the lowest value from its children.
                int v = 100;
                foreach (TreeNode node in children) {
                    if (node.value < v) {
                        v = node.value;
                        bestChildNode = node;
                    }

                    if (debug) Console.WriteLine($"{node.value}");
                }

                if (children.Count == 0) v = 0;

                Console.WriteLine(v);
                this.value = v;
            }
        }


        public List<TreeNode> getAllChildrenAtDepth(int depth) {
            List<TreeNode> children = new List<TreeNode>(this.children);
            List<TreeNode> nextChildren = new List<TreeNode>();

            for (int i = 0; i < depth; i++) {
                foreach (TreeNode child in children) {
                    foreach (TreeNode child2 in child.children) {
                        nextChildren.Add(child2);
                    }
                }

                children = new List<TreeNode>(nextChildren);
                nextChildren.Clear();
            }

            return children;
        }
    }
}
