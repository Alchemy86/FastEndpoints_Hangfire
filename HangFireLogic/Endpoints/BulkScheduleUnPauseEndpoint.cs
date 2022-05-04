using HangFireLogic.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HangFireLogic.Endpoints;

public class BulkScheduleUnPauseEndpoint : Endpoint<EmptyRequest, EmptyResponse> 
{
    private readonly CacheService _cacheService;
    
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("schedule/bulk/unpause");
        Description(x => x.Produces<EmptyResponse>(200, "application/json"));
        AllowAnonymous();
    }

    public BulkScheduleUnPauseEndpoint(CacheService cacheService)
    {
        _cacheService = cacheService;
    }

    // Unpause everything!
    public override Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        _cacheService.Cache.Set("Paused", false);
        return Task.CompletedTask;
        // return SendOkAsync(ct);
    }
}