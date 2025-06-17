namespace HemConverter.Models.Responses.CoordSystem
{
    public class Definition
    {
        public double[][] rotate_matrix { get; set; }
        public double scale_ratio { get; set; }
        public double[] translate_vector { get; set; }
        public int utm_cent_mer { get; set; }
        public int id { get; set; }
    }

}
