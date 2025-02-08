using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Backend.Datamodel.Models.NewSpace
{
    public class NsExternalListItemModel
    {
        public string Organization { get; set; }
        public string? Launcher { get; set; }
        public string Founded { get; set; }
        public string Status { get; set; }
        public char? ItemCurrency { get; set; }
        public string? FirstLaunch { get; set; }
        public string Launches { get; set; }
        public string? Cost { get; set; }
        public char? CostMultiplier { get; set; }
        public string? Perfomance { get; set; }
        public string? PricePerKg { get; set; }
        public bool IsFundingExist { get; set; }
        public string Funding { get; set; }
        public char? FundingMultiplier { get; set; }
        public string? Logo { get; set; }
        public string? Photo { get; set; }
    }
}
