namespace FakerDumpAnalyzer.Models
{
    public class ShovelPlan
    {
        public int? MaterialId { get; set; }
        public PlanType Type { get; set; }
        public CoordinatesType CoordinatesType { get; set; }
        public string Name { get; set; } = string.Empty;
        public double? Volume { get; set; }
        public double? Density { get; set; }
        public int? Order { get; set; }
        public double? AverageContent { get; set; }
        public bool IsPlanCalculatingOnCs { get; set; } = false;

        public List<Point> Upper { get; set; } = [];
        public List<Point> Lower { get; set; } = [];
        public List<Polygon> Terrain { get; set; } = [];
        public List<Polygon> Target { get; set; } = [];
        public ICollection<Point> Contour { get; set; } = [];
        public DateTime UpdateDate { get; set; }
        public ToolType Tool { get; set; }
    }
}
