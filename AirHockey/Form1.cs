//Clara Nascu, AirHockey: March 11, 2021
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace AirHockey
{
    public partial class Form1 : Form
    {
        //global integers used...
        int rectangle1X = 0;
        int rectangle1Y = 130;
        int rectangleWidth = 40;
        int rectangleHeight = 80;

        int rectangle2X = 545;
        int rectangle2Y = 130;

        int bumper1X = 100;
        int bumper1Y = 135;
        int player1Score = 0;

        int bumper2X = 430;
        int bumper2Y = 135;
        int player2Score = 0;

        int bumperWidth = 70;
        int bumperHeight = 70;
        int bumperSpeed = 5;

        int ballX = 285;
        int ballY = 160;
        int ballXSpeed = 8;
        int ballYSpeed = 8;
        int ballWidth = 20;
        int ballHeight = 20;

        //Keys used
        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        bool aDown = false;
        bool dDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Font drawFont = new Font("Arial", 20, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(Color.Red);

        Pen blackPen = new Pen(Color.Black, 10);
        Pen redPen = new Pen(Color.Red, 10);
        Pen bluePen = new Pen(Color.Blue, 10);
        Pen alicebluePen = new Pen(Color.AliceBlue, 10);
        Pen whitePen = new Pen(Color.White, 10);
        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1 
            if (wDown == true && bumper1Y > 0)
            {
                bumper1Y -= bumperSpeed;
            }
            if (sDown == true && bumper1Y < this.Height - bumperHeight)
            {
                bumper1Y += bumperSpeed;
            }

            if (aDown == true && bumper1X > 0)
            {
                bumper1X -= bumperSpeed;
            }
            if (dDown == true && bumper1X < this.Width - bumperWidth)
            {
                bumper1X += bumperSpeed;
            }

            //move player 2 
            if (upArrowDown == true && bumper2Y > 0)
            {
                bumper2Y -= bumperSpeed;
            }
            if (downArrowDown == true && bumper2Y < this.Height - bumperHeight)
            {
                bumper2Y += bumperSpeed;
            }

            if (leftArrowDown == true && bumper2X > 0)
            {
                bumper2X -= bumperSpeed;
            }
            if (rightArrowDown == true && bumper2X < this.Width - bumperWidth)
            {
                bumper2X += bumperSpeed;
            }

            //check for ball collision with top/bottom
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(bumper1X, bumper1Y, bumperWidth, bumperHeight);
            Rectangle player2Rec = new Rectangle(bumper2X, bumper2Y, bumperWidth, bumperHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle rectangle1Rec = new Rectangle(rectangle1X, rectangle1Y, rectangleWidth, rectangleHeight);
            Rectangle rectangle2Rec = new Rectangle(rectangle2X, rectangle2Y, rectangleWidth, rectangleHeight);

            if (ballRec.IntersectsWith(rectangle2Rec))
            {
                player1Score++;
                P1ScoreLabel.Text = $"{player1Score}";
                ballX = 295;
                ballY = 195;
                bumper1Y = 135;
                bumper2Y = 135;
                bumper1X = 100;
                bumper2X = 430;
            }
            else if (ballRec.IntersectsWith(rectangle1Rec))
            {
                player2Score++;
                P2ScoreLabel.Text = $"{player2Score}";
                ballX = 295;
                ballY = 195;
            }
            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 
            if (player1Rec.IntersectsWith(ballRec))
            {
                if (ballXSpeed < 0)
                {
                    ballXSpeed *= -1;
                    ballX = bumper1X + bumperWidth + 1;
                }
                else
                {
                    ballXSpeed *= -1;
                    ballX = bumper1X - bumperWidth - 1;
                }

                //(ballHeight x speed variable + Right - left)
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                if (ballXSpeed < 0)
                {
                    ballXSpeed *= -1;
                    ballX = bumper2X + bumperWidth + 1;
                }
                else
                {
                    ballXSpeed *= -1;
                    ballX = bumper2X - bumperWidth - 1;
                }

            }
            //check if either player scores a point //////////////////////
            if (ballX < 0)
            {
                ballXSpeed *= -1;
            }
            else if (ballX > 600)
            {
                P1ScoreLabel.Text = $"{player1Score}";
            }
            if (ballX > this.Width - ballWidth)
            {
                ballXSpeed *= -1;
            }



            //check if either player won (come back here)
            if (player1Score == 3)
            {
                GameTimer.Enabled = false;
                winLabel.Text = "RED WON";
                SoundPlayer Player = new SoundPlayer(Properties.Resources._58425__suonho__sonokids_omni_06);
                Player.Play();
            }
            if (player2Score == 3)
            {
                GameTimer.Enabled = false;
                winLabel.Text = "BLUE WON";
                SoundPlayer Player = new SoundPlayer(Properties.Resources._58425__suonho__sonokids_omni_06);
                Player.Play();
            }
            Refresh();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) 
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //AirHockey design background shapes/lines

            e.Graphics.DrawArc(whitePen, 400, 70, 400, 220, 80, 200);
            e.Graphics.DrawArc(whitePen, -200, 70, 400, 220, 260, 200);
            e.Graphics.DrawLine(alicebluePen, 0, 170, 600, 170);

            //rectangle nets
            e.Graphics.DrawRectangle(blackPen, rectangle1X, rectangle1Y, rectangleWidth, rectangleHeight);
            e.Graphics.FillRectangle(whiteBrush, rectangle1X, rectangle1Y, rectangleWidth, rectangleHeight);

            e.Graphics.DrawRectangle(blackPen, rectangle2X, rectangle2Y, rectangleWidth, rectangleHeight);
            e.Graphics.FillRectangle(whiteBrush, rectangle2X, rectangle2Y, rectangleWidth, rectangleHeight);

            //bumpers
            e.Graphics.DrawEllipse(bluePen, bumper1X, bumper1Y, bumperWidth, bumperHeight);
            e.Graphics.FillEllipse(redBrush, bumper1X, bumper1Y, bumperWidth, bumperHeight);
            e.Graphics.DrawEllipse(redPen, bumper2X, bumper2Y, bumperWidth, bumperHeight);
            e.Graphics.FillEllipse(blueBrush, bumper2X, bumper2Y, bumperWidth, bumperHeight);

            //ball
            e.Graphics.FillRectangle(blackBrush, ballX, ballY, ballWidth, ballHeight);
            //e.Graphics.DrawString(" ", drawFont, drawBrush, 230, 200);
        }
    }
}