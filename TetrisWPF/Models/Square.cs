using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Models
{
    public class Square
    {

        public double X { get => Position.X; set => Position.X = value; }
        public double Y { get => Position.Y; set => Position.Y = value; }

        public Point Position { get; set; }
        /// <summary>
        /// Width and height of a square
        /// </summary>
        public double Size 
        {
            get => Rectangle.Width;
            set
            {
                Rectangle.Width = Rectangle.Height = value;
            }
        }

        public double StrokeThickness
        {
            get => Rectangle.StrokeThickness;
            set
            {
                Rectangle.StrokeThickness = value;
            }
        }
        public Brush Stroke
        {
            get => Rectangle.Stroke;
            set
            {
                Rectangle.Stroke = value;
            }
        }
        public Brush Fill
        {
            get => Rectangle.Fill;
            set
            {
                Rectangle.Fill = value;
            }
        }
        public Rectangle Rectangle { get; set; }

        public Square()
        {
            Rectangle = new Rectangle();
            Position = new Point();
        }

        public Square(Point pos, double size, Brush fill, Brush stroke, double strokeThicc) : this()
        {
            Position = pos;
            Size = size;
            Fill = fill;
            Stroke = stroke;
            StrokeThickness = strokeThicc;
        }
    }
}
