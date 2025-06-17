using FakerDumpAnalyzer.Models;
using FakerDumpAnalyzer.Services;
using FakerHEM.Helpers;
using System.Windows.Input;

namespace FakerDumpAnalyzer.ViewModels
{
    public class ExporterDesmosViewModel : BaseViewModel
    {
        private readonly DumpProvider _dumpProvider;

        private string content;
        private double minX;
        private double minY;
        private double minZ;
        private double maxX;
        private double maxY;
        private double maxZ;

        public DPoint Offset => new DPoint(minX, minY, minZ);
        public ICommand ExportLower { get; private set; }
        public ICommand ExportUpper { get; private set; }
        public ICommand ExportToolPlane { get; private set; }
        public ICommand ExportSectorIntersections { get; private set; }
        public ICommand ExportLocalSectorSegmentIntersections { get; private set; }
        public ICommand ExportProgressIntersections { get; private set; }
        public ICommand ExportSectors { get; private set; }
        public ICommand ExportFrontSegments { get; private set; }
        public ICommand ExportTarget { get; private set; }
        public ICommand ExportTerrain { get; private set; }
        public ICommand ExportBucketPlane { get; private set; }

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

        public ExporterDesmosViewModel(DumpProvider dumpProvider)
        {
            _dumpProvider = dumpProvider;
            _dumpProvider.OnUpdated += _dumpProvider_OnUpdated;
            ExportLower = new RelayCommand(ExportLowerExecute);
            ExportUpper = new RelayCommand(ExportUpperExecute);
            ExportToolPlane = new RelayCommand(ExportToolPlaneExecute);
            ExportSectorIntersections = new RelayCommand(ExportSectorIntersectionsExecute);
            ExportProgressIntersections = new RelayCommand(ExportProgressIntersectionsExecute);
            ExportSectors = new RelayCommand(ExportSectorsExecute);
            ExportFrontSegments = new RelayCommand(ExportFrontSegmentsExecute);
            ExportLocalSectorSegmentIntersections = new RelayCommand(ExportLocalSectorSegmentIntersectionsExecute);
            ExportTarget = new RelayCommand(ExportTargetExecute);
            ExportTerrain = new RelayCommand(ExportTerrainExecute);
            ExportBucketPlane = new RelayCommand(ExportBucketPlaneExecute);
        }

        private void ExportTerrainExecute()
        {
            var polygons = _dumpProvider.Dump?
                .ActivePlan
                .Terrain;

            if (polygons?.DefaultIfEmpty() is null) return;

            var dPolygons = polygons.Select(x => new DPolygon((DPoint)x.FirstVertex - Offset, (DPoint)x.SecondVertex - Offset, (DPoint)x.ThirdVertex - Offset)).ToList();
            Content = DesmosExporter.ExportPolygons(dPolygons, "T");
        }

        private void ExportTargetExecute()
        {
            var polygons = _dumpProvider.Dump?
                .ActivePlan
                .Target;

            if (polygons?.DefaultIfEmpty() is null) return;
            var dPolygons = polygons.Select(x => new DPolygon((DPoint)x.FirstVertex - Offset, (DPoint)x.SecondVertex - Offset, (DPoint)x.ThirdVertex - Offset)).ToList();

            Content = DesmosExporter.ExportPolygons(dPolygons, "I");
        }

        private void ExportFrontSegmentsExecute()
        {
            const string attribute = "F";
            var segments = _dumpProvider.Dump
                .ViewStreamDTO
                .FrontView
                .Intersections
                .ConnectedSegments;

            var pt1 = segments.Select((x) => $@"\left({x.Upper.First().X*10}, {x.Upper.First().Y * 10}, {x.Upper.First().Z * 10}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = segments.Select((x) => $@"\left({x.Upper.Last().X * 10}, {x.Upper.Last().Y * 10}, {x.Upper.Last().Z * 10}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            var pt3 = segments.Select((x) => $@"\left({x.Lower.First().X * 10}, {x.Lower.First().Y * 10}, {x.Lower.First().Z * 10}\right)");
            var p3 = $@"{attribute}_3=\left[{string.Join(",", pt3)}\right]";

            var pt4 = segments.Select((x) => $@"\left({x.Lower.Last().X * 10}, {x.Lower.Last().Y * 10}, {x.Lower.Last().Z * 10}\right)");
            var p4 = $@"{attribute}_4=\left[{string.Join(",", pt4)}\right]";
            var poly1 = @"\operatorname{segment}\left" + $@"({attribute}_3,{attribute}_4\right)";

            Content = $"{p1}\n\n{p2}\n\n{poly}\n\n{p3}\n\n{p4}\n\n{poly1}";
        }

        private void ExportLocalSectorSegmentIntersectionsExecute()
        {
            const string attribute = "P";
            var segments = _dumpProvider.Dump
                .SectorsIntersectionsUpdater
                .Segments;

            var pt1 = segments.Select((x) => $@"\left({x.P1.X - MinX}, {x.P1.Y - MinY}, {x.P1.Z - MinZ}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = segments.Select((x) => $@"\left({x.P2.X - MinX}, {x.P2.Y - MinY} ,  {x.P2.Z - MinZ}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            Content = $"{p1}\n\n{p2}\n\n{poly}";
        }

        private void ExportSectorsExecute()
        {
            const string attribute = "J";
            var sectors = _dumpProvider.Dump.SectorsManager.Sectors;
            var pt1 = sectors.Select((x) => $@"\left({x.Centroid.X - MinX}, {x.Centroid.Y - MinY}, {(x.TargetHeight - MinZ).ToString("F99").TrimEnd('0')}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = sectors.Select((x) => $@"\left({x.Centroid.X - MinX}, {x.Centroid.Y - MinY}, {x.CurrentHeight ?? x.InitialHeight - MinZ}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            Content = $"{p1}\n\n{p2}\n\n{poly}";
        }

        private void ExportProgressIntersectionsExecute()
        {
            const string attribute = "S";
            var intersections = _dumpProvider.Dump
                .ViewStreamDTO
                .SideView
                .Intersections
                .ConnectedSegments
                .FirstOrDefault()
                .ExcavationProgress;
            var pt1 = intersections.Select((x) => $@"\left({x.X}, {x.Y}, {x.Z}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = intersections.Skip(1).Select((x) => $@"\left({x.X}, {x.Y}, {x.Z}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            Content = $"{p1}\n\n{p2}\n\n{poly}";
        }

        private void ExportSectorIntersectionsExecute()
        {
            const string attribute = "S";
            var intersections = _dumpProvider.Dump.SectorsIntersectionsUpdater.Intersections;
            var pt1 = intersections.Select((x) => $@"\left({x.X}, {x.Y}, {x.Z}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = intersections.Skip(1).Select((x) => $@"\left({x.X}, {x.Y}, {x.Z}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            Content = $"{p1}\n\n{p2}\n\n{poly}";
        }

        private void ExportBucketPlaneExecute()
        {
            var a = _dumpProvider.Dump.NodesCoordinates.LocalLeftBucketPoint;
            var b = _dumpProvider.Dump.NodesCoordinates.LocalRightBucketPoint;
            var c = _dumpProvider.Dump.NodesCoordinates.LocalOverBucketPoint;

            Content = DesmosExporter.ExportPlane((DPoint)a - Offset, (DPoint)b - Offset, (DPoint)c - Offset);
        }


        private void ExportToolPlaneExecute()
        {
            var a = _dumpProvider.Dump.NodesCoordinates.LocalToolPlanePoint1;
            var b = _dumpProvider.Dump.NodesCoordinates.LocalToolPlanePoint2;
            var c = _dumpProvider.Dump.NodesCoordinates.LocalToolPlanePoint3;

             Content = DesmosExporter.ExportPlane((DPoint)a - Offset, (DPoint)b - Offset, (DPoint)c - Offset);
        }

        private void ExportUpperExecute()
        {
            Content = GetPolyline(_dumpProvider.Dump.ActivePlan.Upper, "T");
        }

        private void ExportLowerExecute()
        {
            Content = GetPolyline(_dumpProvider.Dump.ActivePlan.Lower, "L");
        }

        private string GetPolyline(List<Point> points, string attribute)
        {
            var pt1 = points.Select((x) => $@"\left({x.X - MinX}, {x.Y - MinY}, {x.Z - MinZ}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var points2 = points.Skip(1).ToList();
            points2.Add(points.Last());
            var pt2 = points2.Select((x) => $@"\left({x.X - MinX}, {x.Y - MinY}, {x.Z - MinZ}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";

            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            return $"{p1}\n\n{p2}\n\n{poly}";
        }

        private void _dumpProvider_OnUpdated(object? sender, EventArgs e)
        {
            if (_dumpProvider.Dump is null) return;

            var plan = _dumpProvider.Dump.ActivePlan;
            if (plan is null) return;

            var points = plan.Contour.Union(plan.Upper);
            MaxX = points.Max(x => x.X);
            MinX = points.Min(x => x.X);

            MaxY = points.Max(x => x.Y);
            MinY = points.Min(x => x.Y);

            MaxZ = points.Max(x => x.Z);
            MinZ = points.Min(x => x.Z);
        }
    }
}
