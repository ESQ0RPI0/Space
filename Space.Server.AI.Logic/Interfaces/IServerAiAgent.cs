using Microsoft.SemanticKernel.Connectors.OpenAI;
using Space.Shared.Api.ApiResults;

namespace Space.Server.AI.Logic.Interfaces
{
    public interface IServerAiAgent
    {
        Task<ServerResult<TResult>?> ExecuteOperation<TResult>(string prompt, CancellationToken cancellationToken);
        Task<ServerResult<string>> ExecuteOperation(string prompt, CancellationToken cancellationToken);
    }
}