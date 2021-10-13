using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Number_classificatorML.Model;

namespace number_classificator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawingSuface = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawGraph = Graphics.FromImage(DrawingSuface);
            pictureBox1.BackgroundImage = DrawingSuface;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
        }

        bool isPressed = false;
        Bitmap DrawingSuface;
        Graphics DrawGraph;
        Point CurrentPoint;
        Point PreviousPoint;

        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = label1;
            ModelInput input = new ModelInput()
            {
                ImageSource = "image.png"
            };

            ModelOutput output = ConsumeModel.Predict(input);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PreviousPoint = CurrentPoint;
                CurrentPoint = e.Location;
                Paint();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                bmp.Save("image.png", ImageFormat.Png);
            }



            //prediction(image.png);
            ModelInput input = new ModelInput()
            {
                ImageSource = "image.png"
            };

            ModelOutput output = ConsumeModel.Predict(input);

            label0.Text += GetScore(output.Score[0]) + "%";
            label1.Text += GetScore(output.Score[1]) + "%";
            label2.Text += GetScore(output.Score[2]) + "%";
            label3.Text += GetScore(output.Score[3]) + "%";
            label4.Text += GetScore(output.Score[4]) + "%";
            label5.Text += GetScore(output.Score[5]) + "%";
            label6.Text += GetScore(output.Score[6]) + "%";
            label7.Text += GetScore(output.Score[7]) + "%";
            label8.Text += GetScore(output.Score[8]) + "%";
            label9.Text += GetScore(output.Score[9]) + "%";

            //here we fill labels

            ActiveControl = label0;
            ResultLabel.Text += Environment.NewLine + output.Prediction;
            ResultLabel.Visible = true;
            ShowReslutls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveControl = label0;
            DrawGraph.Clear(Color.White);
            RefreshScreen();
        }

        void Paint()
        {
            Pen p = new Pen(Color.Black, 1);
            DrawGraph.FillRectangle(Brushes.Black, new Rectangle(new Point(CurrentPoint.X - 7, CurrentPoint.Y - 7), new Size(14, 14)));
            DrawGraph.DrawRectangle(p, new Rectangle(new Point(CurrentPoint.X - 7, CurrentPoint.Y - 7), new Size(14, 14)));
            pictureBox1.Invalidate();
        }

        void RefreshScreen()
        {
            pictureBox1.Refresh();
            ResultLabel.Text = "This number must be";
            ResultLabel.Visible = false;
            label0.Text = "0 ";
            label1.Text = "1 ";
            label2.Text = "2 ";
            label3.Text = "3 ";
            label4.Text = "4 ";
            label5.Text = "5 ";
            label6.Text = "6 ";
            label7.Text = "7 ";
            label8.Text = "8 ";
            label9.Text = "9 ";
            label0.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
        }

        void ShowReslutls()
        {
            label0.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
        }

        string GetScore(float score)
        {
            if (score < 0.001) return "< 0.1";
            return "= " + Math.Round((100 * score), 2).ToString();
        }
    }
}
