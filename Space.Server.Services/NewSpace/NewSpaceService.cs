using AutoMapper;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Database.Context;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.NewSpace
{
    public class NewSpaceService
    {
        private readonly NewSpaceContext _dc;
        private readonly IMapper _mapper;
        public NewSpaceService(NewSpaceContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<ServerResult<bool>> AddExternalListItem(NewSpaceExternalListItemModel item)
        {
            var result = _mapper.Map<NewSpaceExternalListItemDbModel>(item);

            _dc.NewSpaceExternalListItems.Add(result);
            await _dc.SaveChangesAsync();

            return ServerResults.CachedTrue;
        }
    }
}
