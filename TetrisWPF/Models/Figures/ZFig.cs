using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Models
{
    public class ZFig : Figure
    {
        private static Point[] defaultPositions;

        static ZFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1),
                new Point(2, 1),
            };
        }

        public ZFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Magenta, Brushes.Black, 0.7)
        { }

        public ZFig(ZFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override Figure Clone()
        {
            return new ZFig(this);
        }
    }
}
