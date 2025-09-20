using MediatR;
using Space.Server.Sync.Processes.SpaceFund.Requests;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Processes.SpaceFund
{
    public sealed class SpaceFundSyncProcess : IRequestHandler<SfStartProcessRequest, ServerResult<bool>>
    {
        public SpaceFundSyncProcess()
        {

        }

        public async Task<ServerResult<bool>> Handle(SfStartProcessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
