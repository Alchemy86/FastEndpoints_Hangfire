using Hangfire;

namespace HangFireLogic.HostedServices;

public class Scheduler : IHostedService
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public Scheduler(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _backgroundJobClient.Enqueue(() => Console.WriteLine("Kicked off"));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}