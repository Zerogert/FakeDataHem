using FakerDumpAnalyzer.Models;

namespace FakerDumpAnalyzer.Models
{
    public class ViewStreamDTO
    {
        public SideViewDTO SideView { get; set; }
        public TopViewDTO TopView { get; set; }
        public FrontViewDTO FrontView { get; set; }
        public SensorsDTO Sensors { get; set; }
    }
}
public class SideViewDTO
{
    public SideNodesDTO Nodes { get; set; }
    public IntersectionsDTO Intersections { get; set; }
    public double? BodyPitch { get; set; }
}
public class TopViewDTO
{
    public double? ToolLength { get; set; }
    public double? BucketLength { get; set; }
    public double? Azimuth { get; set; }
    public Point FirstNodeCoords { get; set; }
    public Point ArmJointCoords { get; set; }
    public Point ClosestActivePlanPoint { get; set; }
    public Point ForwardLeftTrack { get; set; }
    public Point ForwardRightTrack { get; set; }
    public Point BackLeftTrack { get; set; }
    public Point BackRightTrack { get; set; }
    public Point LeftBucketPoint { get; set; }
    public Point RightBucketPoint { get; set; }
}
public class FrontViewDTO
{
    public FrontNodesDTO Nodes { get; set; }
    public IntersectionsDTO Intersections { get; set; }
    public double? BodyRoll { get; set; }
}
public class SensorsDTO
{
    public double? PlanDistance { get; set; }
    public double? TrackHeight { get; set; }
    public double? Reach { get; set; }
    public Point BucketTeeth { get; set; }
}


public class IntersectionsDTO
{
    public List<SegmentsIntersectionDTO> ConnectedSegments { get; set; }

    public List<PolylineDTO> Polylines { get; set; }
}

public class SegmentsIntersectionDTO
{
    public List<Point> Upper { get; set; }
    public List<Point> Lower { get; set; }
    public List<Point> ExcavationProgress { get; set; }

}

public class PolylineDTO
{
    public string Color { get; set; }
    public List<Point> Points { get; set; }
    public List<Point> ExcavationProgress { get; set; }
}

public class SideNodesDTO
{
    public HydraulicSideNodes Hydraulic { get; set; }
    public MechanicSideNodes Mechanic { get; set; }
}

public class HydraulicSideNodes
{
    public Point BoomJoint { get; set; }
    public Point ArmJoint { get; set; }
    public Point BucketJoint { get; set; }
    public Point BucketTeeth { get; set; }
}

public class MechanicSideNodes
{
    public Point ArmJoint { get; set; }
    public Point ArmEnd { get; set; }
    public Point BucketTeeth { get; set; }
}

public class FrontNodesDTO
{
    public HydraulicFrontNodes Hydraulic { get; set; }
    public MechanicFrontNodes Mechanic { get; set; }
}

public class HydraulicFrontNodes
{
    public Point LeftBucketPoint { get; set; }
    public Point RightBucketPoint { get; set; }
    public Point BucketJoint { get; set; }
}

public class MechanicFrontNodes
{
    public Point LeftBucketPoint { get; set; }
    public Point RightBucketPoint { get; set; }
    public Point BucketJoint { get; set; }
}
