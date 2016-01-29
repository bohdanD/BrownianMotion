using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Brownian_motion
{
    public partial class Form1 : Form
    {

        Particle p1 = new Particle();
        Particle p2 = new Particle();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        Particle[] p;
        ParticleWithCoord pc;
        double[] xCoor;
        double[] yCoor;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            try {
                if (Convert.ToInt32(textBox1.Text) == 0)
                    throw new ArgumentException();
                p = new Particle[Convert.ToInt32(textBox1.Text)-1];
            }
            catch
            {
                MessageBox.Show("Перевірте введені данні", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                textBox1.ResetText();
                
                return;
            }
            for (int i = 0; i < p.Length; i++)
                p[i] = new Particle();
            pc = new ParticleWithCoord();


            int midle_speed = Convert.ToInt32(Math.Sqrt(3 * 0.013 * trackBar1.Value));

            for (int j = 0; j < p.Length; j++)
            {
                p[j].SetMaxSpeed = midle_speed+1;
                p[j].X = rnd.Next(pictureBox1.Width);
                p[j].Y = rnd.Next(pictureBox1.Height);
                p[j].X_Speed = rnd.Next(-midle_speed, midle_speed);
                p[j].Y_Speed = rnd.Next(-midle_speed, midle_speed);

                drawPoint(Convert.ToInt32(p[j].X), Convert.ToInt32(p[j].Y), this.pictureBox1);
               
            }
            pc.SetMaxSpeed = midle_speed + 1;
            pc.X = rnd.Next(700);
            pc.Y = rnd.Next(350);
            pc.X_Speed = rnd.Next(-midle_speed, midle_speed);
            pc.Y_Speed = rnd.Next(-midle_speed, midle_speed);

            drawPoint(Convert.ToInt32(pc.X), Convert.ToInt32(pc.Y), this.pictureBox1);
            button2.Enabled = true;

            // 
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
        public void drawBrownianPoint(int x, int y)
        {

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            Pen pen = new Pen(Color.Blue, 3);
            g.DrawEllipse(pen, x, y, 40, 40);
            g.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            for (int i=0; i<p.Length; i++)
            {
                p[i].Move(pictureBox1.Size);
                p[i].repulsionPower(p);
               
            }
            pc.Move(pictureBox1.Size);
            pc.repulsionPower(p);

            for(int j=0; j<p.Length; j++)
                drawPoint(Convert.ToInt32(p[j].X), Convert.ToInt32(p[j].Y), pictureBox1);
            drawPoint(Convert.ToInt32(pc.X), Convert.ToInt32(pc.Y), pictureBox1);
            drawPoint(Convert.ToInt32(pc.X), Convert.ToInt32(pc.Y), pictureBox2);

            //=================
            label6.Text = "Час моделювання:\n" + pc.ActionTime+" мс";
            //==================


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label6.Text = "";
            button2.Enabled = false;
           
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
            chart1.Visible = false;
            textBox1.Text = "100";
            trackBar1.Value = 300;
            label3.Text = "300";
            //=====================
           
            //==============================
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button1.Enabled = false;
            button3.Visible = true;
            p1.SetMaxSpeed = 4;
            p1.X = pictureBox1.Width / 2;
            p1.Y = 0;
            p1.X_Speed = 0;
            p1.Y_Speed = 3;
            p2.SetMaxSpeed = 4;
            p2.X = pictureBox1.Width / 2;
            p2.Y = pictureBox1.Height;
            p2.X_Speed = 0;
            p2.Y_Speed = -3;
            //=====================
           
            timer2.Tick += Timer2_Tick;
            timer2.Interval = 50;
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Equals(tabPage2))
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }else
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
                chart1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("За весь час");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            xCoor = pc.X_array;
            yCoor = pc.Y_array;
            chart1.Visible = true;
            for(int i=0; i<pc.X_array.Length-1; i++)
            chart1.Series[0].Points.AddXY(pc.X_array[i], pc.Y_array[i]);
            label7.Visible = true;
            label8.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try {
                chart1.Series.Clear();
                chart1.Series.Add("За вибраний час");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                int border;
               // chart1.Series[0].XAxisType.
                border = (int)int.Parse(textBox2.Text) / 50;
                xCoor = pc.X_array;
                yCoor = pc.Y_array;
                for (int i = 0; i < border; i++)
                    chart1.Series[0].Points.AddXY(xCoor[i], yCoor[i]);

                label7.Visible = true;
                label8.Visible = true;
            }
            catch
            {
                MessageBox.Show("Перевірте введені данні", "Помилка",  MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text Files | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(save.FileName, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(file, Encoding.ASCII);
                for (int i = 0; i < pc.X_array.Length; i++)
                {
                    writer.WriteLine(pc.X_array[i] + "\t" +
                        pc.Y_array[i]);
                }
                writer.Close();
                file.Close();
                MessageBox.Show("Данні записано в текстовий файл", "Інформація",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chart1_ClientSizeChanged(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int j = 0; j < p.Length; j++)
            {
                p[j].X = rnd.Next(pictureBox1.Width);
                p[j].Y = rnd.Next(pictureBox1.Height);
            }
            pc.X = rnd.Next(pictureBox1.Width);
            pc.Y = rnd.Next(pictureBox1.Height);
        }

        private void openTestForm()
        {
            Application.Run(new Form3());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            button3.Visible = false;
            button1.Enabled = true;
            pictureBox1.Refresh();
        }
    }
}
