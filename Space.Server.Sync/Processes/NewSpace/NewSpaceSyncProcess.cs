using MediatR;
using Space.Server.Sync.Processes.NewSpace.Requests;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Processes.NewSpace
{
    /// <summary>
    /// Sync process service for NewSpace
    /// </summary>
    public class NewSpaceSyncProcess : IRequestHandler<NsStartProcessRequest, ServerResult<bool>>
    {

        private readonly IMediator _mediator;

        public NewSpaceSyncProcess(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ServerResult<bool>> Handle(NsStartProcessRequest request, CancellationToken cancellationToken = default)
        {
            var docResult = await _mediator.Send(new NsPageLoadRequest(), cancellationToken);

            if (!docResult.IsCorrect)
                return docResult.Information;

            var doc = docResult.Result;

            var isPageMapped = await _mediator.Send(new NsParseDataRequest(doc), cancellationToken);

            return isPageMapped;
        }
    }
}
