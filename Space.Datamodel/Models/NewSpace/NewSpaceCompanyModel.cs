using static Space.Shared.Common.CommonEnums;

namespace Space.Server.Datamodel.Models.NewSpace
{
    public class NewSpaceCompanyModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Oneliner { get; set; }
        public decimal Funding { get; set; }
        public bool IsFunded { get; set; }
        public CountryName Country { get; set; }
        public DateTime Founded { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? TwitterUrl { get; set; }
    }
}
