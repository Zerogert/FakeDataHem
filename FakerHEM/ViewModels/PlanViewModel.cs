using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace FakerHEM.ViewModels
{
    public class PlanViewModel : INotifyPropertyChanged
    {
        private bool _isVisible;
        private bool _showUpper;
        private bool _showLower;
        private Brush _upperColor;
        private Brush _lowerColor;

        public string Name { get; set; }
        public ObservableCollection<PointViewModel> UpperPoints { get; set; }
        public ObservableCollection<PointViewModel> LowerPoints { get; set; }

        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        public bool ShowUpper
        {
            get => _showUpper;
            set { _showUpper = value; OnPropertyChanged(nameof(ShowUpper)); }
        }

        public bool ShowLower
        {
            get => _showLower;
            set { _showLower = value; OnPropertyChanged(nameof(ShowLower)); }
        }

        public Brush UpperColor
        {
            get => _upperColor;
            set { _upperColor = value; OnPropertyChanged(nameof(UpperColor)); }
        }

        public Brush LowerColor
        {
            get => _lowerColor;
            set { _lowerColor = value; OnPropertyChanged(nameof(LowerColor)); }
        }

        public void UpdateScaledCoordinates(double scale, double offsetX, double offsetY)
        {
            foreach (var point in UpperPoints)
            {
                point.UpdateScaledCoordinates(scale, offsetX, offsetY);
            }

            foreach (var point in LowerPoints)
            {
                point.UpdateScaledCoordinates(scale, offsetX, offsetY);
            }

            OnPropertyChanged(nameof(LowerPoints));
            OnPropertyChanged(nameof(UpperPoints));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}