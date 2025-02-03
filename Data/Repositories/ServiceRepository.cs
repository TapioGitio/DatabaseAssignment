using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories;

public class ServiceRepository(DataContext context, IMemoryCache cache) : BaseRepository<ServiceEntity>(context, cache), IServiceRepository
{

}

