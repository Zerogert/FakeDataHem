namespace FakerDumpAnalyzer.Models
{
    public class Sector
    {
        public int? CsId { get; set; }
        public int? GraderPlanPolygonalId { get; set; }
        public int? ShovelPlanId { get; set; }

        #region Coordinates
        public int BottomLeftId { get; set; }
        public Point2 BottomLeft { get; set; } = new Point2(0, 0);
        public int TopRightId { get; set; }
        public Point2 TopRight { get; set; } = new Point2(0, 0);

        private Point2? _topLeft;
        public Point2 TopLeft => _topLeft ??= new Point2 { X = BottomLeft!.X, Y = TopRight!.Y };

        private Point2? _bottomRight;
        public Point2 BottomRight => _bottomRight ??= new Point2 { X = TopRight!.X, Y = BottomLeft!.Y };

        private Point2? _centroid;
        public Point2 Centroid => _centroid ??= new Point2
        {
            X = (BottomLeft!.X + TopRight!.X) / 2,
            Y = (BottomLeft.Y + TopRight.Y) / 2,
        };

        // Track for vector updates
        private bool _currentHeightChanged = true;
        /// <summary>
        /// Целевая высота сектора, в метрах
        /// </summary>
        public double TargetHeight { get; set; }
        public double InitialHeight { get; set; }
        private double? _currentHeight;

        /// <summary>
        /// Текущая высота сектора, в метрах
        /// </summary>
        public double? CurrentHeight
        {
            get { return _currentHeight; }
            set
            {
                if (_currentHeight != value)
                {
                    _currentHeight = value;
                    _currentHeightChanged = true;
                }
            }
        }

        /// <summary>
        /// Отклонение текущей высоты сектора до целевой. Может быть отрицательной. 
        /// </summary>
        public double? Distance => CurrentHeight is null ? null : CurrentHeight.Value - TargetHeight;
        #endregion

        private double? _sectorArea;
        public double SectorArea => _sectorArea ??= (TopRight!.X - BottomLeft!.X) * (TopRight.Y - BottomLeft!.Y);

        private double? _totalVolume;
        public double TotalVolume => _totalVolume ??= (TargetHeight != InitialHeight ?
            Math.Abs(InitialHeight - TargetHeight) * SectorArea :
            0.0);

        public double CompletedVolume => CurrentHeight.HasValue && CurrentHeight.Value < InitialHeight ?
            Math.Abs(InitialHeight - CurrentHeight.Value) * SectorArea :
            0.0;

        public double Progress => CompletedVolume / TotalVolume;
    }
}