
namespace FakerHEM.Models.Responses
{
    public class Plan
    {
        public string Name { get; set; }
        public List<PointDto> Contour { get; set; }
        public List<PointDto> Upper { get; set; }
        public List<PointDto> Lower { get; set; }
        public List<Polygon> Polygons { get; set; }
        public List<Polygon> Terrain { get; set; }
    }
}
