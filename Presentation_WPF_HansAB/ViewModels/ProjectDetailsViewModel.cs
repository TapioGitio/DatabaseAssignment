using Business.Interfaces;
using Business.Models.SafeToDisplay;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private int _projectId;

    [ObservableProperty]
    private string _projectName;

    [ObservableProperty]
    private DateTime _startDate;

    [ObservableProperty]
    private DateTime _endDate;

    [ObservableProperty]
    private string _status;

    [ObservableProperty]
    private string _serviceName;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private string _managerFirstName;

    [ObservableProperty]
    private string _managerLastName;

    [ObservableProperty]
    private string _managerFullName;

    [ObservableProperty]
    private string _managerPhone;

    [ObservableProperty]
    private string _customerFirstName;

    [ObservableProperty]
    private string _customerLastName;

    [ObservableProperty]
    private string _customerFullName;

    [ObservableProperty]
    private string _customerEmail;

    public ProjectDetailsViewModel(IServiceProvider serviceProvider, IProjectService projectService, ProjectDetailedView detailedView)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;

        ProjectId = detailedView.Id;
        ProjectName = detailedView.Name;
        StartDate = detailedView.StartDate;
        EndDate = detailedView.EndDate;
        Status = detailedView.Status;
        ServiceName = detailedView.ServiceName;
        Price = detailedView.Price;
        CustomerFirstName = detailedView.CustomerFirstName;
        CustomerLastName = detailedView.CustomerLastName;
        CustomerFullName = detailedView.CustomerFullName;
        CustomerEmail = detailedView.CustomerEmail;
        ManagerFirstName = detailedView.ManagerFirstName;
        ManagerLastName = detailedView.ManagerLastName;
        ManagerFullName = detailedView.ManagerFullName;
        ManagerPhone = detailedView.ManagerPhoneNumber;
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectOverViewModel>();
    }

    [RelayCommand]
    private async Task GoToUpdateView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

        var updateProject = await _projectService.ReadOneDetailedAsync(ProjectId);

        // Got help from AI to be able to send parameters through to another viewmodel
        // like here im sending the updateProject to the ProjectUpdateViewModel
        // so that the user can update the project with the correct data from the database.

        // The GetRequiredService was not able to send parameters.
        var updateView = ActivatorUtilities.CreateInstance<ProjectUpdateViewModel>(_serviceProvider, updateProject);

        mainViewModel.CurrentViewModel = updateView;
    }
}

    