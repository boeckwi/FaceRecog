using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FaceRecog
{
    class Algorithm
    {
        string input_path;
        Bitmap input;
        Bitmap output;
        HashSet<Point> SkinPoints;
        public byte cr_min = 142;
        public byte cr_max = 170;
        public byte cb_min = 103;
        public byte cb_max = 123;

        public Algorithm(string path)
        {
            input_path = path;

        }

        public Bitmap Process()
        {
            input = new Bitmap(input_path);
            output = new Bitmap(input);

            Step1();
            Step2();

            return output;
        }

        void Step1()
        {
            var p = new SkinScanner();
            SkinPoints = new HashSet<Point>();
            p.Analyse(input, OnPixel);
        }

        void Step2()
        {
            var s = new Segmenter(SkinPoints);
            var segments = RemoveSmallSegments(s.ScanAll());
            foreach (var seg in segments)
            {
                CircleAround(seg);
            }
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
            //output.SetPixel(x, y, Color.Yellow);
            SkinPoints.Add(new Point(x, y));
        }

        bool IsSkin(byte cr, byte cb)
        {
            return
                cr >= cr_min && cr <= cr_max &&
                cb >= cb_min && cb <= cb_max;
        }

        List<Point>[] RemoveSmallSegments(IEnumerable<List<Point>> segments)
        {
            var segmentsWithAreas = segments.Select(s => new {s, area = AreaOf(s)}).ToArray();
            var averageArea = segmentsWithAreas.Average(s => s.area);
            var result = segmentsWithAreas.Where(s => s.area >= averageArea).Select(s => s.s).ToArray();
            return result;
        }

        int AreaOf(List<Point> segment)
        {
            var r = BoundingRect(segment);
            return (r.Width + 1) * (r.Height + 1);
        }

        void CircleAround(List<Point> seg)
        {
            var r = BoundingRect(seg);
            var g = Graphics.FromImage(output);
            g.DrawEllipse(new Pen(Color.OrangeRed, r.Width / 10), r);
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
