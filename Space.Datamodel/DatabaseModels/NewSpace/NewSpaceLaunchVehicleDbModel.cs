using Space.Shared.Common;
using System.ComponentModel.DataAnnotations;
using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Server.Datamodel.DatabaseModels.NewSpace
{
    /// <summary>
    /// Full NewSpace launch vehicle model
    /// </summary>
    public class NewSpaceLaunchVehicleDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public LaunchVehicleStatus Status { get; set; }
        public string? FirstLaunch { get; set; }
        public int Launches { get; set; }
        public decimal? Cost { get; set; }
        public decimal? PricePerKg { get; set; }
        public int Perfomance { get; set; }
        public LaunchVehiclePropellantType Propelant { get; set; }
        public LaunchVehicleReusabilityType Reusability { get; set; }
        public string Comment { get; set; }
        public string Photo { get; set; }
        public string? PhotoSource { get; set; }
    }
}
