using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Backend.Datamodel.Models.NewSpace
{
    public class NewSpaceListModel
    {
        public string Organization { get; set; }
        public string? Launcher { get; set; }
        public int? Founded { get; set; }
        public LaunchVehicleStatus Status { get; set; }
        public DateTimeOffset? FirstLaunch { get; set; }
        public int Launches { get; set; }
        public decimal? Cost { get; set; }
        public int? Perfomance { get; set; }
        public decimal? PricePerKg => Cost / Perfomance;
        public bool Funding { get; set; }
        public string? Logo { get; set; }
        public string? Photo { get; set; }
    }
}
