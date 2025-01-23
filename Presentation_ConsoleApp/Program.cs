using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\vsProjects\DatabaseAssignment\Data\Data\Local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));


var serviceProvider = serviceCollection.BuildServiceProvider();

//var menu = serviceProvider.GetRequiredService<Menu>();