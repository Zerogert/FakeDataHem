using FakerDumpAnalyzer.Models;

namespace FakerDumpAnalyzer.Models
{
    public class PlanIntersectionUpdater
    {
        public PlanIntersections ExcIntersections { get; set; }
        public PlanIntersections FrontIntersections { get; set; }
    }
}
public class PlanIntersections
{
    public List<Segment> Upper { get; set; } = new();
    public List<Segment> Lower { get; set; } = new();
    public List<Point> Polygons { get; set; } = new();
}