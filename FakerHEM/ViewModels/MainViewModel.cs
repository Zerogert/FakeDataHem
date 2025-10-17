using FakeDataHem.FetchData;
using FakerHEM.Helpers;
using FakerHEM.Models.Responses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FakerHEM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly HemClient _hemClient;
        private ObservableCollection<PlanViewModel> _plans = [];
        private double _scale = 1.0;
        private double _offsetX = 0;
        private double _offsetY = 0;

        public ObservableCollection<PlanViewModel> Plans
        {
            get => _plans;
            set { _plans = value; OnPropertyChanged(nameof(Plans)); }
        }

        private CircleViewModel _circle;

        public CircleViewModel Circle
        {
            get => _circle;
            set { _circle = value; OnPropertyChanged(nameof(Circle)); }
        }

        public double Scale
        {
            get => _scale;
            set { _scale = value;
                UpdateAllScaledCoordinates();
                OnPropertyChanged(nameof(Scale)); }
        }

        public double OffsetX
        {
            get => _offsetX;
            set { _offsetX = value;
                UpdateAllScaledCoordinates();
                UpdateFakeData();
                OnPropertyChanged(nameof(OffsetX)); }
        }

        private void UpdateFakeData()
        {
            Task.Run(async () => {
                var x = (_circle.X / Scale - OffsetX);
                var y = (-_circle.Y / Scale - OffsetY);
                var fakeDate = await _hemClient.GetFakeDataAsync();
                var point = new PointDto(x, y, 722);
                var pointProvider = new PlanPointProvider();
                var coord = await pointProvider.GetPoint(point);
                fakeDate.Lat = coord.X;
                fakeDate.Lng = coord.Y;
                await _hemClient.UpdateFakeDataAsync(fakeDate);
            });
        }

        public double OffsetY
        {
            get => _offsetY;
            set { _offsetY = value;
                UpdateAllScaledCoordinates();
                OnPropertyChanged(nameof(OffsetY)); }
        }

        public ICommand LoadProjectCommand { get; }

        public MainViewModel(HemClient hemClient)
        {
            _hemClient = hemClient;
            LoadProjectCommand = new RelayCommand(async () => await LoadProjectAsync());
            double canvasWidth = 1200;
            double canvasHeight = 800;
            Circle = new CircleViewModel { X = canvasWidth/2, Y = canvasHeight/2 };
        }

        private async Task LoadProjectAsync()
        {
            var project = await _hemClient.GetActiveProjectAsync();
            if (project?.Plans != null)
            {
                Plans = new ObservableCollection<PlanViewModel>(
                    project.Plans.Select(plan => new PlanViewModel
                    {
                        Name = plan.Name,
                        IsVisible = true,
                        ShowUpper = true,
                        ShowLower = true,
                        UpperColor = new SolidColorBrush(GetRandomColor()),
                        LowerColor = new SolidColorBrush(GetRandomColor()),
                        UpperPoints = new ObservableCollection<PointViewModel>(plan.Contour.Select(p => new PointViewModel { X = p.X, Y = p.Y })),
                        LowerPoints = new ObservableCollection<PointViewModel>(plan.Contour.Select(p => new PointViewModel { X = p.X, Y = p.Y }))
                    })
                );

                CenterAndScaleFirstPlan();
            }
        }

        private void UpdateAllScaledCoordinates()
        {
            foreach (var plan in Plans)
            {
                plan.UpdateScaledCoordinates(Scale, OffsetX, OffsetY);
            }
            OnPropertyChanged(nameof(Plans));
        }

        private void CenterAndScaleFirstPlan()
        {
            if (Plans.Any())
            {
                var firstPlan = Plans.FirstOrDefault(x=>x.UpperPoints.Count>1);
                var minX = firstPlan.UpperPoints.Min(p => p.X);
                var maxX = firstPlan.UpperPoints.Max(p => p.X);
                var minY = firstPlan.UpperPoints.Min(p => p.Y);
                var maxY = firstPlan.UpperPoints.Max(p => p.Y);

                double canvasWidth = 1200;
                double canvasHeight = 800;

                double scaleX = 20;
                double scaleY = 20;
                Scale = Math.Min(scaleX, scaleY);

                //OffsetX = -minX * Scale + (canvasWidth - (maxX - minX) * Scale) / 2;
                //OffsetY = -minY * Scale + (canvasHeight - (maxY - minY) * Scale) / 2;
                OffsetX = -minX;
                OffsetY = -minY;
            }
        }

        private Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}