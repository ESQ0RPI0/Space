using System.Linq.Expressions;

namespace Space.Server.Services.Interfaces.Common
{
    public interface IRecurrentProcessor
    {
        Guid AddOrUpdate<T>(Expression<Action<T>> methodCall, string cronExpression, string queue);
        void RemoveIfExists(string recurringJobId);
        void Trigger(string reccuringJobId);
    }
}
