using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HemConverter.Models.Responses
{
    public class Point
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int Order { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public int? GraderPlanPolygonalId { get; set; }

        public const double CalculationTolerance = 0.01;

        public Point()
        {
        }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 GetVector(Point translation)
        {
            return new Vector3((float)(X + translation.X), (float)(Y + translation.Y), (float)(Z + translation.Z));
        }

        public Vector3 GetVector()
        {
            return new Vector3((float)X, (float)Y, (float)Z);
        }

        public static Vector3 GetVector(double x, double y, double z)
        {
            return new Vector3((float)x, (float)y, (float)z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Point point)
                return false;
            return Math.Abs(X - point.X) < CalculationTolerance &&
                   Math.Abs(Y - point.Y) < CalculationTolerance &&
                   Math.Abs(Z - point.Z) < CalculationTolerance;
        }
        public bool IsCorrect() => !double.IsNaN(X) && !double.IsNaN(Y) && !double.IsNaN(Z);

        public override string ToString()
        {
            NumberFormatInfo info = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            return $"({X.ToString(info)}, {Y.ToString(info)}, {Z.ToString(info)})";
        }
    }
}
