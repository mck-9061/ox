using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connect4Fixed;

namespace Connect4Fixed {
    public partial class AIView : Form {
        
        public AIView() {
            InitializeComponent();
            _ = Run();
        }

        private async Task Run() {

            
        }

        private void Button_Click(object sender, EventArgs e) {
            Button button = (Button) sender;

            var name = button.Name;

        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
}
