using Space.Shared.Common.Server;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Shared.Api.Types
{
    public class ServerResult<T>
    {
        public T Result { get; init; }
        public ServerError Error { get; init; }
        public bool IsCorrect => Error == null || Error.ErrorCode != ServerErrorCodes.None || Error.ErrorCode != ServerErrorCodes.Ok;
        public ServerMessage Message { get; set; }

        public ServerResult()
        {
        }

        public ServerResult(T result)
        {
            Result = result;
        }

        public ServerResult(ServerError error)
        {
            Error = error;
        }

        public static implicit operator ServerResult<T>(T result) => new ServerResult<T>(result);
        public static implicit operator ServerResult<T>(ServerErrorCodes error) => new ServerResult<T>(error);

        public static ServerResult<bool> CachedTrue = new ServerResult<bool>(true);
        public static ServerResult<bool> CachedFalse = new ServerResult<bool>(false);

    }

    public class ServerError
    {
        public ServerErrorCodes ErrorCode { get; set; }
        public string Message { get; set; }

        public ServerError(ServerErrorCodes errorCode, string message = null)
        {
            ErrorCode = errorCode;
            Message = message ?? errorCode.GetDescription();
        }

        public static implicit operator ServerError(ServerErrorCodes serverError) => new ServerError(serverError);
    }

    public class ServerMessage
    {
        public string Message { get; set; }
        public ServerMessageTypes Type { get; set; }

        public ServerMessage(string message, ServerMessageTypes type = ServerMessageTypes.None)
        {
            Message = message;
            Type = type;
        }
    }

    public static class ServerResultsExtensions
    {
        public static ServerResult<T> WithMessage<T>(this ServerResult<T> result, string message)
        {
            result.Message = new ServerMessage(message);

            return result;
        }
        public static ServerResult<T> WithMessage<T>(this ServerResult<T> result, string message, ServerMessageTypes type)
        {
            result.Message = new ServerMessage(message, type);

            return result;
        }

        public static ServerResult<T> ToServerResult<T>(this T result)
        {
            return new ServerResult<T>(result);
        }
    }

}
