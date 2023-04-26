using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Shared.Api.ApiResults.Base
{
    public abstract class ServerInformationAbstract
    {
        public string Message { get; init; }
        public ServerErrorCodes? Code { get; init; }
        public ServerMessageTypes? Type { get; init; }
    }
}
