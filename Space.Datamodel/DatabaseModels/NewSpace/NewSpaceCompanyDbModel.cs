using System.ComponentModel.DataAnnotations;
using static Space.Shared.Common.CommonEnums;

namespace Space.Server.Datamodel.DatabaseModels.NewSpace
{
    /// <summary>
    /// Full NewSpace company model
    /// </summary>
    public class NewSpaceCompanyDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Oneliner { get; set; }
        public decimal? Funding { get; set; }
        public bool HasFunding { get; set; }
        public CountryName Country { get; set; }
        public string Founded { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
