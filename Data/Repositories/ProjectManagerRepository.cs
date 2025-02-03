using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories;

public class ProjectManagerRepository(DataContext context, IMemoryCache cache) : BaseRepository<ProjectManagerEntity>(context, cache), IProjectManagerRepository
{

}

