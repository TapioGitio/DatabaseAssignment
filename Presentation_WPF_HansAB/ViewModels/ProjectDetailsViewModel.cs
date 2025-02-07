using Business.Interfaces;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private ObservableCollection<ProjectUpdateForm> _form = [];

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
    private async Task UpdateProject()
    {

    }
}

