using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

        // AI helped me with this to be able to check when properties changed
        //thought mvvm would have a automatic way but tried and failed until I
        //found this. It checks when some attributes change and run my method to validate.
        //If it passes the button update becomes available
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

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _inputCorrect = false;

    [ObservableProperty]
    private ObservableCollection<Status> _statuses = [];

    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    [ObservableProperty]
    private ObservableCollection<ProjectManager> _pms = [];

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

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
        var chechIfDuplicate = await _projectService.ProjectDuplicateAsync(PForm.Name);

        try
        {
            if (string.IsNullOrWhiteSpace(PForm.Name))
                ErrorMessage = " Enter Project Name please";

            else if (chechIfDuplicate)
                ErrorMessage = " A project with the same name already exists";

            else
            {
                PForm.StatusId = SelectedStatus.Id;
                PForm.ServiceId = SelectedService.Id;
                PForm.ProjectManagerId = SelectedPM.Id;
                PForm.CustomerId = SelectedCustomer.Id;

                await _projectService.CreateProjectAsync(PForm);
                GoBack();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = " Error creating the project, try again later!";
            Debug.WriteLine(ex.Message);
        }
    
    }

    private async Task LoadEntityIds()
    {
        // Ai helped me with this listing of the entities
        // it fetches all the attributes from the entities
        // and adds them to a observableCollection to be able
        // to pick a specific one in a combobox.

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

