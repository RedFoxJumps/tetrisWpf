using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Models
{
    public class JFig : Figure
    {
        private static Point[] defaultPositions;

        static JFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
            };
        }

        public JFig(double squareSize = 20) : base(defaultPositions, squareSize, Brushes.Orange, Brushes.Black, 0.7)
        {
            CX = 2;
        }

        public JFig(JFig copy) : base(copy)
        {
            Position = copy.Position.Clone();
        }

        public override Figure Clone()
        {
            return new JFig(this);
        }
    }
}
