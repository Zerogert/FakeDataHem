namespace HemConverter.Models.Responses
{
    public class Sector
    {
        public int Id { get; init; }
        public Point Centroid { get; init; }
        public double target_height { get; set; }
    }
}
