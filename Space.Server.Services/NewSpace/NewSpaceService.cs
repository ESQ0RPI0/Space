using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Database.Context;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Server.Services.Interfaces;
using Space.Server.Services.Interfaces.Common;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.NewSpace
{
    /// <summary>
    /// NewSpace entities service
    /// </summary>
    internal sealed class NewSpaceService : INewSpaceService
    {
        private readonly NewSpaceContext _dc;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<NewSpaceService> _logger;

        public NewSpaceService(NewSpaceContext dc, IMapper mapper, IDateTimeService dateTimeService, ILogger<NewSpaceService> logger)
        {
            _dc = dc;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        public async Task<ServerResult<bool>> AddOrUpdateExternalListItems(IEnumerable<NsExternalListItemModel> items, CancellationToken cancellationToken)
        {
            var dateTime = _dateTimeService.GetCurrentDateTime().Date;
            var date = DateOnly.FromDateTime(dateTime);

            var companies = items.Select(x => x.Organization);

            var existingEntities = await _dc.NewSpaceExternalListItems.Where(x => string.IsNullOrEmpty(x.Launcher) &&
                x.CreatedDate == date &&
                companies.Contains(x.Organization))
                .ToDictionaryAsync(x => (x.Organization, x.Launcher), cancellationToken);

            var result = GetValidEntitiesAsync().ToArray();

            await _dc.NewSpaceExternalListItems.AddRangeAsync(result, cancellationToken);
            await _dc.SaveChangesAsync(cancellationToken);

            return ServerResults.CachedTrue;

            IEnumerable<NewSpaceExternalListItemDbModel> GetValidEntitiesAsync()
            {
                NewSpaceExternalListItemDbModel result;

                foreach (var item in items)
                {
                    if (cancellationToken.IsCancellationRequested)
                        yield break;

                    if (existingEntities.TryGetValue((item.Organization, item.Launcher), out var existing))
                    {
                        _logger.LogInformation($"{nameof(NewSpaceService)}: NewSpace launcher {item.Launcher} was already loaded at date {date}");
                        continue;
                    }

                    result = _mapper.Map<NewSpaceExternalListItemDbModel>(item);

                    result.Created = dateTime;
                    result.CreatedDate = date;

                    yield return result;
                }
            }
        }
    }
}
