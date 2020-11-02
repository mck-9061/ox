using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Fixed.PoshAI {
    class PoshAI {
        public int getNextColumn(string[,] currentBoard) {
            TreeNode rootNode = new TreeNode(currentBoard, null, true, 1);
            // start out by looking 4 moves ahead
            rootNode.generateChildren(false, 4);

            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(3)) {
                node.obtainValueFromChildren(false);
                //Console.WriteLine(node.value);
            }
            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(2)) {
                node.obtainValueFromChildren(false);
                //Console.WriteLine(node.value);
            }
            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(1)) {
                node.obtainValueFromChildren(false);
                //Console.WriteLine(node.value);
            }

            rootNode.obtainValueFromChildren(false);
            //Console.WriteLine(rootNode.value);

            return rootNode.bestChildNode.columnPicked;
        }
    }
}
