using System.Text.Json.Serialization;

namespace FakeDataHem
{
    public class FakeData()
    {
        #region GNSS
        public bool EnableAntenna { get; set; } = false;
        public int Interval { get; set; } = 1000;
        public double Azimuth { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Alt { get; set; }
        #endregion

        [JsonPropertyName("AzimuthBlade")]
        public double FakeAzimuthBlade { get; set; }

        [JsonPropertyName("InclinometersAmplitude")]
        public double InclinometersAmplitude { get; set; }

        [JsonPropertyName("Inclinometers")]
        public List<AnglesData> Inclinometers { get; set; }
    }
}