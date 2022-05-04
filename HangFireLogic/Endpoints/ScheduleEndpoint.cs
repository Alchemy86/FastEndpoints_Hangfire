using Hangfire;

namespace HangFireLogic.Endpoints;

public class ScheduleEndpoint : Endpoint<ScheduleRequest, ScheduleResponse>
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ISuperAwesomeStuff _superAwesomeStuff;

    public ScheduleEndpoint(
        IBackgroundJobClient backgroundJobClient,
        ISuperAwesomeStuff superAwesomeStuff)
    {
        _backgroundJobClient = backgroundJobClient;
        _superAwesomeStuff = superAwesomeStuff;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("schedule/create");
        Description(x => x.Produces<EmptyResponse>(200, "application/json"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(ScheduleRequest req, CancellationToken token)
    {
        var response = new ScheduleResponse();
        var jobId = _backgroundJobClient.Enqueue(() => _superAwesomeStuff.MySuperJob(1, "", req.WhenToFire));
        
        await SendAsync(response, cancellation: token);
    }
}

public class ScheduleResponse
{
}

public class ScheduleRequest
{
    public DateTime WhenToFire { get; set; }
    public bool FireFollowups { get; set; }
}