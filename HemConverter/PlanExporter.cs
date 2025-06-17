using FakeDataHem.FetchData;

namespace HemConverter
{
    public class PlanExporter
    {
        public async Task ExportPlanAsync()
        {
            var client = new HemClient();
            var project = await client.GetActiveProjectAsync();
            var sectors = await client.GetActiveSectorsAsync();

            var e1 = new List<string>();
            var e2 = new List<string>();
            var e3 = new List<string>();

            var startX = sectors.Min(x => x.Centroid.X);
            var startY = sectors.Min(x => x.Centroid.Y);
            var startZ = sectors.Min(x => x.target_height);
            foreach (var polygon in project.Plans[0].Polygons)
            {
                polygon.first_vertex.X -= startX;
                polygon.first_vertex.Y -= startY;
                polygon.first_vertex.Z -= startZ;

                polygon.second_vertex.X -= startX;
                polygon.second_vertex.Y -= startY;
                polygon.second_vertex.Z -= startZ;

                polygon.third_vertex.X -= startX;
                polygon.third_vertex.Y -= startY;
                polygon.third_vertex.Z -= startZ;
            }

            foreach (var sector in sectors.Take(100))
            {
                sector.Centroid.X -= startX;
                sector.Centroid.Y -= startY;
                sector.target_height -= startZ;
            }

            foreach (var polygon in project.Plans[0].Polygons)
            {
                var first = $"({polygon.first_vertex.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.first_vertex.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.first_vertex.Z.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
                var second = $"({polygon.second_vertex.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.second_vertex.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.second_vertex.Z.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
                var third = $"({polygon.third_vertex.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.third_vertex.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {polygon.third_vertex.Z.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
                e1.Add(first);
                e2.Add(second);
                e3.Add(third);
            }
            var e1Result = $"E1=[{string.Join(",", e1)}]";
            var e2Result = $"E2=[{string.Join(",", e2)}]";
            var e3Result = $"E3=[{string.Join(",", e3)}]";

            var s1 = new List<string>();
            var s2 = new List<string>();
            foreach (var sector in sectors.Take(100))
            {
                var first = $"({sector.Centroid.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {sector.Centroid.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {0})";
                var second = $"({sector.Centroid.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {sector.Centroid.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {sector.target_height.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
                s1.Add(first);
                s2.Add(second);
            }

            var s1Result = $"S1=[{string.Join(",", s1)}]";
            var s2Result = $"S2=[{string.Join(",", s2)}]";
        }
    }
}
