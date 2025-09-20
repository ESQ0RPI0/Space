using MediatR;
using Space.Server.Services.Interfaces;
using Space.Server.Sync.Processes.NewSpace.Requests;

namespace Space.Server.Sync.Processes.NewSpace.SubProcesses.NewSpace
{
    internal sealed class NewSpaceSyncFinalizationSubProcess : IRequestHandler<NsFinalizeProcessRequest>
    {
        private readonly INewSpaceETLRequestService _service;

        public NewSpaceSyncFinalizationSubProcess(INewSpaceETLRequestService service)
        {
            _service = service;
        }

        public Task Handle(NsFinalizeProcessRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
