namespace HemConverter.Models.Responses.CoordSystem
{
    public class CoordSystemResponse
    {
        public DateTime created { get; set; }
        public string name { get; set; }
        public Definition definition { get; set; }
        public Reference_Points[] reference_points { get; set; }
        public int c_s_id { get; set; }
        public bool got_coords_system { get; set; }
        public int id { get; set; }
    }

}
