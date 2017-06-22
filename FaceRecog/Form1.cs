using System;
using System.Collections.Generic;
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
        HashSet<Point> SkinPoints = new HashSet<Point>();

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
            SkinPoints = new HashSet<Point>();
            p.Analyse(input, OnPixel);

            pictureBox1.Image = stage1;
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

        void OnPixel(int x, int y, byte yd, byte cr, byte cb)
        {
            if (IsSkin(cr, cb))
            {
                OnSkin(x, y);
            }
        }

        void OnSkin(int x, int y)
        {
            stage1.SetPixel(x, y, Color.Yellow);
            SkinPoints.Add(new Point(x, y));
        }

        bool IsSkin(byte cr, byte cb)
        {
            return
                cr >= cr_min && cr <= cr_max &&
                cb >= cb_min && cb <= cb_max;
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

        void button2_Click(object sender, EventArgs e)
        {
            var s = new Segmenter(SkinPoints);

            var segments = RemoveSmallSegments(s.ScanAll().ToArray());

            foreach (var seg in segments)
            {
                CircleAround(seg, RandomColor());
                pictureBox1.Image = stage1;
            }
        }

        List<Point>[] RemoveSmallSegments(List<Point>[] segments)
        {
            var segmentsWithAreas = segments.Select(s => new { s, area = AreaOf(s) });
            var averageArea = segmentsWithAreas.Average(s => s.area);
            var result = segmentsWithAreas.Where(s => s.area >= averageArea).Select(s => s.s).ToArray();
            return result;
        }

        int AreaOf(List<Point> segment)
        {
            var r = BoundingRect(segment);
            return (r.Width + 1) * (r.Height + 1);
        }

        Color RandomColor()
        {
            return Color.Blue;
        }

        void CircleAround(List<Point> seg, Color c)
        {
            var r = BoundingRect(seg);
            var g = Graphics.FromImage(stage1);
            g.DrawEllipse(new Pen(Color.Blue, r.Width / 10), r);
        }

        static Rectangle BoundingRect(List<Point> seg)
        {
            var minx = seg.Min(p => p.X);
            var maxx = seg.Max(p => p.X);
            var miny = seg.Min(p => p.Y);
            var maxy = seg.Max(p => p.Y);
            var width = maxx - minx + 1;
            var height = maxy - miny + 1;
            var r = new Rectangle(minx, miny, width, height);
            return r;
        }
    }
}
