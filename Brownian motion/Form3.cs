using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brownian_motion
{
    public partial class Form3 : Form
    {
        Particle p1 = new Particle();
        Particle p2 = new Particle();
        public Form3()
        {
            InitializeComponent();
        }

        public void drawPoint(int x, int y, PictureBox pictureBox1)
        {

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            Point dPoint = new Point(x, (pictureBox1.Height - y));
            dPoint.X = dPoint.X - 2;
            dPoint.Y = dPoint.Y - 2;
            Rectangle rect = new Rectangle(dPoint, new Size(4, 4));
            g.FillRectangle(brush, rect);
            g.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            p1.SetMaxSpeed = 4;
            p1.X = pictureBox1.Width/2;
            p1.Y = 0;
            p1.X_Speed = 0;
            p1.Y_Speed = 3;
            p2.SetMaxSpeed = 4;
            p2.X = pictureBox1.Width / 2;
            p2.Y = pictureBox1.Height;
            p2.X_Speed = 0;
            p2.Y_Speed = -3;

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Particle[] pr = new Particle[2];
            pr[0] = p1;
            pr[1] = p2;
            p1.Move(pictureBox1.Size);
            p2.Move(pictureBox1.Size);
            p1.repulsionPower(pr);

            drawPoint(Convert.ToInt32(p1.X), Convert.ToInt32(p1.Y), pictureBox1);
            drawPoint(Convert.ToInt32(p2.X), Convert.ToInt32(p2.Y), pictureBox1);

        }
    }
}
