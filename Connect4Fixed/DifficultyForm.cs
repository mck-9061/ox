using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Connect4Fixed {
    public partial class DifficultyForm : Form {
        public DifficultyForm() {
            InitializeComponent();
            this.button1.Click += new EventHandler(Button_Click);
        }

        private void Button_Click(object sender, EventArgs e) {
            Form1 form = new Form1(Convert.ToInt32(this.comboBox1.Text));
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.ShowDialog();
        }
    }
}
