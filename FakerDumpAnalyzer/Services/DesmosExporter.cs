using System.Globalization;
using System.Text.Json.Serialization;

namespace FakerDumpAnalyzer.Services
{
    public class DesmosExporter
    {
        public static string ExportPlane(DPoint a, DPoint b, DPoint c)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var aText = $@"A=\left({a.X},{a.Y},{a.Z}\right)";
            var bText = $@"B=\left({b.X},{b.Y},{b.Z}\right)";
            var cText = $@"C=\left({c.X},{c.Y},{c.Z}\right)";

            const string plane = @"\operatorname{vector}\left(\left(x,y,z\right),A\right)\cdot\left(\operatorname{vector}\left(A,B\right)\times\operatorname{vector}\left(A,C\right)\right)=0";
            return $"{aText}\n\n{bText}\n\n{cText}\n\n{plane}";

        }

        public static string ExportPolygons(List<DPolygon> polygons, string attribute)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var first = ExportPoints(polygons.Select(x => x.FirstVertex).ToList(), attribute);
            var second = ExportPoints(polygons.Select(x => x.SecondVertex).ToList(), $"{attribute}_1");
            var third = ExportPoints(polygons.Select(x => x.ThirdVertex).ToList(), $"{attribute}_2");
            var triangles = @"\operatorname{triangle}" + @$"\left({attribute},\ {attribute}_1,\ {attribute}_2\right)";

            return $"{first}\n\n{second}\n\n{third}\n\n{triangles}";

        }

        public static string ExportPoints(List<DPoint> dPoints, string attribute)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var pt1 = dPoints.Select((x) => $@"\left({double.Round(x.X, 4)}, {double.Round(x.Y,4)}, {double.Round(x.Z,4)}\right)");
            var p1 = $@"{attribute}=\left[{string.Join(",", pt1)}\right]";

            return p1;
        }

        public static string ExportPolyline(List<DPoint> dPoints, string attribute)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var intersections = dPoints;
            var pt1 = intersections.Select((x) => $@"\left({double.Round(x.X, 4)}, {double.Round(x.Y, 4)}, {double.Round(x.Z, 4)}\right)");
            var p1 = $@"{attribute}_1=\left[{string.Join(",", pt1)}\right]";

            var pt2 = intersections.Skip(1).Select((x) => $@"\left({double.Round(x.X, 4)}, {double.Round(x.Y, 4)}, {x.Z}\right)");
            var p2 = $@"{attribute}_2=\left[{string.Join(",", pt2)}\right]";
            var poly = @"\operatorname{segment}\left" + $@"({attribute}_1,{attribute}_2\right)";

            return $"{p1}\n\n{p2}\n\n{poly}";
        }
    }

    public class DPolyline
    {
        public List<DPoint> Points { get; set; }
    }

    public class DLine
    {
        public DPoint P0 { get; set; }
        public DPoint P1 { get; set; }
    }

    public class DPoint
    {
        public DPoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static DPoint operator -(DPoint left, DPoint right)
        => new DPoint(left.X-right.X, left.Y - right.Y, left.Z - right.Z);
    }


    public class DPolygon
    {
        public DPolygon(DPoint firstVertex, DPoint secondVertex, DPoint thirdVertex)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;
        }

        [JsonPropertyName("firstVertex")]
        public DPoint FirstVertex { get; set; }

        [JsonPropertyName("secondVertex")]
        public DPoint SecondVertex { get; set; }

        [JsonPropertyName("thirdVertex")]
        public DPoint ThirdVertex { get; set; }

        public DPoint[] GetPoints() => [FirstVertex, SecondVertex, ThirdVertex];
    }
}
