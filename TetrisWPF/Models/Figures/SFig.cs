using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Models
{
    public class SFig : Figure
    {
        private static Point[] defaultPositions;

        static SFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 1),
                new Point(1, 1),
                new Point(1, 0),
                new Point(2, 0),
            };
        }

        public SFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Green, Brushes.Black, 0.7)
        { }

        public SFig(SFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override Figure Clone()
        {
            return new SFig(this);
        }
    }
}
