using FakerDumpAnalyzer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerDumpAnalyzer.Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }


        public static explicit operator DPoint(Point v)
        {
            return new DPoint(v.X, v.Y, v.Z);
        }
    }
}
