using FakeDataHem;
using FakeDataHem.FetchData;
using RstConverter.Models;
using Point = HemConverter.Models.Responses.Point;

namespace HemConverter
{
    public class PlanPointProvider
    {
        public async Task<Point> GetPlanPoint()
        {
            var client = new HemClient();
            var result = await client.GetActiveCoordsAsync();
            var converter = new ConverterCoordsSystem();
            var parameters = new RstParameters(
            result.definition.rotate_matrix,
            result.definition.scale_ratio,
            result.definition.translate_vector,
            result.definition.utm_cent_mer);

            converter.SetDefinition(parameters);

            var project = await client.GetActiveProjectAsync();
            var planPoint = project?.Plans?.FirstOrDefault()?.Contour?.FirstOrDefault()
                ?? project?.Plans?.FirstOrDefault()?.Polygons?.FirstOrDefault()?.first_vertex 
                ?? project?.Plans?.FirstOrDefault()?.Terrain?.FirstOrDefault()?.first_vertex
                ?? project?.Plans?.FirstOrDefault()?.Upper?.FirstOrDefault()
                ?? new Point();
            var coords = converter.ToWgs84(planPoint.X, planPoint.Y, planPoint.Z);
            //54.956010888409246, 82.94571144283913, 30.649096132203113

            return coords;
        }

    }
}
