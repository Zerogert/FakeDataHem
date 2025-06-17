
using FakerHEM.Models.Responses;
using RstConverter.Models;


namespace FakeDataHem
{
    public class ConverterCoordsSystem
    {
        private RstParameters? _parameters;

        public PointDto ToLocal(double lat, double lng, double z)
        {
            var utmCoords = RstConverter.RstConverter.Wgs84ToUtm(new Wgs84(lat, lng, z), (int)_parameters!.UtmCentMer);
            var localCoords = RstConverter.RstConverter.UtmToLocal(utmCoords, GetTranslateParameters(_parameters));
            return new PointDto(localCoords.X, localCoords.Y, localCoords.Z);
        }

        public PointDto ToWgs84(double x, double y, double z)
        {
            var utmCoords = RstConverter.RstConverter.LocalToUtm(new LocalCoords(x, y, z), GetTranslateParameters(_parameters!));
            var wgsCoords = RstConverter.RstConverter.UtmToWgs84(utmCoords, (int)_parameters!.UtmCentMer);
            return new PointDto(wgsCoords.Lat, wgsCoords.Lng, wgsCoords.Z);
        }

        public void SetDefinition(RstParameters rstParameters)
        {
            _parameters = rstParameters;
        }

        public bool IsDefinitionUpdated()
        {
            return _parameters is not null;
        }

        private RstParameters GetTranslateParameters(RstParameters parameters)
        {
            return new RstParameters(Translate(parameters.RotateMatrix), parameters.ScaleRatio,
                parameters.TranslateVector, parameters.UtmCentMer);
        }

        private double[][] Translate(double[][] matrix)
        {
            var transMatrix = new double[matrix.Length][];
            for (var i = 0; i < matrix.Length; i++)
            {
                transMatrix[i] = new double[matrix[i].Length];
                for (var j = 0; j < matrix[i].Length; j++)
                    transMatrix[i][j] = matrix[j][i];
            }

            return transMatrix;
        }
    }
}
