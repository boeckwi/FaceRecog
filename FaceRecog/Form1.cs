using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FaceRecog
{
    public partial class Form1 : Form
    {
        HashSet<Point> SkinPoints = new HashSet<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            var path = Environment.GetCommandLineArgs().ElementAtOrDefault(1) ?? "crowd.jpg";

            var f = new Algorithm(path);

            f.cb_min = ByteValueOf(CBmin.Text, 0);
            f.cb_max = ByteValueOf(CBmax.Text, 255);
            f.cr_min = ByteValueOf(CRmin.Text, 0);
            f.cr_max = ByteValueOf(CRmax.Text, 255);

            var output = f.Process();
            pictureBox1.Image = output;
            pictureBox1.Width = output.Width;
            pictureBox1.Height = output.Height;
            Width = pictureBox1.Right + 10;
            Height = pictureBox1.Bottom + 50;
        }

        byte ByteValueOf(string text, byte defaultValue)
        {
            byte b;
            if (byte.TryParse(text, out b))
            {
                return b;
            }

            return defaultValue;
        }

    }
}
