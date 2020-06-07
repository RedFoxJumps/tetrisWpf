using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Models
{
    public class OFig : Figure
    {
        private static Point[] defaultPositions;

        static OFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1),
            };
        }

        public OFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Blue, Brushes.Black, 0.7)
        { }

        public OFig(OFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override bool TryRotateRight(GameField field)
        {
            return true;
        }

        public override bool TryRotateLeft(GameField field)
        {
            return true;
        }

        public override Figure Clone()
        {
            return new OFig(this);
        }
    }
}
