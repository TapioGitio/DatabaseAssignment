using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Data.Repositories;

public class ProjectRepository(DataContext context, IMemoryCache cache) : BaseRepository<ProjectEntity>(context, cache), IProjectRepository
{
    
    public async Task<ProjectEntity?> ReadDetailedAsync(int projectId)
    {
        var result = await _dbSet
            .Include(x => x.ProjectManager)
            .Include(x => x.Status)
            .Include(x => x.Service)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == projectId); 

        var cacheKey = GetCacheKey(nameof(ReadDetailedAsync), projectId.ToString());
        if (_cache.TryGetValue(cacheKey, out ProjectEntity? cachedEntity))
            return cachedEntity;

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(4));

        return result;
    }

    public async Task<bool> DoesProjectExistAsync(string projectName)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Name == projectName);
            if (result == null) 
                return false;
            return true;

        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}

