using Hangfire;

namespace HangFireLogic.Endpoints;

public class BulkScheduleEndpoint : Endpoint<EmptyRequest, EmptyResponse>
{
    private readonly ISuperAwesomeStuff _superAwesomeStuff;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("schedule/bulk");
        Description(x => x.Produces<EmptyResponse>(200, "application/json"));
        AllowAnonymous();
    }

    public BulkScheduleEndpoint(ISuperAwesomeStuff superAwesomeStuff,
        IBackgroundJobClient backgroundJobClient)
    {
        _superAwesomeStuff = superAwesomeStuff;
        _backgroundJobClient = backgroundJobClient;
    }

    public override Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var timeToFire = DateTime.Now.AddSeconds(5);
        
        // Create initial job - set to start in 10 seconds - BUT the job will be paused for 20. 
        var fireJobOne = timeToFire;
        var jobId = _backgroundJobClient.Schedule(() => _superAwesomeStuff.MySuperJobAsync(1, "", fireJobOne), TimeSpan.FromSeconds(5));
        
        // create follow-on jobs. All start 10 seconds apart - building from the first.
        for (var i = 1; i < 10; i++)
        {
            timeToFire = timeToFire.AddSeconds(10);
            var i1 = i;
            var fire = timeToFire;
            
            jobId = _backgroundJobClient.ContinueJobWith(jobId, () => _superAwesomeStuff.MySuperJobAsync(i1+1, jobId, fire));
        }
        
        return SendOkAsync(ct);
    }
}