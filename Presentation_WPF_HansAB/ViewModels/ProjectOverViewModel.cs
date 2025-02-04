using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Data.Entities;
using System.Collections.ObjectModel;

namespace Presentation_WPF_HansAB.ViewModels;

public class ProjectOverViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    public ProjectOverViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _projects = new ObservableCollection<ProjectEntity>((IEnumerable<ProjectEntity>)_projectService.ReadAllWithoutDetailsAsync());
    }

    [ObservableProperty]
    private ObservableCollection<ProjectEntity> _projects;
}

