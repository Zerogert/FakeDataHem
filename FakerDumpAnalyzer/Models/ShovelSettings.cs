using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerDumpAnalyzer.Models
{
    public class ShovelSettings
    {
        [Required]
        public double UnitLength { get; set; }

        [Required]
        public double UnitWidth { get; set; }

        [Required]
        public double BucketWidth { get; set; }

        [Required]
        public double MasterAntennaLeftBottomCornerOffsetX { get; set; }

        [Required]
        public double MasterAntennaLeftBottomCornerOffsetY { get; set; }

        [Required]
        public double MasterAntennaRotateCenterOffsetX { get; set; }

        [Required]
        public double MasterAntennaRotateCenterOffsetY { get; set; }

        [Required]
        public double AzimuthAntennaRotateCenterOffsetX { get; set; }

        [Required]
        public double AzimuthAntennaRotateCenterOffsetY { get; set; }

        [Required]
        public double L3 { get; set; }

        [Required]
        public double L2 { get; set; }

        [Required]
        public double L1 { get; set; }

        [Required]
        public double L0 { get; set; }

        [Required]
        public double LengthFromFirstNodeToGround { get; set; }

        [Required]
        public double LengthFromMasterToGround { get; set; }

        public TypeShovelEnum ToolType { get; set; }

        /// <summary>
        /// Безопасный радиус копания
        /// </summary>
        public double? SafeDiggingRadius { get; set; }

        /// <summary>
        /// Безопасный угол копания
        /// </summary>
        public double? SafeDiggingAngle { get; set; }

        #region Reverse
        /// <summary>
        /// Верхнее плечо - a
        /// </summary>
        public double? SidelinkArmHlink { get; set; }

        /// <summary>
        /// Свободное плечо - b
        /// </summary>
        public double? HlinkSidelinkBucket { get; set; }

        /// <summary>
        /// Нижнее плечо - c
        /// </summary>
        public double? BucketArmHlink { get; set; }

        /// <summary>
        /// Расстояние между ковшовыми шарнирами на рукояти - d
        /// </summary>
        public double? ArmSidelinkBucket { get; set; }

        /// <summary>
        /// Расстояние от второго шарнира на ковше до зубьев - e
        /// </summary>
        public double? BucketHlinkEdge { get; set; }

        /// <summary>
        /// Угол между рукоятью и шарниром (ближайшим) свободного плеча (град.)
        /// </summary>
        public double? AngleArmSidelinkBucketArm { get; set; }
        #endregion

        #region Mechanical
        /// <summary>
        /// Оффсет от оси вращения до шарнира рукояти по Y в метрах
        /// </summary>
        public double? RotationAxisToArmJointOffsetY { get; set; }

        /// <summary>
        /// Расстояние от шарнира рукояти до земли в метрах
        /// </summary>
        public double? LengthFromArmJointToGround { get; set; }

        /// <summary>
        /// Расстояние от шарнира рукояти до земли в метрах
        /// </summary>
        public double? ArmJointToArmBaseline { get; set; }

        /// <summary>
        /// Имя can-шины энкодера
        /// </summary>
        public string? CanName { get; set; } = "can1";
        #endregion
    }
}
