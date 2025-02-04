using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation_ConsoleApp.Dialogs;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\vsProjects\DatabaseAssignment\Data\Data\Local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<IStatusRepository, StatusRepository>();
serviceCollection.AddScoped<IServiceRepository, ServiceRepository>();
serviceCollection.AddMemoryCache();

serviceCollection.AddScoped<IProjectService, ProjectService>();
serviceCollection.AddScoped<IProjectManagerService, ProjectManagerService>();
serviceCollection.AddScoped<ICustomerService, CustomerService>();
serviceCollection.AddScoped<IStatusService, StatusService>();
serviceCollection.AddScoped<IServiceService, ServiceService>();

serviceCollection.AddScoped<MenuDialog>();

 
var serviceProvider = serviceCollection.BuildServiceProvider();

var menu = serviceProvider.GetRequiredService<MenuDialog>();
await menu.MenuChoice();


