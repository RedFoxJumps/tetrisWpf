using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Models
{
    public class IFig : Figure
    {
        private static Point[] defaultPositions;

        static IFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            };
        }

        public IFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Cyan, Brushes.Black, 0.7)
        { }

        public IFig(IFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override Figure Clone()
        {
            return new IFig(this);
        }
    }
}
