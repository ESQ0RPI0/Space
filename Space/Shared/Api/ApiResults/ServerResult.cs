using Space.Shared.Api.ApiResults.Base;
using Space.Shared.Common.Server;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Shared.Api.ApiResults
{
    public class ServerResult<T>
    {
        public T Result { get; init; }
        public ServerInformationAbstract Information { get; init; }
        public bool IsCorrect => Information == null || !Information.Code.HasValue || Information.Code == ServerErrorCodes.None || Information.Code == ServerErrorCodes.Ok;

        public ServerResult()
        {
        }

        public ServerResult(T result)
        {
            Result = result;
        }

        public ServerResult(ServerError error)
        {
            Information = error;
        }
        public ServerResult(T result, string message, ServerMessageTypes type = ServerMessageTypes.None)
        {
            Result = result;
            Information = new ServerMessage(message, type);
        }

        public static implicit operator ServerResult<T>(T result) => new ServerResult<T>(result);
        public static implicit operator ServerResult<T>(ServerErrorCodes error) => new ServerResult<T>(error);
    }

    public class ServerError : ServerInformationAbstract
    {
        public ServerError(ServerErrorCodes errorCode, string message = null, ServerMessageTypes type = ServerMessageTypes.Error)
        {
            Code = errorCode;
            Message = message ?? errorCode.GetDescription();
            Type = type;
        }

        public static implicit operator ServerError(ServerErrorCodes serverError) => new ServerError(serverError);
    }

    public class ServerMessage : ServerInformationAbstract
    {
        public ServerMessage(string message, ServerMessageTypes type = ServerMessageTypes.None)
        {
            Message = message;
            Type = type;
        }
    }

    public static class ServerResults
    {
        public static ServerResult<bool> CachedTrue { get; private set; } = new ServerResult<bool>(true);
        public static ServerResult<bool> CachedFalse { get; private set; } = new ServerResult<bool>(false);
    }

    public static class ServerResultsExtensions
    {
        public static ServerResult<T> ToServerResult<T>(this T result) where T : new()
        {
            return new ServerResult<T>(result);
        }
        public static ServerResult<T> ToServerResult<T>(this T result, string message) where T: new()
        {
            return new ServerResult<T>(result, message);
        }
        public static ServerResult<T> ToServerResult<T>(this T result, string message, ServerMessageTypes type) where T : new()
        {
            return new ServerResult<T>(result, message, type);
        }
        public static ServerResult<T> WithExtras<T>(this ServerErrorCodes code, string message, ServerMessageTypes type = ServerMessageTypes.Error)
        {
            var error = new ServerError(code, message, type);

            return new ServerResult<T>(error);
        }
    }
}
