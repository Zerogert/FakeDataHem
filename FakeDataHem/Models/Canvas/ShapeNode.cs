using System.Windows.Media;

namespace FakeDataHem.Models.Canvas
{
    public class ShapeNode
    {
        public Geometry Geometry { get; set; }
        public Brush Stroke { get; set; }
        public Brush Fill { get; set; }
    }
}
