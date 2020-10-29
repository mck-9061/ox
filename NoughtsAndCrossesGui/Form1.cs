using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoughtsAndCrosses;

namespace NoughtsAndCrossesGui {
    public partial class Form1 : Form {
        bool player = true;
        private int buttonClicked = -1;
        public Form1() {
            InitializeComponent();
            _ = Run();
        }


        private async Task Run() {
            Form1 form = this;

            form.resetButton.Visible = false;
            form.resetButton.Click += new EventHandler(resetButton_Click);

            form.button1.Click += new EventHandler(Button_Click);
            form.button2.Click += new EventHandler(Button_Click);
            form.button3.Click += new EventHandler(Button_Click);
            form.button4.Click += new EventHandler(Button_Click);
            form.button5.Click += new EventHandler(Button_Click);
            form.button6.Click += new EventHandler(Button_Click);
            form.button7.Click += new EventHandler(Button_Click);
            form.button8.Click += new EventHandler(Button_Click);
            form.button9.Click += new EventHandler(Button_Click);


            bool won = false;

            Console.WriteLine("Starting game...");
            string[] locations = new string[9];
            for (int i = 0; i < 9; i++) locations[i] = "-";

            var graph = new GridGraphic();
            Console.WriteLine("Loading game...");

            WinChecker checker = new WinChecker();
            BetterAI ai = null; // new AI("X", "O");
            await Task.Delay(200);
            BetterAI ai2 = null; // new AI("O", "X");
            await Task.Delay(200);

            Random r = new Random();
            await Task.Delay(50);
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());
            Console.Write(r.Next());

            int gameCount = 1;

            if (!player) {
                var aaa = r.Next(100);
                if (r.Next() % 2 == 0) {
                    ai = new BetterAI("X", "O");
                    ai2 = new BetterAI("O", "X");
                } else {
                    ai2 = new BetterAI("X", "O");
                    ai = new BetterAI("O", "X");
                }
            } else {
                ai = new BetterAI("X", "O");
            }



            if (!player) while (!won) {
                form.label1.Text = $"Game {gameCount}";
                graph.DrawGridFromArray(locations, form);
                locations[ai2.getNextSquare(locations)] = ai2.character;
                graph.DrawGridFromArray(locations, form);

                won = checker.checkWin(locations, ai2.character);
                if (won) {
                    MessageBox.Show($"{ai2.character} AI wins!");
                    break;
                }

                // Check for a tie
                bool tie = true;
                foreach (string a in locations) {
                    if (a == "-") tie = false;
                }

                if (tie) {
                    Console.WriteLine("Tie!");
                    for (int i = 0; i < 9; i++) locations[i] = "-";
                    gameCount++;

                    if (ai2.character == "O") {
                        ai2 = new BetterAI("X", "O");
                        ai = new BetterAI("O", "X");
                    } else {
                        ai = new BetterAI("X", "O");
                        ai2 = new BetterAI("O", "X");
                    }

                    for (int i = 0; i < 9; i++) {
                        string buttonName = $"button{i + 1}";

                        Control control = form.Controls.Find(buttonName, true)[0];

                        control.BackColor = Color.White;
                    }

                    continue;
                }

                await Task.Delay(400);

                locations[ai.getNextSquare(locations)] = ai.character;

                graph.DrawGridFromArray(locations, form);

                won = checker.checkWin(locations, ai.character);
                if (won) {
                    MessageBox.Show($"{ai.character} AI wins!");
                    break;
                }


                // Check for a tie
                tie = true;
                foreach (string a in locations) {
                    if (a == "-") tie = false;
                }

                if (tie) {
                    Console.WriteLine("Tie!");
                    for (int i = 0; i < 9; i++) locations[i] = "-";
                    gameCount++;

                    if (ai2.character == "O") {
                        ai2 = new BetterAI("X", "O");
                        ai = new BetterAI("O", "X");
                    } else {
                        ai = new BetterAI("X", "O");
                        ai2 = new BetterAI("O", "X");
                    }

                    for (int i = 0; i < 9; i++) {
                        string buttonName = $"button{i + 1}";

                        Control control = form.Controls.Find(buttonName, true)[0];

                        control.BackColor = Color.White;
                    }

                    continue;
                }
                await Task.Delay(400);
            } 

            //form.resetButton.Visible = true;

            if (player) {
                while (!won)
                {
                    form.label1.Text = ($"Game {gameCount}");
                    graph.DrawGridFromArray(locations, form);

                    while (buttonClicked == -1) {
                        await Task.Delay(25);
                    }

                    var index = buttonClicked;
                    buttonClicked = -1;

                    try {
                        if (locations[index] == "-") locations[index] = "O";
                        else {
                            MessageBox.Show("Invalid location, or that location is already full. Move forfeited.");
                        }
                    } catch {
                        Console.WriteLine("Invalid location, or that location is already full. Move forfeited.");
                    }
                    

                    won = checker.checkWin(locations, "O");
                    if (won) {
                        graph.DrawGridFromArray(locations, form);
                        MessageBox.Show("Player wins!");
                        break;
                    }

                    // Check for a tie
                    bool tie = true;
                    foreach (string a in locations) {
                        if (a == "-") tie = false;
                    }

                    if (tie) {
                        for (int i = 0; i < 9; i++) locations[i] = "-";
                        gameCount++;

                        for (int i = 0; i < 9; i++) {
                            string buttonName = $"button{i + 1}";

                            Control control = form.Controls.Find(buttonName, true)[0];

                            control.BackColor = Color.White;
                        }

                        continue;
                    }

                    graph.DrawGridFromArray(locations, form);

                    locations[ai.getNextSquare(locations)] = ai.character;

                    won = checker.checkWin(locations, "X");
                    if (won) {
                        graph.DrawGridFromArray(locations, form);
                        MessageBox.Show("AI Wins!");
                        break;
                    }


                    // Check for a tie
                    tie = true;
                    foreach (string a in locations) {
                        if (a == "-") tie = false;
                    }

                    if (tie) {
                        Console.WriteLine("Tie!");
                        for (int i = 0; i < 9; i++) locations[i] = "-";
                        gameCount++;

                        for (int i = 0; i < 9; i++) {
                            string buttonName = $"button{i + 1}";

                            Control control = form.Controls.Find(buttonName, true)[0];

                            control.BackColor = Color.White;
                        }
                    }
                }
            }
        }


        private void resetButton_Click(object sender, EventArgs e) {
            for (int i = 0; i < 9; i++) {
                string buttonName = $"button{i + 1}";

                Control control = this.Controls.Find(buttonName, true)[0];

                control.BackColor = Color.White;
                control.Text = "-";
            }
            _ = Run();
        }


        private void Button_Click(object sender, EventArgs e) {
            Button button = (Button) sender;

            var name = button.Name;

            buttonClicked = Convert.ToInt32(name.Replace("button", ""))-1;
        }
    }
}
