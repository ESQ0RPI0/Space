namespace Space.Shared.Common
{
    public class LaunchVehicleEnums
    {
        public enum LaunchVehicleStatus : byte
        {
            Unknown = 0,
            Operational = 1,
            _ = 255
        }
        public enum LaunchVehicleType : byte
        {
            Unknown = 0,
            Land = 1,
        }

        public enum LaunchVehiclePropellantType : byte
        {
            Unknown = 0,
            Solid = 1
        }
        public enum LaunchVehicleReusabilityType : byte
        {
            Unknown = 0,
            NotPlanned = 1,
        }
    }
}
