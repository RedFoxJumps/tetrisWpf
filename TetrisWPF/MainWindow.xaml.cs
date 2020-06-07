using System;
using System.Collections.Generic;
using System.ComponentModel;
using Win = System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using System.Windows;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProp(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        // field variables
        private int blocksWidth;
        private int WidthBlocks
        {
            get => blocksWidth;
            set
            {
                blocksWidth = value;
                OnProp("WidthBlocks");
            }
        }
        
        private int blocksHeight;
        private int HeightBlocks
        {
            get => blocksHeight;
            set
            {
                blocksHeight = value;
                OnProp("HeightBlocks");
            }
        }
        
        private double squareSize;
        private double SquareSize
        {
            get => squareSize;
            set
            {
                squareSize = value;
                OnProp("SquareSize");
            }
        }

        // time handle
        private const double timeBeforeUpdate = 0.8;
        private const double speedUpFallSpeed = 0.05;
        private double variableTimeBeforeUpdate;
        private double timeSinceUpdate;
        private double currentTime;
        private double prevTime;

        // game state
        public double fps;
        public double FPS
        {
            get => fps;
            set
            {
                fps = value;
                OnProp("FPS");
            }
        }

        private bool isPaused;
        private bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                isPausedBtn.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        // figure data
        private double dx, dy;
        private Figure nextFigure;
        private Figure currentFigure;
        private FigureManager figureManager;

        // game field
        private GameField field;

        public MainWindow()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            SquareSize = 20;
            WidthBlocks = 10;
            HeightBlocks = 15;
            figureManager = new FigureManager();
            StartNew(HeightBlocks, WidthBlocks, SquareSize);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            currentTime = (e as RenderingEventArgs).RenderingTime.TotalSeconds;
            double deltaTime = currentTime - prevTime;
            prevTime = currentTime;

            if (!IsPaused)
            {
                Update(deltaTime);
            }
        }

        private void Update(double deltaTime)
        {
            currentFigure.TryMove(dx, dy, field);
            dx = dy = 0;

            timeSinceUpdate += deltaTime;
            if (timeSinceUpdate < variableTimeBeforeUpdate)
                return;

            FPS = 1 / deltaTime;
            timeSinceUpdate = 0;

            currentFigure.Update(field);
        }

        /// <summary>
        /// Starts new game
        /// </summary>
        private void StartNew(int heightBlocks, int widthBlocks, double blockSize)
        {
            mainCanvas.Children.Clear();
            nextFigureCanvas.Children.Clear();
            currentFigure = null;
            nextFigure = null;

            field = new GameField(heightBlocks, widthBlocks, mainCanvas);
            SetCanvasSize(widthBlocks * blockSize, heightBlocks * blockSize);

            // reset time
            variableTimeBeforeUpdate = timeBeforeUpdate;
            timeSinceUpdate = 0;
            currentTime = 0;
            prevTime = 0;

            nextFigure = figureManager.GenerateRandomFigure(SquareSize);
            nextFigure.Stopped += FigureStopped;
            SwapFigures();
        }

        private void _PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // reset fall time
            if (e.Key == Key.S)
                variableTimeBeforeUpdate = timeBeforeUpdate;
        }

        private void _PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.P || e.Key == Key.Space)
            {
                IsPaused = !IsPaused;
            }
            
            // movement
            if (IsPaused)
                return;

            if (e.Key == Key.A)
                dx = -1;
            
            if (e.Key == Key.D)
                dx = 1;
            
            if (e.Key == Key.E)
                currentFigure.TryRotateRight(field);
            if (e.Key == Key.Q)
                currentFigure.TryRotateLeft(field);

            if (e.Key == Key.S)
                variableTimeBeforeUpdate = speedUpFallSpeed;
        }

        private void StartNewClick(object sender, RoutedEventArgs e)
        {
            IsPaused = true;
            if (RestartMessage() == MessageBoxResult.Yes)
            {
                StartNew(HeightBlocks, WidthBlocks, SquareSize);
            }
            IsPaused = false;
        }

        private MessageBoxResult RestartMessage()
        {
            return MessageBox.Show("Запуск новой игры приведет к потере прогресса текущей. Продолжить?",
                   "Аттеншн!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        }

        private void SwapFigures()
        {
            // remove next figure from nextFigureCanvas
            nextFigureCanvas.Children.Clear();
            mainCanvas.AddFigure(nextFigure);
            currentFigure = nextFigure;
            if (currentFigure.TryMove(WidthBlocks / 2, 0, field) == false)
            {
                GameOver();
            }

            nextFigure = figureManager.GenerateRandomFigure();
            nextFigureCanvas.AddFigure(nextFigure);
            nextFigure.SetPosition(1, 1);
            nextFigure.Stopped += FigureStopped;
        }

        private void FigureStopped(object sender, FigureEventStateArgs args)
        {
            currentFigure.FillField(field);
            field.Update();
            
            SwapFigures();
        }

        private void SetCanvasSize(double widthBlocks, double heightBlocks)
        {
            grid.ColumnDefinitions[0].Width = new GridLength(widthBlocks, GridUnitType.Pixel);
            Height = heightBlocks + 40;
            ////////////////////// 40 because of title bar minimal hight  
        }

        private void GameOver()
        {
            if (MessageBox.Show("Хотите начать новую?", "Игра окончена.", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                StartNew(HeightBlocks, WidthBlocks, SquareSize);
            else
                Close();
        }

        private void isPausedBtn_Click(object sender, RoutedEventArgs e)
        {
            IsPaused = false;
            isPausedBtn.Visibility = Visibility.Hidden;
        }

        private void ResizeButtonClick(object sender, RoutedEventArgs e)
        {
            IsPaused = true;
            var win = new ResizeWindow(blocksWidth, blocksHeight);
            if (win.ShowDialog() == true && RestartMessage() == MessageBoxResult.Yes)
            {
                WidthBlocks = win.BlocksWidth;
                HeightBlocks = win.BlocksHeight;
                StartNew(HeightBlocks, WidthBlocks, SquareSize);
            }

            IsPaused = false;
        }
    }
}
