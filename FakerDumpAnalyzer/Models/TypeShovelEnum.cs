using System.Runtime.Serialization;

namespace FakerDumpAnalyzer.Models
{
    public enum TypeShovelEnum
    {
        [EnumMember(Value = "straight_shovel")] Straight_Shovel,
        [EnumMember(Value = "reverse_shovel")] Reverse_Shovel,
        [EnumMember(Value = "straight_mechanical_shovel")] Straight_Mechanical_Shovel
    }
}
