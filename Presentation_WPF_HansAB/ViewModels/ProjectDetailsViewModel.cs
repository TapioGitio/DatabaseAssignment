﻿using Business.Interfaces;
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
    private string _managerPhone;

    [ObservableProperty]
    private string _customerFirstName;

    [ObservableProperty]
    private string _customerLastName;

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
        CustomerEmail = detailedView.CustomerEmail;
        ManagerFirstName = detailedView.ManagerFirstName;
        ManagerLastName = detailedView.ManagerLastName;
        ManagerPhone = detailedView.ManagerPhoneNumber;
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectOverViewModel>();
    }
    [RelayCommand]
    private void UpdateProject()
    {

    }
}

