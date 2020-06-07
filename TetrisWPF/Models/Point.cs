using System;

namespace Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() : this(0.0, 0.0)
        { }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X} ,{Y})";
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public static Point operator+ (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
    }
}
