using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerDumpAnalyzer.Models
{
    public class NodesCoordinatesManager
    {
        public TypeShovelEnum TypeShovel { get;  set; }

        #region Coords

        #region Common
        public Point LocalToolPlanePoint1 { get; set; }
        public Point LocalToolPlanePoint2 { get;  set; }
        public Point LocalToolPlanePoint3 { get;  set; }
        public Point LocalZeroInLocal { get;  set; }
        public Point ExcBucketCoordinate { get;  set; }
        public Point LocalBucketCoordinateCoordinate { get;  set; }
        public Point ExcArmJointCoordinate { get;  set; }

        public Point ExcForwardLeftTrack { get;  set; }
        public Point ExcForwardRightTrack { get;  set; }
        public Point ExcBackLeftTrack { get;  set; }
        public Point ExcBackRightTrack { get;  set; }
        public Point LocalForwardLeftTrack { get;  set; }
        public Point LocalForwardRightTrack { get;  set; }
        public Point LocalBackLeftTrack { get;  set; }
        public Point LocalBackRightTrack { get;  set; }

        public Point ExcLeftBucketPoint { get;  set; }
        public Point LocalLeftBucketPoint { get;  set; }
        public Point ExcRightBucketPoint { get;  set; }
        public Point LocalRightBucketPoint { get;  set; }
        public Point LocalOverBucketPoint { get;  set; }


        public double? LocalTrackHeight { get;  set; }
        #endregion

        #region Hydraulic
        public Point ExcBoomJointCoordinate { get;  set; }
        public Point LocalBoomJointCoordinate { get;  set; }

        public Point ExcBucketJointCoordinate { get;  set; }
        public Point LocalBucketJointCoordinate { get;  set; }
        #endregion

        #region Mechanic
        public Point ExcArmEndCoordinate { get;  set; }
        public Point LocalArmEnd { get;  set; }
        public Point LocalArmJointCoordinate { get;  set; }
        #endregion

        #endregion

    }
}
