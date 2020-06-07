using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Models
{
    public class GameField
    {
        // canvas to work with
        public Canvas canvas;
        private Square[,] landedSquares;

        public int Width { get => landedSquares.GetUpperBound(1) + 1; }
        public int Height { get => landedSquares.GetUpperBound(0) + 1; }

        public Square this[int y, int x]
        {
            get => landedSquares[y, x];
            set => landedSquares[y, x] = value;
        }

        public GameField(int height, int width, Canvas canv)
        {
            canvas = canv;
            landedSquares = new Square[height, width];
        }

        public bool IsPointAvailable(int x, int y)
        {
            return true;
        }

        public void Update()
        {
            ClearRows();
        }

        // ugly
        private void ClearRows()
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    bool empty = landedSquares[y, x] == null;
                    if (empty)
                        break;

                    // whole row is non-null
                    if (x == Width - 1)
                    {
                        // removing squares from canvas
                        for (int col = 0; col < Width; col++)
                            canvas.RemoveSquare(landedSquares[y, col]);

                        // shift all rows
                        for (int row = y; row > 0; row--)
                        {
                            for (int col = 0; col < Width; col++)
                            {
                                // square[i,j] = figure[i-1,j]
                                landedSquares[row, col] = landedSquares[row - 1, col];
                                if (landedSquares[row, col] != null)
                                {
                                    // update Y component of position so we can update square position in canvas later
                                    landedSquares[row, col].Position.Y = row;
                                    Canvas.SetTop(landedSquares[row, col].Rectangle, row * landedSquares[row, col].Size);
                                }
                            }
                        }

                        // check if one row above is full
                        y++;
                    }
                }
            }
        }
    }
}
