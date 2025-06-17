namespace FakerDumpAnalyzer.Models
{
    public class SectorsIntersectionsUpdater
    {
        public List<Point> Intersections { get; set; }
        public List<Segment> Segments { get; set; }
        public List<Segment> OrderedSegments { get; set; }
    }
}
