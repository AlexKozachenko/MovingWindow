using System;
using System.Windows.Forms;

namespace MovingWindow
{
    public partial class Window : Form
    {
        private const int offset = 6;
        private int abscissaX;
        private int bottomBorder;
        private Timer[] keys = new Timer[(int)Keyboard.activeKeys];
        private int ordinateY;
        private int rightBorder;

        public Window()
        {
            bottomBorder = Screen.PrimaryScreen.Bounds.Height - Height;
            rightBorder = Screen.PrimaryScreen.Bounds.Width - Width;
            InitializeComponent();
            InitializeTimers();
            Subscribe();
        }

        private void InitializeTimers()
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = new Timer(components);
            }
        }

        private void KeyDownLogic(object sender, EventArgs e)
        {
            if (Location.Y < bottomBorder)
            {
                ordinateY += offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.Y > bottomBorder)
            {
                StartTimer(keys[(int)Keyboard.up]);
            }
        }

        private void KeyEnterLogic(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void KeyLeftLogic(object sender, EventArgs e)
        {
            if (Location.X > 0)
            {
                abscissaX -= offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.X < 0)
            {
                StartTimer(keys[(int)Keyboard.right]);
            }
        }

        private void KeyRightLogic(object sender, EventArgs e)
        {
            if (Location.X < rightBorder)
            {
                abscissaX += offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.X > rightBorder)
            {
                StartTimer(keys[(int)Keyboard.left]);
            }
        }

        private void KeyUpLogic(object sender, EventArgs e)
        {
            if (Location.Y > 0)
            {
                ordinateY -= offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.Y < 0)
            {
                StartTimer(keys[(int)Keyboard.down]);
            }
        }

        private void StartTimer(Timer timer)
        {
            foreach (Timer key in keys)
            {
                key.Stop();
            }
            timer.Start();
        }

        private void Subscribe()
        {
            keys[(int)Keyboard.down].Tick += new EventHandler(KeyDownLogic);
            keys[(int)Keyboard.enter].Tick += new EventHandler(KeyEnterLogic);
            keys[(int)Keyboard.left].Tick += new EventHandler(KeyLeftLogic);
            keys[(int)Keyboard.right].Tick += new EventHandler(KeyRightLogic);
            keys[(int)Keyboard.up].Tick += new EventHandler(KeyUpLogic);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ordinateY = Location.Y;
            abscissaX = Location.X;
            switch (e.KeyData)
            {
                case Keys.Down:
                    StartTimer(keys[(int)Keyboard.down]);
                    break;
                case Keys.Enter:
                    StartTimer(keys[(int)Keyboard.enter]);
                    break;
                case Keys.Left:
                    StartTimer(keys[(int)Keyboard.left]);
                    break;
                case Keys.Right:
                    StartTimer(keys[(int)Keyboard.right]);
                    break;
                case Keys.Up:
                    StartTimer(keys[(int)Keyboard.up]);
                    break;
            }
        }
    }
}