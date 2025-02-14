using Business.Interfaces;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
    private bool _inputCorrect = false;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Status> _statuses = [];

    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    [ObservableProperty]
    private ObservableCollection<ProjectManager> _pms = [];

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private ProjectDetailedView _detailedView;

    [ObservableProperty]
    private ProjectUpdateForm _updateForm = new();

    [ObservableProperty]
    private Status _selectedStatus = null!;

    [ObservableProperty]
    private Service _selectedService = null!;

    [ObservableProperty]
    private ProjectManager _selectedPM = null!;

    [ObservableProperty]
    private Customer _selectedCustomer = null!;

    public ProjectUpdateViewModel(IServiceProvider serviceProvider, 
                                 ProjectDetailedView detailedView, 
                                 IProjectService projectService , 
                                 IStatusService statusService, 
                                 IServiceService serviceService, 
                                 IProjectManagerService projectManagerService, 
                                 ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        DetailedView = detailedView;

        _statusService = statusService;
        _serviceService = serviceService;
        _projectManagerService = projectManagerService;
        _customerService = customerService;
        Task.Run(() => LoadEntityIds());

        // AI helped me with this to be able to check when properties changed
        //thought mvvm would have a automatic way but tried and failed until I
        //found this. It checks when some attributes change and run my method to validate.
        //If it passes the button update becomes available.
        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SelectedStatus) ||
                e.PropertyName == nameof(SelectedService) ||
                e.PropertyName == nameof(SelectedPM) ||
                e.PropertyName == nameof(SelectedCustomer))
            {
                CheckInput();
            }
        };
    }

    private void CheckInput()
    {
        if (SelectedStatus != null
            && SelectedService != null
            && SelectedPM != null
            && SelectedCustomer != null)
        {
            InputCorrect = true;
            ErrorMessage = string.Empty;
        }
        else
        {
            InputCorrect = false;
            ErrorMessage = " All selections must be made in order to update";
        }
    }

    [RelayCommand]
    private async Task UpdateProjectAsync()
    {
        var chechIfDuplicate = await _projectService.ProjectDuplicateAsync(UpdateForm.Name);

        try
        {
            if (string.IsNullOrWhiteSpace(UpdateForm.Name))
                ErrorMessage = " Enter the project name please";

            else if (chechIfDuplicate)
                ErrorMessage = " No can do, a project with the same name already exists";

            else
            {
                UpdateForm.StatusId = SelectedStatus.Id;
                UpdateForm.ServiceId = SelectedService.Id;
                UpdateForm.ProjectManagerId = SelectedPM.Id;
                UpdateForm.CustomerId = SelectedCustomer.Id;

                var result = await _projectService.UpdateProjectAsync(DetailedView.Id, UpdateForm);
                GoBack();
            }
        }
        catch (Exception ex) 
        {
            ErrorMessage = " Error updating the project: Please try again";
            Debug.WriteLine(ex.Message);
        }
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
