using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Properties;

namespace WindowsFormsApp2
{
    public partial class XO_Game : Form
    {
        public XO_Game()
        {
            InitializeComponent();
        }


        enum enPlayerTurn {Player1 ,Player2 };

        enPlayerTurn PlayerTurn= enPlayerTurn.Player1;

        enum enWinner {player1,player2,Draw,GameInProgress};

        struct stGameStatus
        {
         public   enWinner Winner;
          public  bool GameOver;
          public  byte PlayCount;
        }

        stGameStatus GameStatus;

      
        private void XO_Game_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255);

            Pen pen = new Pen(white);

            pen.Width = 5;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 350, 100, 350, 400);
            e.Graphics.DrawLine(pen, 475, 100, 475, 400);
            e.Graphics.DrawLine(pen, 250, 200, 575, 200);
            e.Graphics.DrawLine(pen, 250, 300, 575, 300);


        }

        void EndGame()
        {

            labPlayerTurn.Text = "Game Over";

                switch (GameStatus.Winner)
                { 
                    case enWinner.player1:

                        labGameWinner.Text = "Player 1";

                        break;
                    case enWinner.player2:

                        labGameWinner.Text = "Player 2";

                        break;
                    case enWinner.Draw:

                        labGameWinner.Text = "Draw";
                        break;

                }

                MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            bool CheckValues(Button btn1, Button btn2, Button btn3)
            {
                if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn2.Tag.ToString() == btn3.Tag.ToString())
                {

                btn1.BackColor = Color.GreenYellow; 
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                    if (btn1.Tag.ToString() == "X")
                    {

                        GameStatus.Winner = enWinner.player1;
                        GameStatus.GameOver = true;
                        EndGame();
                    return true;
                    }
                    else
                    {

                        GameStatus.Winner = enWinner.player2;
                        GameStatus.GameOver = true;
                        EndGame();
                    return true;
                    }



                }
                GameStatus.GameOver = false;
                return false;
            }


            void CheckWinner()
            {
                //checked rows
                //check Row1
                if (CheckValues(button1, button2, button3))
                    return;

                //check Row2
                if (CheckValues(button4, button5, button6))
                    return;

                //check Row3
                if (CheckValues(button7, button8, button9))
                    return;

                //checked cols
                //check col1
                if (CheckValues(button1, button4, button7))
                    return;

                //check col2
                if (CheckValues(button2, button5, button8))
                    return;

                //check col3
                if (CheckValues(button3, button6, button9))
                    return;

                //check Diagonal

                //check Diagonal1
                if (CheckValues(button1, button5, button9))
                    return;

                //check Diagonal2
                if (CheckValues(button3, button5, button7))
                    return;



            }

            void ChangeImage(Button btn)
            {

                if (btn.Tag.ToString() == "?" && !GameStatus.GameOver)
                {
                    switch (PlayerTurn)
                    {
                        case enPlayerTurn.Player1:

                            btn.Image = Resources.X;
                            btn.Tag = "X";
                            PlayerTurn = enPlayerTurn.Player2;
                            labPlayerTurn.Text = "Player 2";
                            GameStatus.PlayCount++;
                          CheckWinner();
                            break;
                        case enPlayerTurn.Player2:

                            btn.Image = Resources.O;
                            btn.Tag = "O";
                            PlayerTurn = enPlayerTurn.Player1;
                            labPlayerTurn.Text = "Player 1";
                            GameStatus.PlayCount++;
                        CheckWinner();

                            break;
                    }
                }
                else
                {

                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                if(GameStatus.PlayCount == 9)
            {

                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();

            }

            }
        
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        private void RestButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;

        }
        private void RestartGame()
        {

            RestButton(button1);
            RestButton(button2);
            RestButton(button3);
            RestButton(button4);
            RestButton(button5);
            RestButton(button6);
            RestButton(button7);
            RestButton(button8);
            RestButton(button9);

            PlayerTurn = enPlayerTurn.Player1;
            labPlayerTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            labGameWinner.Text = "In Progress";



        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
