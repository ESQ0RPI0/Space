using AutoMapper;
using HtmlAgilityPack;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Client.Datamodel.ViewModels;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Server.Datamodel.Models.NewSpace;
using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Server.Sync.Extensions
{
    public class NewSpaceMappingProfile : Profile
    {
        private static char[] MoneyMultipliers = ['M', 'B'];
        private static Dictionary<char, int> MoneyMultipliersMap = new Dictionary<char, int>()
        {
            { 'M', 1000000 },
            { 'B', 1000000000 }
        };

        private static char DefaultCurrency = '$';
        private static char[] StatusRelatedParasiteMarks = ['?'];

        public NewSpaceMappingProfile()
        {
            CreateMap<HtmlNodeCollection, NewSpaceExternalListItemModel>()//too vague and sometimes a better solution seems to be a custom mapping
                .ForMember(u => u.Organization, opts => opts.MapFrom(v => v[0].InnerText))
                .ForMember(u => u.Launcher, opts => opts.MapFrom(v => v[1].InnerText))
                .ForMember(u => u.FirstLaunch, opts => opts.MapFrom(v => v[2].InnerText))
                .ForMember(u => u.Status, opts => opts.MapFrom(v => new string(v[3].InnerText.Where(z => !StatusRelatedParasiteMarks.Contains(z)).ToArray())))
                .ForMember(u => u.ItemCurrency, opts => opts.MapFrom(v => DefaultCurrency))
                .ForMember(u => u.Launches, opts => opts.MapFrom(v => v[4].InnerText))
                .ForMember(u => u.Cost, opts =>
                {
                    opts.PreCondition(v => v[5].InnerText.Any(z => char.IsDigit(z)));
                    opts.MapFrom(v => string.Join("", v[5].InnerText.Where(z => char.IsDigit(z))));
                })
                .ForMember(u => u.CostMultiplier, opts => opts.MapFrom(v => v[5].InnerText.FirstOrDefault(z => MoneyMultipliers.Contains(z), DefaultCurrency)))
                .ForMember(u => u.Perfomance, opts =>
                {
                    opts.PreCondition(v => v[6].InnerText.Any(z => char.IsDigit(z)));
                    opts.MapFrom(v => string.Join("", v[6].InnerText.TakeWhile(z => char.IsDigit(z))));
                })
                .ForMember(u => u.IsFundingExist, opts => opts.MapFrom(v => v[7].InnerText.Contains("Yes") || v[7].InnerText.Any(z => char.IsDigit(z))))
                .ForMember(u => u.Funding, opts =>
                {
                    opts.PreCondition(v => v[7].InnerText.Any(z => char.IsDigit(z)));
                    opts.MapFrom(v => new string(v[7].InnerText.Trim().Where(z => char.IsDigit(z)).ToArray()));
                })
                .ForMember(u => u.FundingMultiplier, opts => opts.MapFrom(v => v[7].InnerText.FirstOrDefault(z => MoneyMultipliers.Contains(z), DefaultCurrency)))
                .ForMember(u => u.Logo, opts => opts.MapFrom(v => v[8].FirstChild.GetAttributeValue("href", "")))
                .ForMember(u => u.Photo, opts => opts.MapFrom(v => v[9].FirstChild.GetAttributeValue("href", "")));

            CreateMap<NewSpaceExternalListItemModel, NewSpaceExternalListItemDbModel>()
                .ForMember(u => u.Organization, opts => opts.MapFrom(v => v.Organization))
                .ForMember(u => u.Launcher, opts => opts.MapFrom(v => v.Launcher))
                .ForMember(u => u.Founded, opts => opts.MapFrom(v => Convert.ToInt32(v.Founded)))
                .ForMember(u => u.Status, opts => opts.MapFrom(v => (LaunchVehicleStatus)Enum.Parse(typeof(LaunchVehicleStatus), v.Status)))
                .ForMember(u => u.FirstLaunch, opts => opts.MapFrom(v => v.FirstLaunch))
                .ForMember(u => u.Launches, opts => opts.MapFrom(v => Convert.ToInt32(v.Launches)))
                .ForMember(u => u.Cost, opts =>
                {
                    opts.PreCondition(v => !string.IsNullOrEmpty(v.Cost));
                    opts.MapFrom(v => Convert.ToDecimal(v.Cost) * MoneyMultipliersMap.GetValueOrDefault(v.CostMultiplier.Value, 1));
                })
                .ForMember(u => u.Perfomance, opts =>
                {
                    opts.PreCondition(v => !string.IsNullOrEmpty(v.Perfomance));
                    opts.MapFrom(v => Convert.ToInt32(v.Perfomance));
                })
                .ForMember(u => u.PricePerKg, opts =>
                {
                    opts.PreCondition(v => !string.IsNullOrEmpty(v.PricePerKg));
                    opts.MapFrom(v => Convert.ToDecimal(new string(v.PricePerKg.Where(z => char.IsDigit(z)).ToArray())));
                })
                .ForMember(u => u.HasFunding, opts => opts.MapFrom(v => v.IsFundingExist))
                .ForMember(u => u.Funding, opts =>
                {
                    opts.PreCondition(v => v.IsFundingExist && !string.IsNullOrEmpty(v.Funding) && v.Funding.Any(z => char.IsDigit(z)));
                    opts.MapFrom(v => Convert.ToDecimal(v.Funding) * MoneyMultipliersMap.GetValueOrDefault(v.FundingMultiplier.Value, 1));
                })
                .ForMember(u => u.Logo, opts => opts.MapFrom(v => v.Logo))
                .ForMember(u => u.Photo, opts => opts.MapFrom(v => v.Photo))
                .ForMember(u => u.Created, opts =>
                {
                    opts.UseDestinationValue();
                });

            CreateMap<NewSpaceExternalListItemDbModel, LaunchVehicleRawViewModel>();
        }
    }
}
