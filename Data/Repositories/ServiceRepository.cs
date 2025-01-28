using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{

}

