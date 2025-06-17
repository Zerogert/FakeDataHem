using HemConverter;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FakeDataHem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _sourceX = 54.9564118;
        private double _sourceY = 82.94582;
        private double _scale = 1000000;

        private double _x = 0;
        private double _y = 0;
        private double _alt = 0;
        private double _az = 0;
        private string _path = @"C:\Users\Zeroget\AppData\Roaming\hem-backend\Settings\fake_data.json";
        private FakeData _fakeData = new FakeData();

        public MainWindow()
        {
            InitializeComponent();
            
            _fakeData = JsonSerializer.Deserialize<FakeData>(File.ReadAllText(_path));
        }
        //56,3414118
        //83,57582
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition((IInputElement)sender);
            _x = pos.X - 10;
            _y = pos.Y - 10;

            Canvas.SetLeft(UnitMark, _x);
            Canvas.SetTop(UnitMark, _y);

            Ltd.Content = GetLtd(_y);
            Lng.Content = GetLng(_x);

            _fakeData.Lat = GetLtd(_y);
            _fakeData.Lng = GetLng(_x);

            SourceLtd.Text = _sourceX.ToString();
            SourceLng.Text = _sourceY.ToString();

            var json = JsonSerializer.Serialize(_fakeData);
            File.WriteAllText(_path, json);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            PathFile.Content = openFileDialog.FileName;
            _path = openFileDialog.FileName;
            _fakeData = JsonSerializer.Deserialize<FakeData>(File.ReadAllText(_path));
        }

        private void Button_Click_Plan_Move(object sender, RoutedEventArgs e)
        {
            var planPointProvider = new PlanPointProvider();
            var planPoint = planPointProvider.GetPlanPoint().ContinueWith(x =>
            {
                Dispatcher.Invoke(() =>
                {
                    SourceLtd.Text = x.Result.X.ToString();
                    SourceLng.Text = x.Result.Y.ToString();
                });
            });
        }

        private double GetLtd(double y)
        {
            return _sourceX - y / _scale + 450 / _scale;
        }

        private double GetLng(double x)
        {
            return _sourceY + x / _scale - 450 / _scale;
        }

        private void AltSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _alt = AltSlider.Value;
            _fakeData.Alt = _alt;
            Alt.Content = _alt;
            var json = JsonSerializer.Serialize(_fakeData);
            File.WriteAllText(_path, json);
        }

        private void SourceLtd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(SourceLng.Text, out var sourceLng))
            {
                _sourceY = sourceLng;
            }
            if (double.TryParse(SourceLtd.Text, out var sourceLtd))
            {
                _sourceX = sourceLtd;
            }
        }

        private void Azimuth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _az = Azimuth.Value;
            _fakeData.Azimuth = _az;
            var json = JsonSerializer.Serialize(_fakeData);
            File.WriteAllText(_path, json);
        }
    }
}