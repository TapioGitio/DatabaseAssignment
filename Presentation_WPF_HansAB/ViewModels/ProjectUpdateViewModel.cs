using Business.Interfaces;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectUpdateViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;
    private readonly IServiceService _serviceService;
    private readonly IProjectManagerService _projectManagerService;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<Status> _statuses = new();

    [ObservableProperty]
    private ObservableCollection<Service> _services = new();

    [ObservableProperty]
    private ObservableCollection<ProjectManager> _pms = new();

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = new();

    [ObservableProperty]
    private ProjectDetailedView _detailedView;
    [ObservableProperty]
    private ProjectUpdateForm _upgradeForm = new();

    [ObservableProperty]
    private Status _selectedStatus = null!;
    [ObservableProperty]
    private Service _selectedService = null!;
    [ObservableProperty]
    private ProjectManager _selectedPM = null!;
    [ObservableProperty]
    private Customer _selectedCustomer = null!;

    public ProjectUpdateViewModel(IServiceProvider serviceProvider, ProjectDetailedView detailedView, IProjectService projectService , IStatusService statusService, IServiceService serviceService, IProjectManagerService projectManagerService, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        DetailedView = detailedView;

        _statusService = statusService;
        _serviceService = serviceService;
        _projectManagerService = projectManagerService;
        _customerService = customerService;
        Task.Run(() => LoadEntityIds());
    }

    [RelayCommand]
    private async Task UpdateProjectAsync()
    {
        UpgradeForm.StatusId = SelectedStatus.Id;
        UpgradeForm.ServiceId = SelectedService.Id;
        UpgradeForm.ProjectManagerId = SelectedPM.Id;
        UpgradeForm.CustomerId = SelectedCustomer.Id;

        await _projectService.UpdateProjectAsync(DetailedView.Id, UpgradeForm);
        GoBack();
    }

    [RelayCommand]

    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectOverViewModel>();
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
