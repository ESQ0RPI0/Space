using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Backend.Datamodel.Models.NewSpace
{
    public class NewSpaceExternalListItemModel
    {
        public string Organization { get; set; }
        public string? Launcher { get; set; }
        public int? Founded { get; set; }
        public LaunchVehicleStatus Status { get; set; }
        public char? ItemCurrency { get; set; }
        public string? FirstLaunch { get; set; }
        public int Launches { get; set; }
        public decimal? Cost { get; set; }
        public char? CostMultiplier { get; set; }
        public int? Perfomance { get; set; }
        public decimal? PricePerKg { get; set; }
        public string Funding { get; set; }
        public char? FundingMultiplier { get; set; }
        public string? Logo { get; set; }
        public string? Photo { get; set; }
    }
}
