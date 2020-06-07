using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for ResizeWindow.xaml
    /// </summary>
    public partial class ResizeWindow : Window
    {
        public int BlocksWidth => (int)blocksW.Value;
        public int BlocksHeight => (int)blocksH.Value;

        public ResizeWindow(int currentWidth, int currentHeight)
        {
            InitializeComponent();

            blocksW.Value = currentWidth;
            blocksH.Value = currentHeight;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (blocksW.Value is null)
            {
                blocksW.Focus();
                invalidValuePopup.PlacementTarget = blocksW;
                invalidValuePopup.IsOpen = true;
                return;
            }

            if (blocksH.Value is null)
            {
                blocksH.Focus();
                invalidValuePopup.IsOpen = true;
                invalidValuePopup.PlacementTarget = blocksH;
                return;
            }

            DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
