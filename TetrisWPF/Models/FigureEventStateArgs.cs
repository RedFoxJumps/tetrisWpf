
namespace Models
{
    public class FigureEventStateArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public FigureEventStateArgs()
        { }

        public FigureEventStateArgs(double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}
