using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FakerDumpAnalyzer.Models
{
    public class CommonSettings
    {
        [Required]
        public string WebSocketMmsIp { get; set; } = string.Empty;

        [Required]
        public int? WebSocketMmsPort { get; set; }

        [Required]
        public int? HttpApiMmsPort { get; set; }

        public int? TcpMmsPort { get; set; }

        [Required]
        public int? ReconnectIntervalMms { get; set; }

        [Required]
        public int? ConnectionTimeout { get; set; }

        [Required]
        public int UnitId { get; set; } = 0;


        [Required]
        public string EndcapSerialPortName { get; set; } = string.Empty;

        [Required]
        public int EndcapSerialPortBaudrate { get; set; }

        [Required]
        public string InclinometersCanName { get; set; } = string.Empty;

        [JsonIgnore]
        public int PeriodReceiveInclinometersData { get; set; }

        [Required]
        public string GpsSerialPortName { get; set; } = string.Empty;

        [Required]
        public int GpsSerialPortBaudrate { get; set; }

        public bool PlanAutoselection { get; set; }

        /// <summary>
        /// Включено ли отображение секторов на боковом виде
        /// </summary>
        public bool ShowSectorsSideView { get; set; } = true;

        public string? ActiveOperator { get; set; }

        /// <summary>
        /// Длина стороны квадрата сектора в метрах
        /// </summary>
        public double SectorSize { get; set; } = 1.5;

        /// <summary>
        /// Максимальная площадь квадрата описанного вокруг полигональнго плана,
        /// в пределах которой вычислаются сектора.
        /// При привышении площади сектора не вычисляются вообще.
        /// </summary>
        public double PlanAreaLimit { get; set; } = 15000;

        /// <summary>
        /// Длина стороны сегмента при расграфовке плана для расчета секторов в метрах
        /// </summary>
        public double SchedulingSegmentSize { get; set; } = 50;

        /// <summary>
        /// Период фонового сохранения статистики в секундах
        /// </summary>
        public double PlanStatisticSavePeriodInSeconds { get; set; } = 300;

        /// <summary>
        /// Работа автостатусов
        /// </summary>
        public bool AutoStatuses { get; set; } = true;

        /// <summary>
        /// Учёт изменений в секторах по тракам экскаватора
        /// </summary>
        public bool ChangesByTracks { get; set; } = true;

        /// <summary>
        /// Название юнита, отображаемое в окне программы
        /// </summary>
        public string? UnitName { get; set; }

        /// <summary>
        /// Количество секторов запрашиваемых с CS за одну итерацию пагинированого запроса
        /// </summary>
        public int SectorsInBatch { get; set; } = 1000;

        /// <summary>
        /// Лимит очереди секторов, при достижении которого сектора будут отсылаться вне таймера
        /// </summary>
        public int SectorsSyncQueueLimit { get; set; } = 50;

        /// <summary>
        /// Таймер, по истечении которого будет происходить отправка секторов из очереди на CS
        /// </summary>
        public int SectorsSyncQueueTimerInSeconds { get; set; } = 120;

    }
}
