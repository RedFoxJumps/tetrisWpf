using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Models
{
    public abstract class Figure : IEnumerable, INotifyPropertyChanged
    {
        #region interfaces
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProp(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public IEnumerator GetEnumerator()
        {
            return squares.GetEnumerator();
        }
        #endregion // interfaces

        #region Properties

        public int CX { get; set; }
        public Point Position { get; protected set; }

        public Point[] Points
        {
            get
            {
                Point[] pts = new Point[squares.Length];
                for (int i = 0; i < squares.Length; i++)
                    pts[i] = squares[i].Position.Clone();

                return pts;
            }

            set
            {
                for (int i = 0; i < squares.Length; i++)
                    squares[i].Position = value[i];
            }
        }

        public double SquareSize
        {
            get => squares[0].Size;
            set
            {
                foreach (var r in squares)
                    r.Size = value;
                OnProp("SquareSize");
            }
        }

        public double StrokeThickness
        {
            get => squares[0].StrokeThickness;
            set
            {
                foreach (var r in squares)
                    r.StrokeThickness = value;
                OnProp("StrokeThickness");
            }
        }
        
        public Brush Stroke
        {
            get => squares[0].Stroke;
            set
            {
                foreach (var r in squares)
                    r.Stroke = value;
                OnProp("Stroke");
            }
        }

        public Brush Fill
        {
            get => squares[0].Fill;
            set
            {
                foreach (Square s in squares)
                    s.Fill = value;
                OnProp("Fill");
            }
        }

        public Square this[int i]
        {
            get => squares[i];
            set => squares[i] = value;
        }
        #endregion // properties

        protected Square[] squares;

        public Figure(int numSquares = 4)
        {
            CX = 1;
            squares = new Square[numSquares];
            for (int i = 0; i < squares.Length; i++)
                squares[i] = new Square();
            Position = new Point();
        }

        public Figure(Point[] pts, double squareSize, Brush fillColor, Brush strokeColor, double strokeThicc = 0.0) : this(pts.Length)
        {
            for (int i = 0; i < pts.Length; i++)
                squares[i].Position = pts[i].Clone();

            SquareSize = squareSize;
            Fill = fillColor;
            Stroke = strokeColor;
            StrokeThickness = strokeThicc;
        }

        public Figure(Figure fig) : this(fig.Points, fig.SquareSize, fig.Fill, fig.Stroke, fig.StrokeThickness)
        { }

        public virtual bool TryRotateLeft(GameField field)
        {
            Figure checkSquares = Clone();

            // center of rotation
            Point p = checkSquares[CX].Position;
            double sum = p.Y + p.X;

            for (int i = 0; i < checkSquares.squares.Length; i++)
            {
                if (i == CX)
                    continue;

                Square s = checkSquares[i];
                double ii = s.Position.Y - p.Y;
                s.Position.Y = sum - s.Position.X;
                s.Position.X = p.X + ii;
            }

            if (!checkSquares.TryMove(0, 0, field))
                return false;

            Points = checkSquares.Points;
            
            return true;
        }

        public virtual bool TryRotateRight(GameField field)
        {
            Figure checkSquares = Clone();

            // center of rotation
            Point p = checkSquares[CX].Position;
            double diff = p.Y - p.X;

            for (int i = 0; i < checkSquares.squares.Length; i++)
            {
                if (i == CX)
                    continue;

                Square s = checkSquares[i];
                double ii = s.Position.Y - p.Y;
                s.Position.Y = s.Position.X + diff;
                s.Position.X = p.X - ii;
            }

            if (!checkSquares.TryMove(0, 0, field))
                return false;

            Points = checkSquares.Points;

            return true;
        }

        /// <summary>
        /// Sets figure position in canvas
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(double x, double y)
        {
            foreach (Square s in squares)
            {
                Canvas.SetLeft(s.Rectangle, (s.Position.X + x) * s.Rectangle.Width);
                Canvas.SetTop(s.Rectangle, (s.Position.Y + y) * s.Rectangle.Width);
            }
        }

        public virtual void Update(GameField field)
        {
            if (!TryMove(0, 1, field))
                Stop();
        }

        /// <summary>
        /// Fills canvas with square's Rectangle 
        /// </summary>
        /// <param name="field"></param>
        public void FillField(GameField field)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                double y = squares[i].Y + Position.Y;
                double x = squares[i].X + Position.X;

                field[(int)y, (int)x] = squares[i];
            }
        }

        /// <summary>
        /// Attemts to move figure in given direction
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool TryMove(double dx, double dy, GameField field)
        {
            foreach (Square s in squares)
            {
                ///////////square local pos  figure pos   offset
                double newX = s.Position.X + Position.X + dx;
                double newY = s.Position.Y + Position.Y + dy;

                if (newX >= field.Width || newX < 0 ||
                    newY >= field.Height || newY < 0 || field[(int)newY, (int)newX] != null)
                {
                    return false;
                }
            }

            SetPosition(Position.X += dx, Position.Y += dy);

            return true;
        }

        /// <summary>
        /// Occurs when figure can't fall anymore
        /// </summary>
        public event StoppedDelegate Stopped;
        public delegate void StoppedDelegate(object sender, FigureEventStateArgs args);

        private void Stop()
        {
            Stopped?.Invoke(this, new FigureEventStateArgs(Position.X, Position.Y));
            // make figure a bit darker
        }

        public abstract Figure Clone();

    }
}
