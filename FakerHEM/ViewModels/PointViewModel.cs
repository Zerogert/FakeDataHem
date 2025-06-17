using System.ComponentModel;

namespace FakerHEM.ViewModels
{
    public class PointViewModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public double ScaledX { get; private set; }
        public double ScaledY { get; private set; }

        public void UpdateScaledCoordinates(double scale, double offsetX, double offsetY)
        {
            ScaledX = (offsetX + X) * scale;
            ScaledY = -(offsetY + Y) * scale;
            OnPropertyChanged(nameof(ScaledX));
            OnPropertyChanged(nameof(ScaledY));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}