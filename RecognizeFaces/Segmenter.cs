using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FaceRecog
{
    public class Segmenter
    {
        HashSet<Point> allPoints;
        HashSet<Point> pointsToScan;

        static readonly Point[] Offsets = new Point[]
        {
            new Point(-1, 0),
            new Point(1, 0),
            new Point(0, -1),
            new Point(0, 1)
        };

        public Segmenter(HashSet<Point> points)
        {
            allPoints = points;
        }

        public IEnumerable<List<Point>> ScanAll()
        {
            pointsToScan = new HashSet<Point>(allPoints);

            while(pointsToScan.Any())
            {
                yield return SegmentFrom(pointsToScan.First());
            }
        }

        List<Point> SegmentFrom(Point start)
        {
            var segment = new List<Point>();

            var stack = new Stack<Point>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                Point test = stack.Pop();

                if (pointsToScan.Contains(test))
                {
                    pointsToScan.Remove(test);
                    segment.Add(test);

                    foreach (var offset in Offsets)
                    {
                        stack.Push(AddOffset(test, offset));
                    }
                }
            }

            return segment;
        }

        Point AddOffset(Point current, Point offset)
        {
            return new Point(current.X + offset.X, current.Y + offset.Y);
        }
    }
}