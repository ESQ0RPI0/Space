using AutoMapper;
using HtmlAgilityPack;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Server.Sync.Extensions
{
    public class NewSpaceMappingProfile : Profile
    {
        private static char[] MoneyMultipliers = new char[]{'M', 'B'};
        private static Dictionary<char, int> MoneyMultipliersMap = new Dictionary<char, int>() { { 'M', 1000000 }, { 'B', 1000000000 } };
        private static char DefaultMultiplier = '$';

        public NewSpaceMappingProfile()
        {
            CreateMap<HtmlNodeCollection, NewSpaceExternalListItemModel>()//too vague and sometimes a better solution seems to be a custom mapping
                .ForMember(u => u.Organization, opts => opts.MapFrom(v => v[0].InnerText))
                .ForMember(u => u.Launcher, opts => opts.MapFrom(v => v[1].InnerText))
                .ForMember(u => u.Founded, opts => opts.MapFrom(v => int.Parse(v[2].InnerText)))
                .ForMember(u => u.ItemCurrency, opts => opts.MapFrom(v => DefaultMultiplier))
                .ForMember(u => u.Status, opts => opts.MapFrom(v => (LaunchVehicleStatus)Enum.Parse(typeof(LaunchVehicleStatus), v[3].InnerText)))
                .ForMember(u => u.FirstLaunch, opts => opts.MapFrom(v => v[4].InnerText))
                .ForMember(u => u.Launches, opts => opts.MapFrom(v => int.Parse(v[5].InnerText)))
                .ForMember(u => u.IsCostDefined, opts => opts.MapFrom(v => v[6].InnerText.Any(z => char.IsDigit(z))))
                .ForMember(u => u.Cost, opts => opts.MapFrom(v => decimal.Parse(string.Join("", v[6].InnerText.Where(z => char.IsDigit(z))))))
                .ForMember(u => u.CostMultiplier, opts => opts.MapFrom(v => v[6].InnerText.FirstOrDefault(z => MoneyMultipliers.Contains(z), DefaultMultiplier)))
                .ForMember(u => u.Perfomance, opts => opts.MapFrom(v => int.Parse(string.Join("", v[7].InnerText.TakeWhile(z => char.IsDigit(z))))))
                .ForMember(u => u.PricePerKg, opts => opts.MapFrom(v => decimal.Parse(string.Join("", v[8].InnerText.TakeWhile(z => char.IsDigit(z))))))
                .ForMember(u => u.IsFundingDefined, opts => opts.MapFrom(v => v[9].InnerText.Any(z => char.IsDigit(z))))
                .ForMember(u => u.Funding, opts => opts.MapFrom(v => v[9].InnerText.Trim()))
                .ForMember(u => u.FundingMultiplier, opts => opts.MapFrom(v => v[9].InnerText.FirstOrDefault(z => MoneyMultipliers.Contains(z), DefaultMultiplier)))
                .ForMember(u => u.Logo, opts => opts.MapFrom(v => v[10].FirstChild.GetAttributeValue("href", "")))
                .ForMember(u => u.Photo, opts => opts.MapFrom(v => v[11].FirstChild.GetAttributeValue("href", "")));

            CreateMap<NewSpaceExternalListItemModel, NewSpaceExternalListItemDbModel>()
                .ForMember(u => u.Organization, opts => opts.MapFrom(v => v.Organization))
                .ForMember(u => u.Launcher, opts => opts.MapFrom(v => v.Launcher))
                .ForMember(u => u.Founded, opts => opts.MapFrom(v => v.Founded))
                .ForMember(u => u.Status, opts => opts.MapFrom(v => v.Status))
                .ForMember(u => u.FirstLaunch, opts => opts.MapFrom(v => v.FirstLaunch))
                .ForMember(u => u.Launches, opts => opts.MapFrom(v => v.Launches))
                .ForMember(u => u.Cost, opts => {
                    opts.PreCondition(v => v.IsCostDefined);
                    opts.MapFrom(v => Convert.ToDecimal(v.Cost) * MoneyMultipliersMap.GetValueOrDefault(v.CostMultiplier.Value, 1));
                    })
                .ForMember(u => u.Perfomance, opts => opts.MapFrom(v => v.Perfomance))
                .ForMember(u => u.PricePerKg, opts => opts.MapFrom(v => v.PricePerKg))
                .ForMember(u => u.HasFunding, opts => opts.MapFrom(v => v.IsFundingDefined || !string.IsNullOrEmpty(v.Funding)))
                .ForMember(u => u.Funding, opts => {
                    opts.PreCondition(v => v.IsFundingDefined);
                    opts.MapFrom(v => Convert.ToDecimal(v.Funding) * MoneyMultipliersMap.GetValueOrDefault(v.FundingMultiplier.Value, 1));
                    })
                .ForMember(u => u.Logo, opts => opts.MapFrom(v => v.Logo))
                .ForMember(u => u.Photo, opts => opts.MapFrom(v => v.Photo));
        }
    }
}
