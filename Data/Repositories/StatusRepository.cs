using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories;

public class StatusRepository(DataContext context, IMemoryCache cache) : BaseRepository<StatusEntity>(context, cache), IStatusRepository
{

}

