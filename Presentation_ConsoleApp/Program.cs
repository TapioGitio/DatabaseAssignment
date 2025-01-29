using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\vsProjects\DatabaseAssignment\Data\Data\Local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<IStatusRepository, StatusRepository>();
serviceCollection.AddScoped<IServiceRepository, ServiceRepository>();



var serviceProvider = serviceCollection.BuildServiceProvider();

//var menu = serviceProvider.GetRequiredService<Menu>();