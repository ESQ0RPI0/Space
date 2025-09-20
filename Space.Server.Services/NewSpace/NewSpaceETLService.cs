using Microsoft.EntityFrameworkCore;
using Space.Server.Database.Context;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Server.Services.Interfaces;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.NewSpace
{
    internal sealed class NewSpaceETLService : INewSpaceETLRequestService
    {
        private readonly MainContext _dc;

        public NewSpaceETLService(MainContext dc)
        {
            _dc = dc;
        }

        public Task<ServerResult<Guid>> CreateAsync(NewSpaceETLRequestDbModel entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<long>> GetActiveRequests(CancellationToken cancellationToken)
        {
            return await _dc.ETLRequests
                .Where(x => !x.IsCompleted && x.Count < 10)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public Task<ServerResult<bool>> MarkAsCompletedAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResult<bool>> MarkAsDeletedAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServerResult<bool>> MarkAsFailedAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
