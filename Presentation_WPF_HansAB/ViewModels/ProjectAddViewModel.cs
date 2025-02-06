using Business.Interfaces;
using Business.Models.RegForms;
using CommunityToolkit.Mvvm.ComponentModel;
using Data.Entities;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public partial class ProjectAddViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    public ProjectAddViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
    }

    [ObservableProperty]
    private ProjectRegistrationForm _pForm = new();

    [ObservableProperty]
    private ObservableCollection<ProjectEntity> _pEntity = [];
}

