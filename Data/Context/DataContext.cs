using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<StatusEntity> Status { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProjectManagerEntity> ProjectManagers { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
}
