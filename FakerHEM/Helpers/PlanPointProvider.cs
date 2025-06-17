using FakeDataHem;
using FakeDataHem.FetchData;
using FakerHEM.Models.Responses;
using RstConverter.Models;
using System.Windows;
namespace FakerHEM.Helpers
{
    public class PlanPointProvider
    {
        public async Task<PointDto> GetPoint(PointDto point)
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

            var coords = converter.ToWgs84(point.X, point.Y, point.Z);
            //54.956010888409246, 82.94571144283913, 30.649096132203113

            return coords;
        }

    }
}
