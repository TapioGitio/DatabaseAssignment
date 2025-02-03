﻿using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
namespace Data.Repositories;

public class CustomerRepository(DataContext context, IMemoryCache cache) : BaseRepository<CustomerEntity>(context, cache), ICustomerRepository
{

}

