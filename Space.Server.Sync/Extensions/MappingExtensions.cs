using AutoMapper;
using HtmlAgilityPack;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using static Space.Shared.Common.LaunchVehicleEnums;

namespace Space.Server.Sync.Extensions
{
    public class NewSpaceMappingProfile : Profile
    {
        public NewSpaceMappingProfile()
        {
            CreateMap<HtmlNodeCollection, NewSpaceExternalListItemModel>()
                .ForMember(u => u.Organization, opts => opts.MapFrom(v => v[0].InnerText))
                .ForMember(u => u.Launcher, opts => opts.MapFrom(v => v[1].InnerText))
                .ForMember(u => u.Founded, opts => opts.MapFrom(v => int.Parse(v[2].InnerText)))
                .ForMember(u => u.Status, opts => opts.MapFrom(v => (LaunchVehicleStatus)Enum.Parse(typeof(LaunchVehicleStatus), v[3].InnerText)))
                .ForMember(u => u.FirstLaunch, opts => opts.MapFrom(v => v[4].InnerText))
                .ForMember(u => u.Launches, opts => opts.MapFrom(v => int.Parse(v[5].InnerText)))
                .ForMember(u => u.Cost, opts => opts.MapFrom(v => decimal.Parse(string.Join("", v[6].InnerText.Where(z => char.IsDigit(z))))))
                .ForMember(u => u.Perfomance, opts => opts.MapFrom(v => int.Parse(string.Join("", v[7].InnerText.TakeWhile(z => char.IsDigit(z))))))
                .ForMember(u => u.PricePerKg, opts => opts.MapFrom(v => decimal.Parse(string.Join("", v[8].InnerText.TakeWhile(z => char.IsDigit(z))))))
                .ForMember(u => u.Funding, opts => opts.MapFrom(v => v[9].InnerText.Trim()))
                .ForMember(u => u.Logo, opts => opts.MapFrom(v => v[10].FirstChild.GetAttributeValue("href", "")))
                .ForMember(u => u.Photo, opts => opts.MapFrom(v => v[11].FirstChild.GetAttributeValue("href", "")));

            CreateMap<NewSpaceExternalListItemModel, NewSpaceExternalListItemDbModel>();
        }
    }
}
