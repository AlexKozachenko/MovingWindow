using System;
using System.Windows.Forms;

namespace MovingWindow
{
    public partial class Window : Form
    {
        private int abscissaX;
        private Timer keyDown;
        private Timer keyEnter;
        private Timer keyLeft;
        private Timer keyRight;
        private Timer keyUp;
        private int ordinateY;
        
        public Window()
        {
            InitializeComponent();
            keyDown = new Timer(components);
            keyEnter = new Timer(components);
            keyLeft = new Timer(components);
            keyRight = new Timer(components);
            keyUp = new Timer(components);
            keyDown.Tick += new EventHandler(KeyDown_Tick);
            keyEnter.Tick += new EventHandler(KeyEnter_Tick);
            keyLeft.Tick += new EventHandler(KeyLeft_Tick);
            keyRight.Tick += new EventHandler(KeyRight_Tick);
            keyUp.Tick += new EventHandler(KeyUp_Tick);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ordinateY = Location.Y;
            abscissaX = Location.X;
            switch (e.KeyData)
            { 
                case Keys.Down:
                    StartTimer(keyDown);
                    break;
                case Keys.Enter:
                    StartTimer(keyEnter);
                    break;
                case Keys.Left:
                    StartTimer(keyLeft);
                    break;
                case Keys.Right:
                    StartTimer(keyRight);
                    break;
                case Keys.Up:
                    StartTimer(keyUp);
                    break;
            }
        }

        private void KeyDown_Tick(object sender, EventArgs e)
        {
            ordinateY += 6;
            SetDesktopLocation(abscissaX, ordinateY);
            if (Location.Y > Screen.PrimaryScreen.Bounds.Height - Height)
            {
                StartTimer(keyUp);
            }
        }

        private void KeyEnter_Tick(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void KeyLeft_Tick(object sender, EventArgs e)
        {
            abscissaX -= 6;
            SetDesktopLocation(abscissaX, ordinateY);
            if (Location.X < 0)
            {
                StartTimer(keyRight);
            }
        }

        private void KeyRight_Tick(object sender, EventArgs e)
        {
            abscissaX += 6;
            SetDesktopLocation(abscissaX, ordinateY);
            if (Location.X > Screen.PrimaryScreen.Bounds.Width - Width)
            {
                StartTimer(keyLeft);
            }
        }

        private void KeyUp_Tick(object sender, EventArgs e)
        {
            ordinateY -= 6;
            SetDesktopLocation(abscissaX, ordinateY);
            if (Location.Y < 0)
            {
                StartTimer(keyDown);
            }
        }

        private void StartTimer(Timer timer)
        {
            keyDown.Stop();
            keyEnter.Stop();
            keyLeft.Stop();
            keyRight.Stop();
            keyUp.Stop();
            timer.Start();
        }
    }
}