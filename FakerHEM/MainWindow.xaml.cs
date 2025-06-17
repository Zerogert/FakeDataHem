using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FakeDataHem.FetchData;
using FakerHEM.ViewModels;

namespace FakerHEM
{
    public partial class MainWindow : Window
    {
        private Point? _lastMousePosition;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация ViewModel
            var hemClient = new HemClient(); // Предполагается, что у вас есть реализация HemClient
            DataContext = new MainViewModel(hemClient);
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.Scale += e.Delta > 0 ? 0.5 : -0.5;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (_lastMousePosition is not null)
                {
                    var mousePosition = e.GetPosition(sender as IInputElement);
                    viewModel.OffsetX += (mousePosition.X - _lastMousePosition.Value.X)/10;
                    viewModel.OffsetY -= (mousePosition.Y - _lastMousePosition.Value.Y)/10;
                    _lastMousePosition = mousePosition;
                }                
                return;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _lastMousePosition = e.GetPosition(sender as IInputElement);
        }

        private void Canvas_MouseLeftButtonUpn(object sender, MouseButtonEventArgs e)
        {
            _lastMousePosition = null;
        }
    }
}