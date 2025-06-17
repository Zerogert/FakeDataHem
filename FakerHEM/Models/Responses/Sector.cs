using FakerHEM.Models.Responses;

namespace HemConverter.Models.Responses
{
    public class Sector
    {
        public int Id { get; init; }
        public PointDto Centroid { get; init; }
        public double target_height { get; set; }
    }
}
