namespace FakerDumpAnalyzer.Models
{
    public class DumpModel
    {
        public ShovelPlan CurrentPlan { get; set; }
        public ShovelPlan ActivePlan { get; set; }
        public ShovelDeviceSnapshot ShovelDeviceSnapshot { get; set; }
        public NodesCoordinatesManager NodesCoordinates { get; set; }
        public PlanIntersectionUpdater PlanIntersectionUpdater { get; set; }
        public SectorsIntersectionsUpdater SectorsIntersectionsUpdater { get; set; }
        public ViewStreamDTO ViewStreamDTO { get; set; }
        public CommonSettings CommonSettings { get; set; }
        public ShovelSettings ShovelSettings { get; set; }
        public AppSettingsDto AppSettings { get; set; }
        public SectorsManager SectorsManager { get; set; }
    }
}
