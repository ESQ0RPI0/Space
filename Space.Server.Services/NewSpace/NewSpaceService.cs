﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Server.Database.Context;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.NewSpace
{
    internal class NewSpaceService : INewSpaceService
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

        public async Task<ServerResult<List<NsRawItemViewModel>>> GetRawList(PagingForm form, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceExternalListItems
                .AsNoTracking()
                .Skip(form.Offset)
                .Take(form.Count)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<NsRawItemViewModel>>(result);
        }

        public async Task<ServerResult<NsRawLaunchVehicleViewModel>> GetRawVehicle(int id, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceLaunchVehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return _mapper.Map<NsRawLaunchVehicleViewModel>(result);
        }

        public async Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> GetByCountryAsync(string country, CancellationToken cancellationToken)
        {
            var result = await _dc.NewSpaceLaunchVehicles
                .Where(u => !string.IsNullOrEmpty(u.Country) && u.Country == country)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<NsRawLaunchVehicleViewModel>>(result);
        }
    }
}
