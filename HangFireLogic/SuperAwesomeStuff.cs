using HangFireLogic.Services;
using Microsoft.Extensions.Caching.Memory;
namespace HangFireLogic;

public class SuperAwesomeStuff : ISuperAwesomeStuff
{
    private readonly ILogger<SuperAwesomeStuff> _logger;
    private readonly CacheService _cacheService;

    public SuperAwesomeStuff(ILogger<SuperAwesomeStuff> logger, CacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    /// <summary>
    /// Super amazing method to fire and wait
    /// </summary>
    /// <param name="workId"></param>
    /// <param name="followingJob"></param>
    /// <param name="freezeUntil"></param>
    /// <returns></returns>
    public async Task<int> MySuperJobAsync(int workId, string followingJob, DateTime freezeUntil)
    {
        _logger.LogInformation($"Job: {workId}: HANGFIRE JOB STARTED FOLLOWING JOB: {followingJob}");
        
        // Lets go ahead and wait till we should be running even if we have been triggered by hangfire
        while (true)
        {
            if (DateTime.Now >= freezeUntil)
                break;
        }
        
        _logger.LogInformation($"Job: {workId}: WAIT TIME EXPIRED");

        while (true)
        {
            if (!_cacheService.Cache.Get<bool>("Paused"))
                break;
        }
        
        _logger.LogInformation($"Job: {workId}: UNPAUSED - COMPLETE");
        
        return await Task.FromResult(workId);
    }
    
    public int MySuperJob(int workId, string followingJob, DateTime freezeUntil)
    {
        _logger.LogInformation($"Job: {workId}: HANGFIRE JOB STARTED FOLLOWING JOB: {followingJob}");
        
        // Lets go ahead and wait till we should be running even if we have been triggered by hangfire
        while (true)
        {
            if (DateTime.Now >= freezeUntil)
                break;
        }
        
        _logger.LogInformation($"Job: {workId}: WAIT TIME EXPIRED");
        
        // Wait until we have been unpaused as a global setting
        while (true)
        {
            if (!_cacheService.Cache.Get<bool>("Paused"))
                break;
        }
        
        _logger.LogInformation($"Job: {workId}: UNPAUSED - COMPLETE");

        return workId;
    }
    
}

public interface ISuperAwesomeStuff
{
    Task<int> MySuperJobAsync(int workId, string followingJob, DateTime freezeUntil);
    int MySuperJob(int workId, string followingJob, DateTime freezeUntil);
}