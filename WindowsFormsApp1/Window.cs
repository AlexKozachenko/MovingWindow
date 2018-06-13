// Close#4
using System;
using System.Windows.Forms;

namespace MovingWindow
{
    public partial class Window : Form
    {
        private const int Offset = 6;
        private int abscissaX;
        private int bottomBorder;
        private Timer[] keys = new Timer[(int)Keyboard.ActiveKeys];
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
                ordinateY += Offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.Y > bottomBorder)
            {
                StartTimer(keys[(int)Keyboard.Up]);
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
                abscissaX -= Offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.X < 0)
            {
                StartTimer(keys[(int)Keyboard.Right]);
            }
        }

        private void KeyRightLogic(object sender, EventArgs e)
        {
            if (Location.X < rightBorder)
            {
                abscissaX += Offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.X > rightBorder)
            {
                StartTimer(keys[(int)Keyboard.Left]);
            }
        }

        private void KeyUpLogic(object sender, EventArgs e)
        {
            if (Location.Y > 0)
            {
                ordinateY -= Offset;
                SetDesktopLocation(abscissaX, ordinateY);
            }
            if (Location.Y < 0)
            {
                StartTimer(keys[(int)Keyboard.Down]);
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
            keys[(int)Keyboard.Down].Tick += new EventHandler(KeyDownLogic);
            keys[(int)Keyboard.Enter].Tick += new EventHandler(KeyEnterLogic);
            keys[(int)Keyboard.Left].Tick += new EventHandler(KeyLeftLogic);
            keys[(int)Keyboard.Right].Tick += new EventHandler(KeyRightLogic);
            keys[(int)Keyboard.Up].Tick += new EventHandler(KeyUpLogic);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ordinateY = Location.Y;
            abscissaX = Location.X;
            switch (e.KeyData)
            {
                case Keys.Down:
                    StartTimer(keys[(int)Keyboard.Down]);
                    break;
                case Keys.Enter:
                    StartTimer(keys[(int)Keyboard.Enter]);
                    break;
                case Keys.Left:
                    StartTimer(keys[(int)Keyboard.Left]);
                    break;
                case Keys.Right:
                    StartTimer(keys[(int)Keyboard.Right]);
                    break;
                case Keys.Up:
                    StartTimer(keys[(int)Keyboard.Up]);
                    break;
            }
        }
    }
}