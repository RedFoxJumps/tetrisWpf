using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Models
{
    public class FigureManager
    {
        protected static Random rand;

        static FigureManager()
        {
            rand = new Random();
        }

        public virtual Figure GenerateRandomFigure(double size = 20)
        {
            switch(rand.Next(7))
            {
                case 0:
                    return new LFig(size);
                case 1:
                    return new JFig(size);
                case 2:
                    return new TFig(size);
                case 3:
                    return new OFig(size);
                case 4:
                    return new IFig(size);
                case 5:
                    return new SFig(size);
                case 6:
                    return new ZFig(size);

                default:
                    return null;
            }
        }
    }
}
