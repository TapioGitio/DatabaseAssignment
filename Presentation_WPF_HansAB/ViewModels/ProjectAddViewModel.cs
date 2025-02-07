using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectAddViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;
    private readonly IServiceService _serviceService;
    private readonly IProjectManagerService _projectManagerService;
    private readonly ICustomerService _customerService;

    public ProjectAddViewModel(IServiceProvider serviceProvider, IProjectService projectService, IStatusService statusService, IServiceService serviceService, IProjectManagerService projectManagerService, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _statusService = statusService;
        _serviceService = serviceService;
        _projectManagerService = projectManagerService;
        _customerService = customerService;

        Task.Run(() => LoadEntityIds());
    }
    [ObservableProperty]
    private ObservableCollection<Status> _statuses = new ObservableCollection<Status>();

    [ObservableProperty]
    private ObservableCollection<Service> _services = new ObservableCollection<Service>();

    [ObservableProperty]
    private ObservableCollection<ProjectManager> _pms = new ObservableCollection<ProjectManager>();

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();

    [ObservableProperty]
    private ProjectRegistrationForm _pForm = new();

    [ObservableProperty]
    private ObservableCollection<ProjectEntity> _pEntity = [];

    [ObservableProperty]
    private Status _selectedStatus = null!;
    [ObservableProperty]
    private Service _selectedService = null!;
    [ObservableProperty]
    private ProjectManager _selectedPM = null!;
    [ObservableProperty]
    private Customer _selectedCustomer = null!;

    [RelayCommand]
    private void GoBack()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectOverViewModel>();
    }

    [RelayCommand]
    private async Task SaveProject()
    {
        PForm.StatusId = SelectedStatus.Id;
        PForm.ServiceId = SelectedService.Id;
        PForm.ProjectManagerId = SelectedPM.Id;
        PForm.CustomerId = SelectedCustomer.Id;

        await _projectService.CreateProjectAsync(PForm);
        GoBack();
    }

    private async Task LoadEntityIds()
    {
        var statuses = await _statusService.ReadStatusAsync();

        foreach (var status in statuses)
        {
            Statuses.Add(status);
        }

        var services = await _serviceService.ReadServicesAsync();
        foreach (var service in services)
        {
            Services.Add(service);
        }

        var projectManagers = await _projectManagerService.ReadPMAsync();
        foreach (var pm in projectManagers)
        {
            Pms.Add(pm);
        }

        var customers = await _customerService.ReadCustomersAsync();
        foreach (var customer in customers)
        {
            Customers.Add(customer);
        }
    }
}

