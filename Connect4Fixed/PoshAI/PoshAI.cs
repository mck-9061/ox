using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Fixed.PoshAI {
    class PoshAI {
        public int getNextColumn(string[,] currentBoard) {
            TreeNode rootNode = new TreeNode(currentBoard, null, true, 0);
            // start out by looking 4 moves ahead
            rootNode.generateChildren(false, 4);

            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(3)) {
                node.obtainValueFromChildren();
            }
            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(2)) {
                node.obtainValueFromChildren();
            }
            foreach (TreeNode node in rootNode.getAllChildrenAtDepth(1)) {
                node.obtainValueFromChildren();
            }

            rootNode.obtainValueFromChildren();

            return rootNode.bestChildNode.columnPicked;
        }
    }
}
