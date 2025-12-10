using Quartz;

namespace Template.Application.Abstractions.Integration.Quartz;

public interface IScheduledJob : IJob
{
    static abstract string JobName { get; }
}
