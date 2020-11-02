using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Connect4Fixed {
    class GuiDrawer {
        public static void drawGrid(Form1 form, string[,] board) {
            for (int x = 0; x < board.GetLength(0); x += 1) {
                for (int y = 0; y < board.GetLength(1); y += 1) {
                    string buttonName = $"button{x+1}_{y+1}";
                    Button button = (Button) form.Controls.Find(buttonName, true)[0];

                    button.Text = board[x, y];

                    if (button.Text == "O") button.BackColor = Color.Red;
                    if (button.Text == "X") button.BackColor = Color.Blue;
                }
            }
        }


        public static void drawAiGrid(AIView form, string[,] board) {
            for (int x = 0; x < board.GetLength(0); x += 1) {
                for (int y = 0; y < board.GetLength(1); y += 1) {
                    string buttonName = $"button{x + 1}_{y + 1}";
                    Button button = (Button)form.Controls.Find(buttonName, true)[0];

                    button.Text = board[x, y];

                    if (button.Text == "O") button.BackColor = Color.Red;
                    if (button.Text == "X") button.BackColor = Color.Blue;
                }
            }
        }
    }
}
