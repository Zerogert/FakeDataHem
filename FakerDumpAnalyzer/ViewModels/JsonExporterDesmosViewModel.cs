using FakerDumpAnalyzer.Helpers;
using FakerDumpAnalyzer.Models;
using FakerDumpAnalyzer.Services;
using FakerHEM.Helpers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Input;

namespace FakerDumpAnalyzer.ViewModels
{
    public class JsonExporterDesmosViewModel : BaseViewModel
    {

        private string content;
        private double minX;
        private double minY;
        private double minZ;
        private double maxX;
        private double maxY;
        private double maxZ;

        public DPoint Offset => new DPoint(minX, minY, minZ);
        public ICommand ExportPolygons { get; private set; }
        public ICommand ExportPoints { get; private set; }
        public ICommand ExportPolyline { get; private set; }

        public string JsonContent{ get; set; }
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public double MinX
        {
            get => minX;
            set
            {
                minX = value;
                OnPropertyChanged(nameof(MinX));
            }
        }
        public double MinY
        {
            get => minY;
            set
            {
                minY = value;
                OnPropertyChanged(nameof(MinY));
            }
        }
        public double MinZ
        {
            get => minZ;
            set
            {
                minZ = value;
                OnPropertyChanged(nameof(MinZ));
            }
        }
        public double MaxX
        {
            get => maxX;
            set
            {
                maxX = value;
                OnPropertyChanged(nameof(MaxX));
            }
        }
        public double MaxY
        {
            get => maxY;
            set
            {
                maxY = value;
                OnPropertyChanged(nameof(MaxY));
            }
        }
        public double MaxZ
        {
            get => maxZ;
            set
            {
                maxZ = value;
                OnPropertyChanged(nameof(MaxZ));
            }
        }

        public JsonExporterDesmosViewModel()
        {
            ExportPolygons = new RelayCommand(ExportPolygonsExecute);
            ExportPoints = new RelayCommand(ExportPointsExecute);
            ExportPolyline = new RelayCommand(ExportPolylineExecute);
        }

        private void ExportPolylineExecute()
        {
            var points = JsonSerializer.Deserialize<List<DPoint>>(JsonContent, DefaultSerializerOptions.GetOptions()) ?? [];
            UpdateOffset(points);

            if (points?.DefaultIfEmpty() is null) return;

            points = points.Select(x => x - Offset).ToList();
            Content = DesmosExporter.ExportPolyline(points, "T");
        }

        private void ExportPointsExecute()
        {
            var points = JsonSerializer.Deserialize<List<DPoint>>(JsonContent, DefaultSerializerOptions.GetOptions()) ?? [];
            UpdateOffset(points);

            if (points?.DefaultIfEmpty() is null) return;
            points = points.Select(x => x - Offset).ToList();
            Content = DesmosExporter.ExportPoints(points, "T");
        }

        private void ExportPolygonsExecute()
        {
            var polygons = JsonSerializer.Deserialize<List<DPolygon>>(JsonContent, DefaultSerializerOptions.GetOptions()) ?? [];
            UpdateOffset(polygons.SelectMany(x => x.GetPoints()));

            if (polygons?.DefaultIfEmpty() is null) return;

            var dPolygons = polygons.Select(x => new DPolygon((DPoint)x.FirstVertex - Offset, (DPoint)x.SecondVertex - Offset, (DPoint)x.ThirdVertex - Offset)).ToList();
            Content = DesmosExporter.ExportPolygons(dPolygons, "T");
        }

        private void UpdateOffset(IEnumerable<DPoint> points)
        {
            MaxX = points.Max(x => x.X);
            MinX = points.Min(x => x.X);

            MaxY = points.Max(x => x.Y);
            MinY = points.Min(x => x.Y);

            MaxZ = points.Max(x => x.Z);
            MinZ = points.Min(x => x.Z);
        }
    }
}
