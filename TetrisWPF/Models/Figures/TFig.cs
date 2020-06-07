using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Models
{
    public class TFig : Figure
    {
        private static Point[] defaultPositions;

        static TFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
                new Point(1, 0),
            };
        }

        public TFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Yellow, Brushes.Black, 0.7)
        {  }

        public TFig(TFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override Figure Clone()
        {
            return new TFig(this);
        }
    }
}
