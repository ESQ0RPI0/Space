using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Server.Datamodel.DatabaseModels.Main.Space
{
    internal class LaunchVehicleDbModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Organization))]
        public int CompanyId { get; set; }
        public CompanyDbModel Organization { get; set; }
        public string? Launcher { get; set; }
        public int? Founded { get; set; }
        public LaunchVehicleStatus Status { get; set; }
        public DateTimeOffset? FirstLaunch { get; set; }
        public int Launches { get; set; }
        public decimal? Cost { get; set; }
        public int? Perfomance { get; set; }
        public decimal? PricePerKg { get; set; }
        public string Funding { get; set; }
        public string? Logo { get; set; }
        public string? Photo { get; set; }
    }
}
