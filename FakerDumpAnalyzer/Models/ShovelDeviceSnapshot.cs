namespace FakerDumpAnalyzer.Models
{
    public class ShovelDeviceSnapshot
    {
        public InclinometerTelemetryInfo<double>? Bucket { get; private set; }
        public InclinometerTelemetryInfo<double>? Arm { get; private set; }
        public InclinometerTelemetryInfo<double>? Boom { get; private set; }
        public InclinometerTelemetryInfo<double>? Body { get; private set; }

        public double? CalculatedAzimuth;
        public double? AzimuthBody { get; private set; }

        public ShovelPlan ShovelActivePlan { get; private set; }

        /// <summary>
        /// Координаты антенны в локальной системе координат
        /// </summary>
        public Point? HostAntennaCoordinates { get; set; }
    }
}
