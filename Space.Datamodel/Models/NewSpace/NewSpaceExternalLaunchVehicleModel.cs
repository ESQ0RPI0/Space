using static Space.Shared.Space.LaunchVehicleEnums;

namespace Space.Server.Datamodel.Models.NewSpace
{
    public class NewSpaceExternalLaunchVehicleModel
    {
        public int Name { get; set; }
        public string[] SecondaryNames { get; set; }
        public LaunchVehicleStatus LauncherStatus { get; set; }
        public DateTime FirstLaunch { get; set; }
        public DateTime FirstAnnouncedLaunch { get; set; }
        public int Launches { get; set; }
        public decimal LaunchCost { get; set; }
        public int CostPerKg => (int)(LaunchCost / Perfomance);
        public int Perfomance { get; set; }
        public LaunchVehicleType LaunchType { get; set; }
        public LaunchVehiclePropellantType PropellantType { get; set; }
        public LaunchVehicleReusabilityType Reusability { get; set; }
        public string[] Photos { get; set; }
        public NewSpaceExternalCompanyModel Company { get; set; }
    }
}
