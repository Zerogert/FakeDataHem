using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FakerDumpAnalyzer.Models
{
    public class Polygon
    {
        [JsonPropertyName("first_vertex")]
        public Point FirstVertex { get; set; } = new();

        [JsonPropertyName("second_vertex")]
        public Point SecondVertex { get; set; } = new();

        [JsonPropertyName("third_vertex")]
        public Point ThirdVertex { get; set; } = new();

        [JsonPropertyName("fourth_vertex")]
        public Point? FourthVertex { get; set; }

        public Polygon()
        {
        }

        public int? GraderPlanPolygonalId { get; set; }

        [JsonIgnore]
        public bool IsTriangle => FourthVertex is null && !ThirdVertex.Equals(FourthVertex);

        private Point? _centerMass;

        [JsonIgnore]
        public Point CenterMass
        {
            get
            {
                if (_centerMass == null)
                {
                    if (IsTriangle)
                    {
                        _centerMass = new Point()
                        {
                            X = (FirstVertex.X + SecondVertex.X + ThirdVertex.X) / 3,
                            Y = (FirstVertex.Y + SecondVertex.Y + ThirdVertex.Y) / 3,
                            Z = (FirstVertex.Z + SecondVertex.Z + ThirdVertex.Z) / 3
                        };
                    }

                    else
                    {
                        _centerMass = new Point()
                        {
                            X = (FirstVertex.X + SecondVertex.X + ThirdVertex.X + FourthVertex!.X) / 4,
                            Y = (FirstVertex.Y + SecondVertex.Y + ThirdVertex.Y + FourthVertex!.Y) / 4,
                            Z = (FirstVertex.Z + SecondVertex.Z + ThirdVertex.Z + FourthVertex!.Z) / 4
                        };
                    }
                }
                return _centerMass;
            }
        }

        public IEnumerable<Point> GetPoints()
        {
            yield return FirstVertex;
            yield return SecondVertex;
            yield return ThirdVertex;
            if (FourthVertex is not null) yield return FourthVertex;
        }


        /// <summary>
        /// Optimized version of points inside polygon
        /// </summary>
        public bool PointInsidePolygon(Point2 point) => PointInsidePolygon(point.X, point.Y);

        /// <summary>
        /// Optimized version of points inside polygon
        /// </summary>
        public bool PointInsidePolygon(double X, double Y)
        {
            if (FirstVertex.X == X && FirstVertex.Y == Y ||
                SecondVertex.X == X && SecondVertex.Y == Y ||
                ThirdVertex.X == X && ThirdVertex.Y == Y ||
                !IsTriangle && FourthVertex!.X == X && FourthVertex.Y == Y)
            {
                return false;
            }

            var inside = false;

            inside ^= IsEdgeIntersect(FirstVertex, SecondVertex, X, Y);
            inside ^= IsEdgeIntersect(SecondVertex, ThirdVertex, X, Y);
            inside ^= IsEdgeIntersect(ThirdVertex, FirstVertex, X, Y);

            if (FourthVertex is not null)
            {
                inside ^= IsEdgeIntersect(ThirdVertex, FourthVertex, X, Y);
                inside ^= IsEdgeIntersect(FourthVertex, FirstVertex, X, Y);
            }

            return inside;
        }

        private bool IsEdgeIntersect(Point p1, Point p2, double X, double Y)
        {
            if (p2.Y > Y != p1.Y > Y &&
                X < (p1.X - p2.X) * (Y - p2.Y) / (p1.Y - p2.Y) + p2.X)
            {
                return true;
            }

            return false;
        }

        public double InterpolateZ(double x, double y)
        {
            double areaABC = CalculateTriangleArea(FirstVertex.X, FirstVertex.Y, SecondVertex.X, SecondVertex.Y, ThirdVertex.X, ThirdVertex.Y);
            double alpha = CalculateTriangleArea(x, y, SecondVertex.X, SecondVertex.Y, ThirdVertex.X, ThirdVertex.Y) / areaABC;
            double beta = CalculateTriangleArea(FirstVertex.X, FirstVertex.Y, x, y, ThirdVertex.X, ThirdVertex.Y) / areaABC;
            double gamma = CalculateTriangleArea(FirstVertex.X, FirstVertex.Y, SecondVertex.X, SecondVertex.Y, x, y) / areaABC;

            double interpolatedZ = alpha * FirstVertex.Z + beta * SecondVertex.Z + gamma * ThirdVertex.Z;

            return interpolatedZ;
        }

        private double CalculateTriangleArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return 0.5 * Math.Abs((x1 - x3) * (y2 - y1) - (x1 - x2) * (y3 - y1));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + FirstVertex.GetHashCode();
                hash = hash * 23 + SecondVertex.GetHashCode();
                hash = hash * 23 + ThirdVertex.GetHashCode();
                if (FourthVertex != null)
                {
                    hash = hash * 23 + FourthVertex.GetHashCode();
                }
                return hash;
            }
        }
    }
}
