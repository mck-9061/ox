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
    public partial class Form1 : Form {
        private int buttonClicked = -1;
        private int difficulty;
        private bool player = true;
        public Form1(int difficulty) {
            InitializeComponent();
            this.difficulty = difficulty;
            _ = Run();
        }

        private async Task Run() {
            AIView viewer = new AIView();

            //viewer.Show();

            string[,] board;
            Form1 form = this;

            board = new string[7, 6];

            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 6; j++) {
                    board[i, j] = $"{i+1}_{j+1}";
                }
            }

            if (player) {
                // Add event handlers to buttons
                form.button1.Click += new EventHandler(Button_Click);
                form.button2.Click += new EventHandler(Button_Click);
                form.button3.Click += new EventHandler(Button_Click);
                form.button4.Click += new EventHandler(Button_Click);
                form.button5.Click += new EventHandler(Button_Click);
                form.button6.Click += new EventHandler(Button_Click);
                form.button7.Click += new EventHandler(Button_Click);

                form.buttonNerd.Click += new EventHandler(buttonNerd_Click);


                GuiDrawer.drawGrid(form, board);

                //Connect4AI ai = new Connect4AI("X", "O");
                PoshAI.PoshAI ai = new PoshAI.PoshAI();

                bool done = false;
                while (!done) {
                    // Wait for user input
                    while (buttonClicked == -1) await Task.Delay(25);
                    int column = buttonClicked;
                    buttonClicked = -1;

                    Button lowestColumnButton = null;

                    string baseButtonName = $"button{column}_";

                    if (form.Controls.Find(baseButtonName + "1", true)[0].Text == $"{column}_1")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "1", true)[0];

                    else if (form.Controls.Find(baseButtonName + "2", true)[0].Text == $"{column}_2")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "2", true)[0];

                    else if (form.Controls.Find(baseButtonName + "3", true)[0].Text == $"{column}_3")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "3", true)[0];

                    else if (form.Controls.Find(baseButtonName + "4", true)[0].Text == $"{column}_4")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "4", true)[0];

                    else if (form.Controls.Find(baseButtonName + "5", true)[0].Text == $"{column}_5")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "5", true)[0];

                    else if (form.Controls.Find(baseButtonName + "6", true)[0].Text == $"{column}_6")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "6", true)[0];


                    else {
                        MessageBox.Show("That column is full. Move forfeited.");
                    }



                    if (lowestColumnButton != null) {
                        int row = Convert.ToInt32(lowestColumnButton.Name.Split("_")[1]);

                        board[column - 1, row - 1] = "O";


                        // Check if player has won
                        if (WinChecker.won("O", board, false)) {
                            GuiDrawer.drawGrid(form, board);
                            MessageBox.Show("Red wins!");
                            Console.WriteLine("Luck.");
                            done = true;
                            break;
                        }
                    }



                    GuiDrawer.drawGrid(form, board);

                    form.label2.Text = "Waiting for AI...";
                    await Task.Delay(100);


                    // AI

                    column = ai.getNextColumn(board);

                    lowestColumnButton = null;

                    baseButtonName = $"button{column}_";

                    if (form.Controls.Find(baseButtonName + "1", true)[0].Text == $"{column}_1")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "1", true)[0];

                    else if (form.Controls.Find(baseButtonName + "2", true)[0].Text == $"{column}_2")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "2", true)[0];

                    else if (form.Controls.Find(baseButtonName + "3", true)[0].Text == $"{column}_3")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "3", true)[0];

                    else if (form.Controls.Find(baseButtonName + "4", true)[0].Text == $"{column}_4")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "4", true)[0];

                    else if (form.Controls.Find(baseButtonName + "5", true)[0].Text == $"{column}_5")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "5", true)[0];

                    else if (form.Controls.Find(baseButtonName + "6", true)[0].Text == $"{column}_6")
                        lowestColumnButton = (Button) form.Controls.Find(baseButtonName + "6", true)[0];



                    if (lowestColumnButton != null) {
                        int row = Convert.ToInt32(lowestColumnButton.Name.Split("_")[1]);

                        board[column - 1, row - 1] = "X";
                        GuiDrawer.drawGrid(form, board);

                        // Check if AI has won
                        if (WinChecker.won("X", board, false)) {
                            MessageBox.Show("Blue wins!");
                            Console.WriteLine("I win again! :)");
                            done = true;
                            break;
                        }
                    }

                    // AI Stats
                    Label label = form.label2;

                    //label2.Text = $"Games simulated: {ai.simulatedGames}\n";
                    //label2.Text += $"AI wins in {ai.wins} of these games.\n";
                    //label2.Text += $"AI loses in {ai.losses} of these games.\n";
                    //label2.Text += $"{ai.unconclusive} of these games are unconclusive.\n";
                    //label2.Text += $"Players tie in {ai.ties} of these games.\n";

                    Label nerd = form.label3;
                    nerd.Text = "Latest column weights:\n";

                    //foreach (int c in ai.latestWeights.Keys) {
                        //nerd.Text += $"Column {c}: Weight {ai.latestWeights[c]}\n";
                    //}

                    form.progressBar1.Value = 0;


                    // Give the AI a bit of personality
                    Console.ForegroundColor = ConsoleColor.Green;
                    double emotion = 0; //ai.getEmotion();

                    if (emotion == 10) Console.WriteLine("I've already won in every game I've simulated. Better start making better moves.");
                    else if (emotion == -10) Console.WriteLine("Grrrrr.");
                    else if (emotion > 8) Console.WriteLine("Give up.");
                    else if (emotion > 6) Console.WriteLine("You know I'm beating you, right?");
                    //else if (emotion > 4) Console.WriteLine($"Did you know that in {ai.wins} realities I will beat you within the next {difficulty} moves? This will be one if you're not careful.");
                    else if (emotion > 0) Console.WriteLine("You've fallen into my trap. Or have you? :)");
                    else if (emotion == 0) Console.WriteLine("We're even. For now.");
                    else if (emotion > -4) Console.WriteLine("You think you're so smart.");
                    else if (emotion > -7) Console.WriteLine("I can still win this.");
                    else if (emotion > -10) Console.WriteLine("I can see more moves ahead than your human brain. I'm not falling for that.");



                    label2.Text += "The AI is currently ";
                    if (emotion == 10) label2.Text += "Extremely Joyful";
                    else if (emotion > 9) label2.Text += "Very Joyful";
                    else if (emotion > 8) label2.Text += "Joyful";
                    else if (emotion > 7) label2.Text += "Very Happy";
                    else if (emotion > 6) label2.Text += "Happy";
                    else if (emotion > 5) label2.Text += "Cocky";
                    else if (emotion > 4) label2.Text += "Smirking";
                    else if (emotion > 3) label2.Text += "Nonchalant";
                    else if (emotion > 2) label2.Text += "Passive";
                    else if (emotion > 1) label2.Text += "Passive";
                    else if (emotion > 0) label2.Text += "Passive";
                    else if (emotion > -1) label2.Text += "Passive";
                    else if (emotion > -2) label2.Text += "Thinking Slightly";
                    else if (emotion > -3) label2.Text += "Thinking";
                    else if (emotion > -4) label2.Text += "Thinking Intensely";
                    else if (emotion > -5) label2.Text += "Stressed";
                    else if (emotion > -6) label2.Text += "Slightly Annoyed";
                    else if (emotion > -7) label2.Text += "Annoyed";
                    else if (emotion > -8) label2.Text += "Angry";
                    else if (emotion > -9) label2.Text += "Very Angry";
                    else if (emotion >= -10) label2.Text += "Baby Raging";
                }
            }
            else {
                // Have 2 AIs fight each other!


                form.buttonNerd.Click += new EventHandler(buttonNerd_Click);


                GuiDrawer.drawGrid(form, board);

                Connect4AI ai = new Connect4AI("X", "O");
                Connect4AI ai2 = new Connect4AI("O", "X");

                bool done = false;
                while (!done) {
                    int column = ai2.getNextColumn(board, difficulty, form.progressBar1, viewer);

                    Button lowestColumnButton = null;

                    string baseButtonName = $"button{column}_";

                    if (form.Controls.Find(baseButtonName + "1", true)[0].Text == $"{column}_1")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "1", true)[0];

                    else if (form.Controls.Find(baseButtonName + "2", true)[0].Text == $"{column}_2")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "2", true)[0];

                    else if (form.Controls.Find(baseButtonName + "3", true)[0].Text == $"{column}_3")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "3", true)[0];

                    else if (form.Controls.Find(baseButtonName + "4", true)[0].Text == $"{column}_4")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "4", true)[0];

                    else if (form.Controls.Find(baseButtonName + "5", true)[0].Text == $"{column}_5")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "5", true)[0];

                    else if (form.Controls.Find(baseButtonName + "6", true)[0].Text == $"{column}_6")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "6", true)[0];



                    if (lowestColumnButton != null) {
                        int row = Convert.ToInt32(lowestColumnButton.Name.Split("_")[1]);

                        board[column - 1, row - 1] = "O";


                        // Check if player has won
                        if (WinChecker.won("O", board, false)) {
                            GuiDrawer.drawGrid(form, board);
                            MessageBox.Show("Red wins!");
                            done = true;
                            break;
                        }
                    }



                    GuiDrawer.drawGrid(form, board);

                    form.label2.Text = "Waiting for AI...";
                    await Task.Delay(1000);


                    // AI

                    column = ai.getNextColumn(board, difficulty, form.progressBar1, viewer);

                    lowestColumnButton = null;

                    baseButtonName = $"button{column}_";

                    if (form.Controls.Find(baseButtonName + "1", true)[0].Text == $"{column}_1")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "1", true)[0];

                    else if (form.Controls.Find(baseButtonName + "2", true)[0].Text == $"{column}_2")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "2", true)[0];

                    else if (form.Controls.Find(baseButtonName + "3", true)[0].Text == $"{column}_3")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "3", true)[0];

                    else if (form.Controls.Find(baseButtonName + "4", true)[0].Text == $"{column}_4")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "4", true)[0];

                    else if (form.Controls.Find(baseButtonName + "5", true)[0].Text == $"{column}_5")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "5", true)[0];

                    else if (form.Controls.Find(baseButtonName + "6", true)[0].Text == $"{column}_6")
                        lowestColumnButton = (Button)form.Controls.Find(baseButtonName + "6", true)[0];



                    if (lowestColumnButton != null) {
                        int row = Convert.ToInt32(lowestColumnButton.Name.Split("_")[1]);

                        board[column - 1, row - 1] = "X";
                        GuiDrawer.drawGrid(form, board);

                        // Check if AI has won
                        if (WinChecker.won("X", board, false)) {
                            MessageBox.Show("Blue wins!");
                            done = true;
                            break;
                        }
                    }

                    // AI Stats
                    Label label = form.label2;

                    label2.Text = $"Games simulated: {ai.simulatedGames}\n";
                    label2.Text += $"AI wins in {ai.wins} of these games.\n";
                    label2.Text += $"AI loses in {ai.losses} of these games.\n";
                    label2.Text += $"{ai.unconclusive} of these games are unconclusive.\n";
                    label2.Text += $"Players tie in {ai.ties} of these games.\n";

                    Label nerd = form.label3;
                    nerd.Text = "Latest column weights:\n";

                    foreach (int c in ai.latestWeights.Keys) {
                        nerd.Text += $"Column {c}: Weight {ai.latestWeights[c]}\n";
                    }
                }

            }
        }

        private void Button_Click(object sender, EventArgs e) {
            Button button = (Button) sender;

            var name = button.Name;

            buttonClicked = Convert.ToInt32(name.Replace("button", ""));
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void buttonNerd_Click(object sender, EventArgs e) {
            this.label3.Visible = !this.label3.Visible;
        }
    }
}
