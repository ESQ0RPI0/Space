using Hangfire;
using Space.Server.Services.Interfaces.Common;
using System.Linq.Expressions;

namespace Space.Server.Services.Background
{
    public class HangfireRecurrentProcessor : IRecurrentProcessor
    {
        public Guid AddOrUpdate<T>(Expression<Action<T>> methodCall, string cronExpression, string queue)
        {
            var jobId = Guid.NewGuid();

            RecurringJob.AddOrUpdate(recurringJobId: jobId.ToString(), methodCall: methodCall, cronExpression: cronExpression, queue: queue);

            return jobId;
        }

        public void RemoveIfExists(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }

        public void Trigger(string reccuringJobId)
        {
            RecurringJob.TriggerJob(reccuringJobId);
        }
    }
}
