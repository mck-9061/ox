using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NoughtsAndCrossesGui {
    class GridGraphic {
        public GridGraphic() {

        }

        public void DrawGridFromArray(string[] array, Form1 form) {
            for (int i = 0; i < 9; i++) {
                string buttonName = $"button{i + 1}";

                Control button = form.Controls.Find(buttonName, true)[0];

                button.Text = array[i];

                if (button.Text == "X") button.BackColor = Color.Red;
                if (button.Text == "O") button.BackColor = Color.Blue;
            }
        }
    }
}
