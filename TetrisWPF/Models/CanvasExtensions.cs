using System;
using System.CodeDom;
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
    public static class CanvasExtensions
    {
        public static void RemoveSquare(this Canvas c, Square s)
        {
            c.Children.Remove(s.Rectangle);
        }

        public static void AddFigure(this Canvas c, Figure f)
        {
            foreach (Square square in f)
            {
                c.Children.Add(square.Rectangle);
            }
        }

        public static void RemoveFigure(this Canvas c, Figure f)
        {
            foreach (Square square in f)
            {
                c.Children.Remove(square.Rectangle);
            }
        }
    }
}
