using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_WPF_HansAB.ViewModels;
using Presentation_WPF_HansAB.Views;
using System.Windows;

namespace Presentation_WPF_HansAB;


public partial class App : Application
{
    private readonly IHost _host;
    public App()
    {
        _host = Host.CreateDefaultBuilder()

        .ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\vsProjects\DatabaseAssignment\Data\Data\Local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddMemoryCache();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectManagerService, ProjectManagerService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IServiceService, ServiceService>();

            services.AddScoped<ProjectOverViewModel>();
            services.AddScoped<ProjectOverViewView>();


            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>();
        })
        .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
