namespace FakerHEM.Models.Responses
{
    public class PointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public PointDto()
        {
        }

        public PointDto(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
