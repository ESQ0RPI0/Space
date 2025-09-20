using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Server.Database.Context;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Server.Services.Interfaces;
using Space.Shared.Api.ApiResults;
using System.Linq.Expressions;

namespace Space.Server.Services.NewSpace
{
    internal class NewSpaceQueryService : INewSpaceQueryService
    {
        private readonly NewSpaceContext _dc;
        private readonly IMapper _mapper;

        public NewSpaceQueryService(NewSpaceContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<ServerResult<IEnumerable<NewSpaceExternalListItemDbModel>>> GetRawListByPredicate(Expression<Func<NewSpaceExternalListItemDbModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dc.NewSpaceExternalListItems.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<ServerResult<List<NsRawItemViewModel>>> GetRawViewModelByForm(PagingForm form, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceExternalListItems
                .AsNoTracking()
                .Skip(form.Offset)
                .Take(form.Count)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<NsRawItemViewModel>>(result);
        }

        public async Task<ServerResult<NsRawLaunchVehicleViewModel>> GetVehicle(int id, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceLaunchVehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return _mapper.Map<NsRawLaunchVehicleViewModel>(result);
        }

        public async Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> GetVehiclesByCountryAsync(string country, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceLaunchVehicles
                .AsNoTracking()
                .Where(u => !string.IsNullOrEmpty(u.Country) && u.Country == country)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<NsRawLaunchVehicleViewModel>>(result);
        }
    }
}
