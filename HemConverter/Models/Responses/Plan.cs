namespace HemConverter.Models.Responses
{
    public class Plan
    {
        public string Name { get; set; }
        public List<Point> Contour { get; set; }
        public List<Point> Upper { get; set; }
        public List<Point> Lower { get; set; }
        public List<Polygon> Polygons { get; set; }
        public List<Polygon> Terrain { get; set; }
    }
}
