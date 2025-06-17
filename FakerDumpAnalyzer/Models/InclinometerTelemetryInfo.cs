namespace FakerDumpAnalyzer.Models
{
    public class InclinometerTelemetryInfo<TAngleValue>
    {
        public uint? Address { get; set; }
        public InclPlaceType InclPlaceType { get; set; }
        public TAngleValue? Pitch { get; set; }
        public TAngleValue? Roll { get; set; }
        public bool IsCorrect { get; set; }
    }
}
