﻿using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Client.Datamodel.ViewModels
{
    public class NsRawItemViewModel
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string? Launcher { get; set; }
        public int? Founded { get; set; }
        public LaunchVehicleStatus Status { get; set; }
        public string? FirstLaunch { get; set; }
        public int Launches { get; set; }
        public decimal? Cost { get; set; }
        public int? Perfomance { get; set; }
        public decimal? PricePerKg { get; set; }
        public decimal? Funding { get; set; }
        public bool HasFunding { get; set; }
        public string? Logo { get; set; }
        public string? Photo { get; set; }
    }
}
