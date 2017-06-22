using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FaceRecog
{
    public partial class Form1 : Form
    {
        Bitmap input;
        Bitmap stage1;
        byte cb_min;
        byte cb_max;
        byte cr_min;
        byte cr_max;

        public Form1()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            cb_min = ByteValueOf(CBmin.Text, 0);
            cb_max = ByteValueOf(CBmax.Text, 255);
            cr_min = ByteValueOf(CRmin.Text, 0);
            cr_max = ByteValueOf(CRmax.Text, 255);

            var path = Environment.GetCommandLineArgs().ElementAtOrDefault(1) ?? "crowd.jpg";
            input = new Bitmap(path);
            stage1 = new Bitmap(input);
            pictureBox1.Image = input;
            pictureBox1.Width = input.Width;
            pictureBox1.Height = input.Height;
            Width = pictureBox1.Right + 10;
            Height = pictureBox1.Bottom + 50;

            var p = new SkinScanner();
            p.Analyse(input, OnPixel);

            pictureBox1.Image = stage1;
        }

        byte ByteValueOf(string text, byte defaultValue)
        {
            byte b;
            if(byte.TryParse(text, out b))
            {
                return b;
            }

            return defaultValue;
        }

        void OnPixel(int x, int y, byte yd, byte cr, byte cb)
        {
            if(IsSkin(cr, cb))
            {
                stage1.SetPixel(x, y, Color.Yellow);
            }
        }

        bool IsSkin(byte cr, byte cb)
        {
            return
                cr >= cr_min && cr <= cr_max && 
                cb >= cb_min && cb <= cb_max ;
        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
            var p = me.Location;

            var s = new SkinScanner();
            var result = s.AnalysePixel(input, p.X, p.Y);
            var cr = result.Item1;
            var cb = result.Item2;
            MessageBox.Show($"cr:{cr} cb:{cb}");
        }
    }
}
