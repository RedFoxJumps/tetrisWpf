using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Models
{
    public class LFig : Figure
    {
        private static Point[] defaultPositions;

        static LFig()
        {
            defaultPositions = new Point[]
            {
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
                new Point(2, 0),
            };
        }

        public LFig() : this(20)
        { }

        /// <summary>
        /// Creates new L-shape Figure with default squares position
        /// </summary>
        /// <param name="squareSize"></param>
        public LFig(double squareSize) : base(defaultPositions, squareSize, Brushes.Red, Brushes.Black, 0.7)
        {  }

        public LFig(LFig fig) : base(fig)
        {
            Position = fig.Position.Clone();
            Fig = fig;
        }

        public LFig Fig { get; }

        public override Figure Clone()
        {
            return new LFig(this);
        }

    }
}
